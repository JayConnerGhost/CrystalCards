import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
import {Card } from './card'
@Injectable({
  providedIn: 'root'
})
export class ApiService {

 
  apiURL: string = 'http://localhost:55265/api';

  constructor(private httpClient: HttpClient) {}

public getCards(){
  return this.httpClient.get<Card[]>(`${this.apiURL}/cards`);
}

updateIdea(title: string, description: string, Id: number): Observable<Card> {
console.log(title, description, Id);
var idea=new Card();
idea.id=Id;
idea.description=description;
idea.title=title;

return this.httpClient.put<Card>(`${this.apiURL}/cards`,idea);

}
}
