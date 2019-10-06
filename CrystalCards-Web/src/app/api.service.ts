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

  NewBasic(Id: number, Title: any, Description: any): Observable<Card>  {
    let card = new Card();
    card.description = Description;
    card.title = Title;
    card.id = Id;
    return this.httpClient.post<Card>(`${this.apiURL2}/cards`, card);
  }
  public getCards(): Observable<Card[]> {
    return this.httpClient.get<Card[]>(`${this.apiURL2}/cards`);
  }

    public update(title, description, id, points, actionPoints): Observable<Card> {
    console.log(title, description, id, points);
    let card = new Card();
    card.description = description;
    card.title = title;
    card.id = id;
    card.npPoints = points;
    card.actionPoints=actionPoints;

    return this.httpClient.put<Card>(`${this.apiURL2}/cards/${id}`, card);
  }

}
