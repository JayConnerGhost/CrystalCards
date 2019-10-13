import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http/';
import { ConfigService } from './config.service';
import { map } from 'rxjs/operators';
import {JwtHelperService} from '@auth0/angular-jwt';
@Injectable({
  providedIn: 'root'
})
export class AuthService {
baseUrl = `${this.config.master_apiURL}/auth/`;
jwtHelper = new JwtHelperService();
decodedToken: any;
  constructor(private http:HttpClient, private config:ConfigService) { }




  register(model:any){
    return this.http.post(this.baseUrl+'register',model);
  }

  loggedIn(){
    const token=localStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token);
  }

  login(model:any){
    return this.http.post(this.baseUrl+'login', model)
    .pipe(
        map((response:any)=>{
          const user=response;
          if(user){
            localStorage.setItem('token', user.token);
            this.decodedToken=this.jwtHelper.decodeToken(user.token);
            console.log(this.decodedToken);
          }
        }
    ));
  }
}
