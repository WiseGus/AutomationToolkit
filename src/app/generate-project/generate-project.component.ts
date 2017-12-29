import { Component, OnInit } from '@angular/core';
import { DataService } from '../svc/data.service';
import { Preset } from '../create-template/preset';

@Component({
  selector: 'app-generate-project',
  templateUrl: './generate-project.component.html',
  styleUrls: ['./generate-project.component.css']
})
export class GenerateProjectComponent implements OnInit {

  presets: Preset[] = [];

  constructor(private _dataSvc: DataService) { }

  ngOnInit() {
    this._dataSvc.getAll<Preset[]>('presets')
      .subscribe(p => {
        this.presets = p;
      });
  }

  generate(preset: Preset) { }

  delete(presetName: string) {
    this._dataSvc.delete('presets', presetName)
      .subscribe(p => {
        this.ngOnInit();
      });
  }

}
