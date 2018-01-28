import { Component, forwardRef, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';

import { ApiService } from '../../svc/api.service';
import { WizardStep } from '../wizard-step';

interface FormEditor {
  assignType: string;
  isDefaultForAssignType: boolean;
  editorName: string;
}

interface DetailInfo {
  name: string;
  caption: string;
  dataType: string;
  include: boolean;
  formEditor: string;
}

@Component({
  // tslint:disable-next-line:component-selector
  selector: 'wiz-step-details',
  templateUrl: './wiz-step-details.component.html',
  styleUrls: ['./wiz-step-details.component.css'],
  providers: [{ provide: WizardStep, useExisting: forwardRef(() => WizStepDetailsComponent) }]
})
export class WizStepDetailsComponent implements OnInit, WizardStep {
  editorCategory = 'Glx';
  className: string;
  detailsInfo: DetailInfo[];
  allFormEditors: { category: string, data: FormEditor[] }[];
  filteredFormEditors: FormEditor[];

  constructor(private _api: ApiService) { }

  ngOnInit() { }

  init(args: any) {
    this._api.getAll<any[]>('formeditors')
      .subscribe(p => {
        this.setupFormEditors(p);
        this.className = args.className;
        this.detailsInfo = args.dataSourceInfo;
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
    return Observable.create(observer => {
      console.log('NextStep', this.detailsInfo);
    });
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
