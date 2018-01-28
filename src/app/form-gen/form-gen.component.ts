import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';

import { WizardStep } from './wizard-step';

@Component({
  selector: 'app-form-gen',
  templateUrl: './form-gen.component.html',
  styleUrls: ['./form-gen.component.css']
})
export class FormGenComponent implements OnInit, AfterViewInit {
  @ViewChild(WizardStep) currentWizStep: WizardStep;

  currentStep = 0;
  maxStep = 1;

  ngOnInit() { }

  ngAfterViewInit() {
    this.currentWizStep.init();
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
          setTimeout(() => this.currentWizStep.init(p), 0);
        });
    }
  }
}
