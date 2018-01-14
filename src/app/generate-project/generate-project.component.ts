import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ApiService } from '../svc/api.service';
import { Preset, Keyword } from '../create-template/preset';
import { DisableElementDirective } from '../directives/disable-element.directive';

export enum jobStatusEnum { None, Working, DoneOK, DoneError }

@Component({
  selector: 'app-generate-project',
  templateUrl: './generate-project.component.html',
  styleUrls: ['./generate-project.component.css']
})
export class GenerateProjectComponent implements OnInit {
  jobStatusEnum = jobStatusEnum;

  preset: Preset;
  keywords: Keyword[];
  showDetails = false;
  jobStatus: jobStatusEnum = jobStatusEnum.None;
  resultMessage: string;
  useAutomationUpdates: boolean;

  constructor(private _api: ApiService, private _route: ActivatedRoute) { }

  ngOnInit() {
    this._route.params.subscribe(prms => {
      const presetAlias = prms['alias'];
      this._api.getSingle<Preset>('presets', presetAlias)
        .subscribe(p => {
          this.preset = p;
          this.keywords = this.preset.keywords.filter(x => x.showInGenerate);
          this.useAutomationUpdates = (<any>p).automationUpdates.useAutomationUpdates;
        });
    });
  }

  toggleShowDetails() {
    this.showDetails = !this.showDetails;
  }

  generateProject() {
    this.jobStatus = jobStatusEnum.Working;
    (<any>this.preset).automationUpdates.useAutomationUpdates = this.useAutomationUpdates;
    console.log('Generating template: ', this.preset);
    this._api.post('generateprojects', this.preset)
      .subscribe(
      (res: any) => {
        this.jobStatus = jobStatusEnum.DoneOK;
        this.resultMessage = res ? res.resultMessage : '';
      },
      error => {
        this.jobStatus = jobStatusEnum.DoneError;
        this.resultMessage = error.error.error;
      });
  }

}
