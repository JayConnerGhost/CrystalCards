import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ConfigService {
  // tslint:disable-next-line:variable-name
  master_apiURL = 'http://localhost:54650';

  constructor() { }
}
