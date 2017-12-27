import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { NgxElectronModule } from 'ngx-electron';
import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { HttpClientModule } from '@angular/common/http';
import { DataService } from './svc/data.service';
import { Configuration } from './conf/configuration';
import { CreateTemplateComponent } from './create-template/create-template.component';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';

const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: 'home', component: HomeComponent },
  { path: 'create-template', component: CreateTemplateComponent },
  { path: '**', component: HomeComponent }
];

@NgModule({
  declarations: [
    AppComponent,
    CreateTemplateComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule,
    RouterModule.forRoot(routes),
    NgxElectronModule,
    HttpClientModule,
    NgbModule.forRoot()
  ],
  providers: [
    DataService,
    Configuration
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
