import { Component, OnInit } from '@angular/core';
import { ApiService } from '../svc/api.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  templatesCount: number;

  constructor(private _api: ApiService) { }

  ngOnInit() {
    this._api.getAll<any[]>('presets')
      .subscribe(p => this.templatesCount = p.length);
  }

}
