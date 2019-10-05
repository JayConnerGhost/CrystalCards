import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Card } from './card';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  apiURL: string = 'http://localhost:60885/api';
  apiURL2: string = "http://localhost:55265/api";
  apiUR_staging: string = "https://crystalcardsapi20191004033914.azurewebsites.net/api";

  constructor(private httpClient: HttpClient) { }

  public getCards(): Observable<Card[]> {
    return this.httpClient.get<Card[]>(`${this.apiUR_staging}/cards`);
  }

  public update(title, description, id, points): Observable<Card> {
    console.log(title, description, id, points);
    let card = new Card();
    card.description = description;
    card.title = title;
    card.id = id;
    card.npPoints = points;

    return this.httpClient.put<Card>(`${this.apiUR_staging}/cards/${id}`, card);
  }

}
