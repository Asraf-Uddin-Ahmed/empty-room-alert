import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';

/*
  Generated class for the RemoteServiceProvider provider.

  See https://angular.io/guide/dependency-injection for more info on providers
  and Angular DI.
*/
@Injectable()
export class RemoteServiceProvider {

  constructor(public http: Http) {
    console.log('Hello RemoteServiceProvider Provider');
  }

  apiBaseUrl: string = "http://localhost:5000/";

  get(endPoint) {
    return this.http.get(this.apiBaseUrl + endPoint).map(res => res.json());
  }
}
