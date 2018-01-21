import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

import { ApiService } from '../svc/api.service';
import { HttpClient } from '@angular/common/http';
import { ElectronService } from 'ngx-electron';

@Component({
  selector: 'app-form-gen',
  templateUrl: './form-gen.component.html',
  styleUrls: ['./form-gen.component.css']
})
export class FormGenComponent implements OnInit {

  wizStep = 0;
  pocoInfo: any;

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
      const classFullNameSplit = frm.value.classFullName.split('.');
      this._http.get(this.generateUrl('formgen/pocoinfo'), {
        params: {
          'assemblyPath': frm.value.assemblyPath,
          'classFullName': frm.value.classFullName
        }
      })
        .subscribe(p => {
          this.pocoInfo = {
            className: classFullNameSplit[classFullNameSplit.length - 1],
            dataSourceInfo: p
          };
        });
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
