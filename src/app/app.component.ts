import { Component, OnInit } from '@angular/core';
import { ElectronService } from 'ngx-electron';
import { DataService } from './svc/data.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  serverRunning = false;

  constructor(private _electron: ElectronService, private _dataSvc: DataService) {
  }

  ngOnInit() {
    this._dataSvc.getAll<any>('settings/ping')
      .subscribe(p => this.serverRunning = true);
  }

}
