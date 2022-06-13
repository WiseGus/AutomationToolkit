import { Component, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { ApiService } from "../svc/api.service";
import { Preset, Keyword } from "../create-template/preset";
import { DisableElementDirective } from "../directives/disable-element.directive";
import { merge } from "rxjs/observable/merge";
import { AppSettings } from "../settings/appsettings-model";
import { forkJoin } from "rxjs/observable/forkJoin";

export enum jobStatusEnum {
  None,
  Working,
  DoneOK,
  DoneError,
}

@Component({
  selector: "app-generate-project",
  templateUrl: "./generate-project.component.html",
  styleUrls: ["./generate-project.component.css"],
})
export class GenerateProjectComponent implements OnInit {
  jobStatusEnum = jobStatusEnum;

  preset: Preset;
  keywords: Keyword[];
  globalKeywords: Keyword[];
  showDetails = false;
  jobStatus: jobStatusEnum = jobStatusEnum.None;
  resultMessage: string;
  useAutomationUpdates: boolean;

  constructor(private _api: ApiService, private _route: ActivatedRoute) {}

  ngOnInit() {
    this._route.params.subscribe((prms) => {
      const presetAlias = prms["alias"];
      const preset$ = this._api.getSingle<Preset>("presets", presetAlias);
      const globalKeywords$ = this._api.getAll<AppSettings[]>("settings");

      forkJoin(preset$, globalKeywords$).subscribe((p) => {
        this.preset = p[0];
        this.globalKeywords = p[1];
        this.keywords = this.preset.keywords.filter((x) => x.showInGenerate);
        this.useAutomationUpdates = (<any>(
          this.preset
        )).automationUpdates.useAutomationUpdates;
      });
    });
  }

  toggleShowDetails() {
    this.showDetails = !this.showDetails;
  }

  generateProject() {
    this.jobStatus = jobStatusEnum.Working;
    (<any>(
      this.preset
    )).automationUpdates.useAutomationUpdates = this.useAutomationUpdates;
    console.log("Generating template: ", this.preset);

    const finalPreset = {
      ...this.preset,
      keywords: this.preset.keywords.concat(this.globalKeywords),
    };

    this._api.post("generateprojects", finalPreset).subscribe(
      (res: any) => {
        this.jobStatus = jobStatusEnum.DoneOK;
        this.resultMessage = res ? res.resultMessage : "";
      },
      (error) => {
        this.jobStatus = jobStatusEnum.DoneError;
        this.resultMessage = error.error.error;
      }
    );
  }
}
