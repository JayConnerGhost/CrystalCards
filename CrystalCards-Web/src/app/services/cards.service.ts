import { BehaviorSubject } from 'rxjs';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CardsService {
  refreshEvent: BehaviorSubject<boolean>;
  constructor() {

    this.refreshEvent = new BehaviorSubject(true);
  }

  refreshData()
  {
    this.refreshEvent.next(true);
  }

  ShowCardsAsList() {
    this.refreshEvent.next(false);
  }

  ShowCardsAsDeck() {
    this.refreshEvent.next(true);
  }
}
