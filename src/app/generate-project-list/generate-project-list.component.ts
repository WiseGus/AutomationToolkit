import { Component, OnInit } from '@angular/core';
import { ApiService } from '../svc/api.service';
import { Preset } from '../create-template/preset';

@Component({
  selector: 'app-generate-project-list',
  templateUrl: './generate-project-list.component.html',
  styleUrls: ['./generate-project-list.component.css']
})
export class GenerateProjectListComponent implements OnInit {

  presetsWithCategories: { category: string, presets: Preset[] }[] = [];
  tooltip: { title: string, body: string };

  constructor(private _api: ApiService) { }

  ngOnInit() {
    this._api.getAll<Preset[]>('presets')
      .subscribe(presets => {
        const categories = new Set(presets.map(x => x.aliasCategory));
        categories.forEach(category => {
          const categoryPresets = presets.filter(p => p.aliasCategory === category);
          this.presetsWithCategories.push({
            category: category,
            presets: categoryPresets
          });
        });
      });
  }

  delete(presetAlias: string) {
    this._api.delete('presets', presetAlias)
      .subscribe(p => {
        this.ngOnInit();
      });
  }

}
