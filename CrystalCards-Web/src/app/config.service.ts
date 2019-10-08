import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ConfigService {

  constructor() { }
  apiURL: string = 'http://localhost:50872/api';
  apiURL2: string = "http://localhost:55265/api";
  apiUR_staging: string = "https://crystalcardsapi20191004033914.azurewebsites.net/api";

  public master_apiURL: string =`${this.apiURL}`;
  public Resources: string=`/${this.master_apiURL}/Resources`;
  public Images: string=`${this.Resources}/Images`;

}
