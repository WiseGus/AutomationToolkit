import { EventEmitter } from '@angular/core';
import { Observable } from 'rxjs/Observable';

export abstract class WizardStep {
  abstract init(args: { formGenInfo: FormGenInfo, data?: any });
  abstract isValid(): boolean;
  abstract nextStep(): Observable<any>;
}

export interface DetailInfo {
  name: string;
  caption: string;
  dataType: string;
  include: boolean;
  formEditor: string;
}

export interface FormGenInfo {
  isGlxSchema?: boolean;
  tableXmlPath?: string;
  assemblyPath?: string;
  classFullName?: string;
  propertiesInfo?: DetailInfo[];
}
