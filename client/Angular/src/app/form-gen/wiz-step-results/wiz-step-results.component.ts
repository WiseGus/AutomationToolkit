import { Component, OnInit, forwardRef } from '@angular/core';
import { WizardStep, FormGenInfo } from '../wiz-model';
import { Observable } from 'rxjs/Observable';

@Component({
  selector: 'app-wiz-step-results',
  templateUrl: './wiz-step-results.component.html',
  styleUrls: ['./wiz-step-results.component.css'],
  providers: [{ provide: WizardStep, useExisting: forwardRef(() => WizStepResultsComponent) }]
})
export class WizStepResultsComponent implements WizardStep {
  designerData: string;

  private _formGenInfo: FormGenInfo;

  constructor() { }

  init(args: { formGenInfo: FormGenInfo; data?: any; }) {
    this._formGenInfo = args.formGenInfo;
    this.designerData = args.data;
  }

  isValid(): boolean {
    throw new Error('Method not implemented.');
  }

  nextStep(): Observable<any> {
    throw new Error('Method not implemented.');
  }
}
