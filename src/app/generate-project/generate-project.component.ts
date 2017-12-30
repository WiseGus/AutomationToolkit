import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DataService } from '../svc/data.service';
import { Preset, Keyword } from '../create-template/preset';

@Component({
  selector: 'app-generate-project',
  templateUrl: './generate-project.component.html',
  styleUrls: ['./generate-project.component.css']
})
export class GenerateProjectComponent implements OnInit {

  preset: Preset;
  keywords: Keyword[];
  showDetails = false;

  constructor(private _dataSvc: DataService, private _route: ActivatedRoute) { }

  ngOnInit() {
    this._route.params.subscribe(prms => {
      const presetName = prms['name'];
      this._dataSvc.getSingle<Preset>('presets', presetName)
        .subscribe(p => {
          this.preset = p;
          this.keywords = this.preset.keywords.filter(x => !x.replacement);
        });
    });
  }

  toggleShowDetails() {
    this.showDetails = !this.showDetails;
  }

  generateProject() {
    if (!this.preset.projectName || this.preset.keywords.filter(p => !p.replacement).length > 0) {
      return;
    }

    console.log('Generating template: ', this.preset);
    this._dataSvc.post('generateprojects', this.preset)
      .subscribe(p => { });
  }

  updateProjectName(value: string) {
    this.preset.name = value;
  }

}
