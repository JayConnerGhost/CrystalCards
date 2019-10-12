import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http/';
import { ConfigService } from './config.service';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
baseUrl = `${this.config.apiURL}/auth/`;
  constructor(private http:HttpClient, private config:ConfigService) { }

  login(model:any){
    return this.http.post(this.baseUrl+'login', model)
    .pipe(
        map((response:any)=>{
          const user=response;
          if(user){
            localStorage.setItem('token', user.token);
          }
        }
    ));
  }
}
