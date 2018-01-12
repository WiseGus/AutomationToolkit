import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DataService } from '../svc/data.service';
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

  constructor(private _dataSvc: DataService, private _route: ActivatedRoute) { }

  ngOnInit() {
    this._route.params.subscribe(prms => {
      const presetAlias = prms['alias'];
      this._dataSvc.getSingle<Preset>('presets', presetAlias)
        .subscribe(p => {
          this.preset = p;
          this.keywords = this.preset.keywords.filter(x => x.showInGenerate);
        });
    });
  }

  toggleShowDetails() {
    this.showDetails = !this.showDetails;
  }

  generateProject() {
    this.jobStatus = jobStatusEnum.Working;
    console.log('Generating template: ', this.preset);
    this._dataSvc.post('generateprojects', this.preset)
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
