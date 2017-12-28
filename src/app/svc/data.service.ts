import { HttpClient, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Configuration } from '../conf/configuration';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';

@Injectable()
export class DataService {

  private actionUrl: string;

  constructor(private http: HttpClient, private _configuration: Configuration) {
  }

  public getAll<T>(controller: string): Observable<T> {
    return this.http.get<T>(this.generateUrl(controller));
  }

  public getSingle<T>(controller: string, id: number): Observable<T> {
    return this.http.get<T>(this.generateUrl(controller) + id);
  }

  public post<T>(controller: string, itemToUpdate: any): Observable<T> {
    return this.http
      .post(this.generateUrl(controller), itemToUpdate) as Observable<T>;
  }

  public update<T>(controller: string, id: number, itemToUpdate: any): Observable<T> {
    return this.http
      .put<T>(this.generateUrl(controller) + id, JSON.stringify(itemToUpdate));
  }

  public delete<T>(controller: string, id: number): Observable<T> {
    return this.http.delete<T>(this.generateUrl(controller) + id);
  }

  private generateUrl(controller: string) {
    return this._configuration.ServerWithApiUrl + controller + '/';
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
