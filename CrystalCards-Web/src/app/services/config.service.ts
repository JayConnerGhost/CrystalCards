import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ConfigService {

  constructor() { }
  Url:string ='http://localhost:50872';
  Url2:string ='http://localhost:54891';
  Url3:string ='http://www.katiekatcoder.co.uk/api';


  apiURL: string = `${this.Url}/api`;
  apiURL2: string = `${this.Url2}/api`;
  apiUR_staging: string = `${this.Url3}/api`;

  public master_apiURL: string =`${this.apiUR_staging}`;
  public Resources: string=`${this.Url3}/Resources`;
  public Images: string=`${this.Resources}/Images`;

}
