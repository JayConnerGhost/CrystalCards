import { Injectable } from '@angular/core';
import {Observable} from 'rxjs';
import {Card} from '../card';
import {AuthService} from './auth.service';
import {HttpClient} from '@angular/common/http';
import {ConfigService} from './config.service';

@Injectable({
  providedIn: 'root'
})
export class CardService {

  constructor(private authService: AuthService,
              private httpClient: HttpClient,
              private configService: ConfigService
  ) { }

 public New(Title: string, Description: string): Observable<Card>{
    const card = new Card(Title, Description);

    let userName = null;
    if (this.authService.loggedIn)
    {
      userName = this.authService.getUserName();
    }
    return this.httpClient.post<Card>(`${this.configService.master_apiURL}/cards/${userName}`, card);

  }

  Get(): Observable<Card> {
    let userName = null;
    if (this.authService.loggedIn)
    {
      userName = this.authService.getUserName();
    }
    return this.httpClient.get<Card>(`${this.configService.master_apiURL}/cards/GetForUserName/${userName}`);
  }
}
