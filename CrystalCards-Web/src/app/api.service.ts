import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {Card } from './card';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  apiURL: string = 'http://localhost:60885/api';

  constructor(private httpClient: HttpClient) {}

public getCards(){
  return this.httpClient.get<Card[]>(`${this.apiURL}/cards`);
}

public updateIdea(title, description, id): Observable<Card> {
  console.log(title,description, id);
  let card = new Card();
  card.description = description;
  card.title = title;
  card.id = id;

  return this.httpClient.put<Card>(`${this.apiURL}/cards/${id}`, card);

}
}
