import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { NgxElectronModule } from 'ngx-electron';
import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { HttpClientModule } from '@angular/common/http';
import { DataService } from './svc/data.service';
import { CreateTemplateComponent } from './create-template/create-template.component';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { SettingsComponent } from './settings/settings.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { GenerateProjectListComponent } from './generate-project-list/generate-project-list.component';
import { GenerateProjectComponent } from './generate-project/generate-project.component';

const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: 'home', component: HomeComponent },
  { path: 'generateproject/:name', component: GenerateProjectComponent },
  { path: 'generateprojectlist', component: GenerateProjectListComponent },
  { path: 'createtemplate/:name', component: CreateTemplateComponent },
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
    GenerateProjectComponent
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
    DataService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
