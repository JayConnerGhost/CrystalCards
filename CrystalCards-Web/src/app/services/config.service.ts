import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ConfigService {

  constructor() { }
  Url:string ='http://localhost:50873';
  Url2:string ='http://localhost:55265';
  Url3:string ='http://www.katiekatcoder.co.uk/api';


  apiURL: string = `${this.Url}/api`;
  apiURL2: string = `${this.Url2}/api`;
  apiUR_staging: string = `${this.Url3}/api`;

  public master_apiURL: string =`${this.apiURL}`;
  public Resources: string=`${this.Url}/Resources`;
  public Images: string=`${this.Resources}/Images`;

}
