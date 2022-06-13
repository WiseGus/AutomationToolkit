import { HttpClient, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { ElectronService } from 'ngx-electron';

@Injectable()
export class ApiService {

  private actionUrl: string;
  private _isElectronApp: boolean;

  constructor(electron: ElectronService, private http: HttpClient) {
    this._isElectronApp = electron.isElectronApp;
  }

  public getAll<T>(controller: string): Observable<T> {
    return this.http.get<T>(this.generateUrl(controller));
  }

  public getSingle<T>(controller: string, id: string): Observable<T> {
    return this.http.get<T>(this.generateUrl(controller) + id);
  }

  public post<T>(controller: string, itemToUpdate: any): Observable<T> {
    return this.http
      .post(this.generateUrl(controller), itemToUpdate) as Observable<T>;
  }

  public update<T>(controller: string, id: string, itemToUpdate: any): Observable<T> {
    return this.http
      .put<T>(this.generateUrl(controller) + id, itemToUpdate);
  }

  public delete<T>(controller: string, id: string): Observable<T> {
    return this.http.delete<T>(this.generateUrl(controller) + id);
  }

  public generateUrl(controller: string) {
    let apiUrl = `api/${controller}/`;
    if (this._isElectronApp) {
      apiUrl = `http://localhost:5000/${apiUrl}`;
    }
    return apiUrl;
  }
}


@Injectable()
export class CustomInterceptor implements HttpInterceptor {

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (!req.headers.has('Content-Type')) {
      req = req.clone({ headers: req.headers.set('Content-Type', 'application/json') });
    }

    req = req.clone({ headers: req.headers.set('Accept', 'application/json') });
    console.log(JSON.stringify(req.headers));
    return next.handle(req);
  }
}
