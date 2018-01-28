import { HttpClient } from '@angular/common/http';
import { Component, forwardRef, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { NgbTabset } from '@ng-bootstrap/ng-bootstrap';
import { ElectronService } from 'ngx-electron';
import { Observable } from 'rxjs/Observable';

import { WizardStep } from '../wizard-step';

const PROMISE = Promise.resolve(null);

@Component({
  // tslint:disable-next-line:component-selector
  selector: 'wiz-step-preparation',
  templateUrl: './wiz-step-preparation.component.html',
  styleUrls: ['./wiz-step-preparation.component.css'],
  providers: [{ provide: WizardStep, useExisting: forwardRef(() => WizStepPreparationComponent) }]
})
export class WizStepPreparationComponent implements OnInit, WizardStep {
  args: {
    asmPath: string,
    fullName: string,
    tableXmlPath: string
  };
  @ViewChild(NgForm) frm: NgForm;
  @ViewChild(NgbTabset) tabSet: NgbTabset;

  private _isElectronApp: boolean;

  constructor(electron: ElectronService, private _http: HttpClient, private _route: ActivatedRoute) {
    this._isElectronApp = electron.isElectronApp;
    this.args = {
      asmPath: 'D:\\WebProjects\\AutomationToolkit\\Api.Tests\\bin\\Debug\\netcoreapp2.0\\Api.Tests.dll',
      fullName: 'Api.Tests.DummyModel',
      tableXmlPath: 'C:\\ksofos\\Development\\CrmNet\\Baseline\\Sources\\Schema\\slsSchemaTable.Files\\cmContacts.xml'
    };
  }

  ngOnInit() { }

  init(args?: any) { }

  isValid(): boolean {
    if (this.tabSet.activeId === 'schemaTab') {
      return this.frm.value.tableXmlPath;
    } else {
      return (this.frm.value.assemblyPath && this.frm.value.classFullName);
    }
  }

  nextStep(): Observable<any> {
    return Observable.create(observer => {
      console.log('NextStep', this.frm);

      let url: string;
      let params: {
        [param: string]: string | string[];
      };
      let callback;

      if (this.tabSet.activeId === 'schemaTab') {
        const classNameSplit = this.frm.value.tableXmlPath.split('\\') as string[];

        url = 'formgen/pocoinfosch';
        params = {
          'tableXmlPath': this.frm.value.tableXmlPath
        };
        callback = (res) => {
          observer.next({
            className: classNameSplit[classNameSplit.length - 1].replace('.xml', ''),
            dataSourceInfo: res
          });
        };
      } else {
        const classFullNameSplit = this.frm.value.classFullName.split('.');

        url = 'formgen/pocoinfoasm';
        params = {
          'assemblyPath': this.frm.value.assemblyPath,
          'classFullName': this.frm.value.classFullName
        };
        callback = (res) => {
          observer.next({
            className: classFullNameSplit[classFullNameSplit.length - 1],
            dataSourceInfo: res
          });
        };
      }

      this._http.get(this.generateUrl(url), { params: params })
        .subscribe(p => callback(p));
    });
  }

  private generateUrl(url: string) {
    let apiUrl = `api/${url}`;
    if (this._isElectronApp
    ) {
      apiUrl = `http://localhost:5000/${apiUrl}`;
    }
    return apiUrl;
  }
}
