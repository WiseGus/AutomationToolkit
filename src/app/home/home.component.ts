import { Component, OnInit } from '@angular/core';
import { DataService } from '../svc/data.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  templatesCount: number;

  constructor(private _dataSvc: DataService) { }

  ngOnInit() {
    this._dataSvc.getAll<string[]>('presets')
      .subscribe(p => this.templatesCount = p.length);
  }

}
