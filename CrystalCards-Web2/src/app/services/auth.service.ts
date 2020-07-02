import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {ConfigService} from './config.service';
import {User} from '../user';

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

 public register(FirstName: string, SecondName: string, Username: string, Password: string) {
  const user = new User(FirstName, SecondName, Username, Password);
  const path = `${this.config.master_apiURL}/auth/register`;
  return this.http.post(path, user);
  }
}
