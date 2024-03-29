import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Component, forwardRef, ViewChild } from "@angular/core";
import { NgForm } from "@angular/forms";
import { NgbTabset } from "@ng-bootstrap/ng-bootstrap";
import { ElectronService } from "ngx-electron";
import { Observable } from "rxjs/Observable";

import { FormGenInfo, WizardStep } from "../wiz-model";

const PROMISE = Promise.resolve(null);

@Component({
  // tslint:disable-next-line:component-selector
  selector: "wiz-step-preparation",
  templateUrl: "./wiz-step-preparation.component.html",
  styleUrls: ["./wiz-step-preparation.component.css"],
  providers: [
    {
      provide: WizardStep,
      useExisting: forwardRef(() => WizStepPreparationComponent),
    },
  ],
})
export class WizStepPreparationComponent implements WizardStep {
  args: {
    asmPath: string;
    fullName: string;
    tableXmlPath: string;
  };
  @ViewChild(NgForm) frm: NgForm;
  @ViewChild(NgbTabset) tabSet: NgbTabset;

  private _isElectronApp: boolean;
  private _formGenInfo: FormGenInfo;

  constructor(electron: ElectronService, private _http: HttpClient) {
    this._isElectronApp = electron.isElectronApp;
    this.args = {
      asmPath:
        "C:\\Users\\ksofos\\Documents\\Visual Studio 2017\\Projects\\AutomationToolkit\\Api.Tests\\bin\\Debug\\netcoreapp2.0\\Api.Tests.dll",
      fullName: "Api.Tests.DummyModel",
      tableXmlPath:
        "C:\\ksofos\\Development\\CrmNet\\Baseline\\Sources\\Schema\\slsSchemaTable.Files\\cmContacts.xml",
    };
  }

  init(args: { formGenInfo: FormGenInfo; data?: any }) {
    this._formGenInfo = args.formGenInfo;
  }

  isValid(): boolean {
    if (this.tabSet.activeId === "schemaTab") {
      return this.frm.value.tableXmlPath;
    } else {
      return this.frm.value.assemblyPath && this.frm.value.classFullName;
    }
  }

  nextStep(): Observable<any> {
    return Observable.create((observer) => {
      console.log("NextStep", this.frm);

      const url = "formgen";
      let params: {
        [param: string]: string | string[];
      };
      let callback;

      if (this.tabSet.activeId === "schemaTab") {
        const classNameSplit = this.frm.value.tableXmlPath.split(
          "\\"
        ) as string[];

        params = {
          isGlxSchema: "true",
          tableXmlPath: this.frm.value.tableXmlPath,
        };
        this._formGenInfo.isGlxSchema = true;
        this._formGenInfo.tableXmlPath = this.frm.value.tableXmlPath;

        callback = (res) => {
          observer.next({
            className: classNameSplit[classNameSplit.length - 1].replace(
              ".xml",
              ""
            ),
            dataSourceInfo: res,
          });
        };
      } else {
        const classFullNameSplit = this.frm.value.classFullName.split(".");

        params = {
          isGlxSchema: "false",
          assemblyPath: this.frm.value.assemblyPath,
          classFullName: this.frm.value.classFullName,
        };
        this._formGenInfo.isGlxSchema = false;
        this._formGenInfo.assemblyPath = this.frm.value.assemblyPath;
        this._formGenInfo.classFullName = this.frm.value.classFullName;

        callback = (res) => {
          observer.next({
            className: classFullNameSplit[classFullNameSplit.length - 1],
            dataSourceInfo: res,
          });
        };
      }

      const headers = new HttpHeaders({
        "Content-Type": "application/json",
        Accept: "application/json",
      });

      this._http
        .get(this.generateUrl(url), { headers: headers, params: params })
        .subscribe((p) => callback(p));
    });
  }

  private generateUrl(url: string) {
    let apiUrl = `api/${url}`;
    if (this._isElectronApp) {
      apiUrl = `http://localhost:5000/${apiUrl}`;
    }
    return apiUrl;
  }
}
