import { Component, OnInit } from "@angular/core";
import { ApiService } from "../svc/api.service";
import { AppSettings } from "./appsettings-model";

@Component({
  selector: "app-settings",
  templateUrl: "./settings.component.html",
  styleUrls: ["./settings.component.css"],
})
export class SettingsComponent implements OnInit {
  appSettings: AppSettings[] = [];
  postOk: boolean;

  constructor(private _api: ApiService) {}

  ngOnInit() {
    this._api
      .getAll<AppSettings[]>("settings")
      .subscribe((p) => (this.appSettings = p));
  }

  save() {
    this._api
      .post<any>("settings", this.appSettings)
      .subscribe((p) => (this.postOk = true));
  }
}
