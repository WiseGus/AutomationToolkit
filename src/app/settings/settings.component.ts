import { Component, OnInit } from '@angular/core';
import { ApiService } from '../svc/api.service';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.css']
})
export class SettingsComponent implements OnInit {

  appSettings: any = {};
  postOk: boolean;

  constructor(private _api: ApiService) { }

  ngOnInit() {
    this._api.getAll<any>('settings')
      .subscribe(p => {
        this.appSettings.tfsUrl = p.tfsUrl;
        this.appSettings.tfsWorkspace = p.tfsWorkspace;
        this.appSettings.rootSourcesPath = p.rootSourcesPath;
        this.appSettings.tfPath = p.tfPath;
        this.appSettings.debugMode = p.debugMode;
      });
  }

  save() {
    this._api.post<any>('settings', this.appSettings)
      .subscribe(p => this.postOk = true);
  }

}
