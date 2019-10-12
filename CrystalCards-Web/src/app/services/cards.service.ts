import { BehaviorSubject } from 'rxjs';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CardsService {
  refreshEvent: BehaviorSubject<void>;
  constructor() { 

    this.refreshEvent = new BehaviorSubject(null);
  }

  refreshData()
  {
    this.refreshEvent.next();
  }
}
