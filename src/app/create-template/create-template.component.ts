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
      alias: '',
      aliasCategory: '',
      projectName: '',
      templateOrigin: '',
      outputFolderPath: '',
      fileKeywordTypesExtensions: '',
      keywords: [],
      addToSourceControl: true,
      useAutomationUpdates: false
    };
  }

  ngOnInit() {
    this._route.params.subscribe(prms => {
      const presetAlias = prms['alias'];

      if (presetAlias) {
        this.isNew = false;
        this._dataSvc.getSingle<Preset>('presets', presetAlias)
          .subscribe(p => {
            this.preset = p;
            this.preset.useAutomationUpdates = (<any>p).automationUpdates.useAutomationUpdates;
            console.log('Preset: ', p);

          });
      }
    });
  }

  save() {
    if (this.isNew) {
      if (!this.preset.alias) {
        return;
      }

      console.log('Posting preset: ', this.preset);
      this._dataSvc.post<Preset>('presets', this.preset)
        .subscribe(p => this.postOk = true);
    } else {
      console.log('Updating preset: ', this.preset);
      this._dataSvc.update<Preset>('presets', this.preset.alias, this.preset)
        .subscribe(p => this.postOk = true);
    }
  }

  addKeyword(keywordValue: HTMLInputElement, keywordReplace: HTMLInputElement, keywordType: HTMLInputElement, keywordShowInGenerate: HTMLInputElement) {
    if (!keywordValue.value) {
      return;
    }
    const keyword = this.preset.keywords.find(p => p.keywordName === keywordValue.value);
    if (keyword) {
      return;
    }

    this.preset.keywords.push({
      keywordName: keywordValue.value,
      replacement: keywordReplace.value,
      keywordType: keywordType.value,
      showInGenerate: keywordShowInGenerate.checked
    });

    keywordValue.value = '';
    keywordReplace.value = '';
    keywordType.value = 'text';
    keywordShowInGenerate.checked = false;
  }

  removeKeyword(keyword: string) {
    const idx = this.preset.keywords.findIndex(p => p.keywordName === keyword);
    this.preset.keywords.splice(idx, 1);
  }

}
