import { Component, OnInit, ElementRef } from '@angular/core';
import { Preset, Keyword } from './preset';
import { element } from 'protractor';
import { DataService } from '../svc/data.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-create-template',
  templateUrl: './create-template.component.html',
  styleUrls: ['./create-template.component.css']
})
export class CreateTemplateComponent implements OnInit {

  isNew = true;
  preset: Preset;
  postOk: boolean;

  constructor(private _dataSvc: DataService, private _route: ActivatedRoute) {
    this.preset = {
      name: '',
      templateOrigin: '',
      fileTypesExtensions: '',
      keywords: [],
      autoUpdates: {}
    };
  }

  ngOnInit() {
    this._route.params.subscribe(prms => {
      const presetName = prms['name'];

      if (presetName) {
        this.isNew = false;
        this._dataSvc.getSingle<Preset>('presets', presetName)
          .subscribe(p => {
            this.preset = p;
          });
      }
    });
  }

  save() {
    if (this.isNew) {
      if (!this.preset.name) {
        return;
      }
      this._dataSvc.post<Preset>('presets', this.preset)
        .subscribe(p => this.postOk = true);
    } else {
      this._dataSvc.update<Preset>('presets', this.preset.name, this.preset)
        .subscribe(p => this.postOk = true);
    }
  }

  addKeyword(keywordValue: HTMLInputElement, keywordReplace: HTMLInputElement, keywordType: HTMLInputElement) {
    if (!keywordValue.value) {
      return;
    }
    const keyword = this.preset.keywords.find(p => p.keyword === keywordValue.value);
    if (keyword) {
      return;
    }

    this.preset.keywords.push({
      keyword: keywordValue.value,
      replacement: keywordReplace.value,
      type: keywordType.value
    });

    keywordValue.value = '';
    keywordReplace.value = '';
    keywordType.value = '';
  }

  removeKeyword(keyword: string) {
    const idx = this.preset.keywords.findIndex(p => p.keyword === keyword);
    this.preset.keywords.splice(idx, 1);
  }

}
