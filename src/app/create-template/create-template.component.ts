import { Component, OnInit, ElementRef } from '@angular/core';
import { Preset, Keyword } from './preset';
import { element } from 'protractor';

@Component({
  selector: 'app-create-template',
  templateUrl: './create-template.component.html',
  styleUrls: ['./create-template.component.css']
})
export class CreateTemplateComponent implements OnInit {

  preset: Preset;

  constructor() {
    this.preset = {
      name: '',
      templateOrigin: '',
      fileTypesExtensions: '',
      keywords: [{ keyword: '$EntityName$', replacement: 'Glx.ENT.IS', type: 'text' }],
      autoUpdates: {}
    };
  }

  ngOnInit() {
  }

  save() {
    console.log(this.preset);
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
