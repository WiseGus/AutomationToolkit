import { EventEmitter } from '@angular/core';
import { Observable } from 'rxjs/Observable';

export abstract class WizardStep {
  abstract init(args: any);
  abstract isValid(): boolean;
  abstract previousStep(): void;
  abstract nextStep(): Observable<any>;
}