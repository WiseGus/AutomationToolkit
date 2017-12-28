import { Component, OnInit } from '@angular/core';
import { DataService } from '../svc/data.service';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.css']
})
export class SettingsComponent implements OnInit {

  appSettings: any = {};
  postOk: boolean;

  constructor(private _dataSvc: DataService) { }

  ngOnInit() {
    this._dataSvc.getAll<any>('settings')
      .subscribe(p => {
        this.appSettings.tfsUrl = p.tfsUrl;
        this.appSettings.templatesFolderPath = p.templatesFolderPath;
      });
  }

  save() {
    this._dataSvc.post<any>('settings', this.appSettings)
      .subscribe(p => {
        this.postOk = true;
      });
  }

}
