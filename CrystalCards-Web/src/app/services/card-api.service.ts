import { Injectable } from '@angular/core';
import {Observable} from "rxjs";
import {Card} from "../card";
import {AuthService} from "./auth.service";
import {ConfigService} from "./config.service";
import {HttpClient} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class CardApiService {

  constructor(
    private authService:AuthService,
    private configService:ConfigService,
    private httpClient: HttpClient
    ) { }

  public getCards(): Observable<Card[]> {
    let username = null;
    if(this.authService.loggedIn){
      username = this.authService.getUserName();
    }
    return this.httpClient.get<Card[]>(
      `${this.configService.master_apiURL}/cards/GetForUserName/${username}`
    );
  }
}
