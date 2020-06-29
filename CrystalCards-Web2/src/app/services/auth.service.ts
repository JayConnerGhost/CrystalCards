import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {ConfigService} from './config.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(
    public config: ConfigService,
    public http: HttpClient
  ) { }

  public isUserInDatabase(username: string){
    const path = `${this.config.master_apiURL}/auth/IsUserInSystem/${username}`;
    return this.http.get<boolean>(path);
  }

  register(value: any) {

  }
}
