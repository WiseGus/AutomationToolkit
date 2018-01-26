import { HttpClient } from '@angular/common/http';
import { Component, forwardRef, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgbTabset } from '@ng-bootstrap/ng-bootstrap';
import { ElectronService } from 'ngx-electron';
import { Observable } from 'rxjs/Observable';
import { of } from 'rxjs/observable/of';

import { WizardStep } from '../wizard-step';

const PROMISE = Promise.resolve(null);

interface FormEditor {
  assignType: 'Text' | 'Guid' | 'Memo' | 'Number' | 'Date';
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
export class WizStepDetailsComponent implements OnInit, WizardStep {
  args: any;
  @ViewChild(NgbTabset) tabSet: NgbTabset;

  formEditors: FormEditor[];

  private _isElectronApp: boolean;

  constructor(electron: ElectronService, private _http: HttpClient, private _route: ActivatedRoute) {
    this._isElectronApp = electron.isElectronApp;
  }

  ngOnInit() {
    this.fillEditors();
  }

  init(args: any) {
    PROMISE.then(() => this.args = args);
  }

  isValid(): boolean {
    return true;
  }

  previousStep() { }

  nextStep(): Observable<any> {
    return of(null);
  }

  private fillEditors() {
    this._http.get(this.generateUrl('formeditors'))
      .subscribe(p => {
        this.formEditors = <any>p;
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
