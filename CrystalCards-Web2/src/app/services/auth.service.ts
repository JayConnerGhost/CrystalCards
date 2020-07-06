import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {ConfigService} from './config.service';
import {user} from '../user';
import {LoginRequest} from '../loginRequest';
import {map} from 'rxjs/operators';
import {LoginResponse} from '../loginResponse';
import {JwtHelperService} from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  jwtHelper = new JwtHelperService();
  private decodedToken: any;

  constructor(
    public config: ConfigService,
    public http: HttpClient

  ) { }

  public isUserInDatabase(username: string){
    const path = `${this.config.master_apiURL}/auth/IsUserInSystem/${username}`;
    return this.http.get<boolean>(path);
  }

 public register(FirstName: string, SecondName: string, Username: string, Password: string) {
  const u = new user(FirstName, SecondName, Username, Password);
  const path = `${this.config.master_apiURL}/auth/register`;
  return this.http.post(path, u);
  }

  public login(Username: string, Password: string) {
    const loginRequest = new LoginRequest(Username, Password);
    const path = `${this.config.master_apiURL}/auth/login`;
    return this.http.post(path, loginRequest)
      .pipe(
        map((response: LoginResponse) => {
           // console.log(response);
            if (user) {
              localStorage.setItem('token', response.token);
              localStorage.setItem('username', response.username);
              this.decodedToken = this.jwtHelper.decodeToken(response.token);
              //console.log(this.decodedToken);
            }
          }
        ));
  }
}
