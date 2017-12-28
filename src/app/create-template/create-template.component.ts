import { Component, OnInit } from '@angular/core';
import { Preset } from './preset';

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
      fileTypesExtensions: [],
      keywords: new Map<string, string>(),
      autoUpdates: {}
    };
  }

  ngOnInit() {
  }

  save() {
    console.log(this.preset);
  }

  addFileTypeExtension(text: string) {
    if (!text) {
      return;
    }
    const idx = this.preset.fileTypesExtensions.indexOf(text);
    if (idx > -1) {
      return;
    }

    this.preset.fileTypesExtensions.push(text);
  }

  removeFileTypeExtension(keyword: string) {
    const idx = this.preset.fileTypesExtensions.indexOf(keyword);
    this.preset.fileTypesExtensions.splice(idx, 1);
  }

}
