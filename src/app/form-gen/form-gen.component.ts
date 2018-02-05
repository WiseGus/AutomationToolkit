import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';

import { WizardStep, FormGenInfo } from './wiz-model';

@Component({
  selector: 'app-form-gen',
  templateUrl: './form-gen.component.html',
  styleUrls: ['./form-gen.component.css']
})
export class FormGenComponent implements AfterViewInit {
  @ViewChild(WizardStep) currentWizStep: WizardStep;

  currentStep = 0;
  maxStep = 2;

  private _formGenInfo: FormGenInfo = {};

  constructor() { }

  ngAfterViewInit() {
    this.currentWizStep.init({
      formGenInfo: this._formGenInfo,
      data: null
    });
  }

  nextStep() {
    if (this.currentWizStep.isValid()) {
      this.currentWizStep.nextStep()
        .subscribe(p => {
          console.log('Result', p);

          if (this.currentStep === this.maxStep) {
            return;
          }
          this.currentStep++;
          setTimeout(() => {
            const x = this._formGenInfo;
            this.currentWizStep.init({
              formGenInfo: this._formGenInfo,
              data: p
            });
          }, 0);
        });
    }
  }
}
