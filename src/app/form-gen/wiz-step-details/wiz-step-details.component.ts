import { Component, forwardRef } from '@angular/core';
import { Observable } from 'rxjs/Observable';

import { ApiService } from '../../svc/api.service';
import { DetailInfo, FormGenInfo, WizardStep } from '../wiz-model';

interface FormEditor {
  assignType: string;
  isDefaultForAssignType: boolean;
  editorName: string;
}

@Component({
  // tslint:disable-next-line:component-selector
  selector: 'wiz-step-details',
  templateUrl: './wiz-step-details.component.html',
  styleUrls: ['./wiz-step-details.component.css'],
  providers: [{ provide: WizardStep, useExisting: forwardRef(() => WizStepDetailsComponent) }]
})
export class WizStepDetailsComponent implements WizardStep {
  editorCategory = 'Glx';
  className: string;
  detailsInfo: DetailInfo[];
  allFormEditors: { category: string, data: FormEditor[] }[];
  filteredFormEditors: FormEditor[];

  private _formGenInfo: FormGenInfo;

  constructor(private _api: ApiService) { }

  init(args: { formGenInfo: FormGenInfo; data?: any; }) {
    this._formGenInfo = args.formGenInfo;
    this._api.getAll<any[]>('formeditors')
      .subscribe(p => {
        this.setupFormEditors(p);
        this.className = args.data.className;
        this.detailsInfo = args.data.dataSourceInfo;
        this.detailsInfo.forEach(x => {
          x.include = true;
          x.formEditor = this.getDefaultFormEditor(x);
        });
      });
  }

  isValid(): boolean {
    return true;
  }

  nextStep(): Observable<any> {
    this._formGenInfo.propertiesInfo = this.detailsInfo
      .filter(p => p.include && p.formEditor);
    return this._api.post<any>('formgen', this._formGenInfo);
  }

  private setupFormEditors(formEditors: any[]) {
    this.allFormEditors = [];

    const categories = new Set(formEditors.map(p => p.category));
    categories.forEach(category => {
      this.allFormEditors.push({
        category: category,
        data: this.getGroupedFormEditors(category, formEditors)
      });
    });

    this.filterFormEditors(this.editorCategory);
  }

  filterFormEditors(value: string) {
    this.filteredFormEditors = this.allFormEditors.find(p => p.category === value).data;

    if (this.detailsInfo) {
      this.detailsInfo.forEach(x => {
        x.formEditor = this.getDefaultFormEditor(x);
      });
    }
  }

  private getGroupedFormEditors(category: any, formEditors: any[]): FormEditor[] {
    const res: FormEditor[] = [];

    for (let i = 0; i < formEditors.length; i++) {
      if (formEditors[i].category === category) {
        res.push(formEditors[i]);
      }
    }

    return res;
  }

  private getDefaultFormEditor(detailInfo: DetailInfo): string {
    const defEditorForDataType = this.filteredFormEditors.find(p => p.assignType.indexOf(detailInfo.dataType) > -1 && p.isDefaultForAssignType);
    return defEditorForDataType ? defEditorForDataType.editorName : null;
  }

}
