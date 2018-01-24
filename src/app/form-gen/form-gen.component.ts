import { HttpClient } from '@angular/common/http';
import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { NgbTabset } from '@ng-bootstrap/ng-bootstrap/tabset/tabset';
import { ElectronService } from 'ngx-electron';

@Component({
  selector: 'app-form-gen',
  templateUrl: './form-gen.component.html',
  styleUrls: ['./form-gen.component.css']
})
export class FormGenComponent implements OnInit {

  @ViewChild(NgbTabset) tabSet: NgbTabset;

  wizStep = 0;
  pocoInfo: any;
  data = {
    asmPath: 'C:\\Users\\ksofos\\Documents\\Visual Studio 2017\\Projects\\AutomationToolkit\\Api.Tests\\bin\\Debug\\netcoreapp2.0\\Api.Tests.dll',
    fullName: 'Api.Tests.DummyModel',
    tableXmlPath: 'C:\\ksofos\\Development\\CrmNet\\Baseline\\Sources\\Schema\\slsSchemaTable.Files\\cmContacts.xml'
  };

  private _isElectronApp: boolean;

  constructor(electron: ElectronService, private _http: HttpClient, private _route: ActivatedRoute) {
    this._isElectronApp = electron.isElectronApp;
  }

  ngOnInit() {
  }

  previousStep() {
    if (this.wizStep === 0) {
      return;
    }
    this.wizStep--;
  }

  nextStep(frm: NgForm) {
    if (!frm.valid) {
      alert('Invalid values found');
      return;
    }
    this.wizStep++;
    this.performStep(frm);
  }

  private performStep(frm: NgForm) {
    if (this.wizStep === 1) {

      let url: string;
      let params: {
        [param: string]: string | string[];
      };
      let getCallback;

      if (this.tabSet.activeId === 'schemaTab') {
        const classNameSplit = frm.value.tableXmlPath.split('\\') as string[];

        url = 'formgen/pocoinfosch';
        params = {
          'tableXmlPath': frm.value.tableXmlPath
        };
        getCallback = (res) => {
          this.pocoInfo = {
            className: classNameSplit[classNameSplit.length - 1].replace('.xml', ''),
            dataSourceInfo: res
          };
        };
      } else {
        const classFullNameSplit = frm.value.classFullName.split('.');

        url = 'formgen/pocoinfoasm';
        params = {
          'assemblyPath': frm.value.assemblyPath,
          'classFullName': frm.value.classFullName
        };
        getCallback = (res) => {
          this.pocoInfo = {
            className: classFullNameSplit[classFullNameSplit.length - 1],
            dataSourceInfo: res
          };
        };
      }

      this._http.get(this.generateUrl(url), { params: params })
        .subscribe(p => getCallback(p));
    }
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
