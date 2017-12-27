import { Component, OnInit } from '@angular/core';
import { ElectronService } from 'ngx-electron';
import { HttpClient } from '@angular/common/http';
import { DataService } from './svc/data.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  constructor(private _electron: ElectronService, private _dataSvc: DataService) {
  }

  ngOnInit(): void {
    // this._dataSvc.getAll<any[]>()
    //   .subscribe(p => this.values = p);
  }

}
