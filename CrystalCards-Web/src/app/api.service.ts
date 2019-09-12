import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
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
}
