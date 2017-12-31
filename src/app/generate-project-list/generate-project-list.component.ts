import { Component, OnInit } from '@angular/core';
import { DataService } from '../svc/data.service';
import { Preset } from '../create-template/preset';

@Component({
  selector: 'app-generate-project-list',
  templateUrl: './generate-project-list.component.html',
  styleUrls: ['./generate-project-list.component.css']
})
export class GenerateProjectListComponent implements OnInit {

  presets: Preset[] = [];

  constructor(private _dataSvc: DataService) { }

  ngOnInit() {
    this._dataSvc.getAll<Preset[]>('presets')
      .subscribe(p => {
        this.presets = p;
      });
  }

  delete(presetAlias: string) {
    this._dataSvc.delete('presets', presetAlias)
      .subscribe(p => {
        this.ngOnInit();
      });
  }

}
