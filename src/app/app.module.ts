import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule, Routes } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NgxElectronModule } from 'ngx-electron';

import { AppComponent } from './app.component';
import { CreateTemplateComponent } from './create-template/create-template.component';
import { DisableElementDirective } from './directives/disable-element.directive';
import { GenerateProjectListComponent } from './generate-project-list/generate-project-list.component';
import { GenerateProjectComponent } from './generate-project/generate-project.component';
import { HomeComponent } from './home/home.component';
import { SettingsComponent } from './settings/settings.component';
import { ApiService } from './svc/api.service';

const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: 'home', component: HomeComponent },
  { path: 'generateproject/:alias', component: GenerateProjectComponent },
  { path: 'generateprojectlist', component: GenerateProjectListComponent },
  { path: 'createtemplate/:alias', component: CreateTemplateComponent },
  { path: 'createtemplate', component: CreateTemplateComponent },
  { path: 'settings', component: SettingsComponent },
  { path: '**', component: HomeComponent }
];

@NgModule({
  declarations: [
    AppComponent,
    CreateTemplateComponent,
    HomeComponent,
    SettingsComponent,
    GenerateProjectListComponent,
    GenerateProjectComponent,
    DisableElementDirective
  ],
  imports: [
    FormsModule,
    ReactiveFormsModule,
    BrowserModule,
    RouterModule.forRoot(routes),
    NgxElectronModule,
    HttpClientModule,
    NgbModule.forRoot()
  ],
  providers: [
    ApiService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
