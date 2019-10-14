import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Card } from '.././card';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
import { ConfigService } from './config.service';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor(
    private httpClient: HttpClient,
    private configService: ConfigService,
    private authService: AuthService
    ) { }

  NewBasic(Id: number, Title: any, Description: any): Observable<Card>  {
    let card = new Card();
    card.description = Description;
    card.title = Title;
    card.id = Id;
    return this.httpClient.post<Card>(`${this.configService.master_apiURL}/cards`, card);
  }
  public getCards(): Observable<Card[]> {
    return this.httpClient.get<Card[]>(`${this.configService.master_apiURL}/cards`);
  }

    public update(title, description, id, points, actionPoints): Observable<Card> {
    console.log(title, description, id, points);
    let card = new Card();
    card.description = description;
    card.title = title;
    card.id = id;
    card.npPoints = points;
    card.actionPoints=actionPoints;

    return this.httpClient.put<Card>(`${this.configService.master_apiURL}/cards/${id}`, card);
  }

  GetImageURLs(cardId): any {
    console.log(cardId);
    let userName=this.authService.decodedToken.unique_name;
    return this.httpClient.get<string[]>(`${this.configService.master_apiURL}/moodwall/${cardId}`);
   // return this.httpClient.get<string[]>(`${this.configService.master_apiURL}/moodwall/${userName}/${cardId}`);

  }

}
