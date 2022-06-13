import { Component, OnInit } from '@angular/core';
import { ElectronService } from 'ngx-electron';
import { ApiService } from './svc/api.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  serverRunning = false;

  constructor(private _electron: ElectronService, private _api: ApiService) {
  }

  ngOnInit() {
    this._api.getAll<any>('settings/ping')
      .subscribe(p => this.serverRunning = true);
  }

}
