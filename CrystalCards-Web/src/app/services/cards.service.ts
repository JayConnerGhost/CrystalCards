import { BehaviorSubject } from 'rxjs';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CardsService {
  asCardWall:boolean=true;

  refreshEvent: BehaviorSubject<boolean>;
  constructor() {

    this.refreshEvent = new BehaviorSubject(this.asCardWall);
  }

  refreshData()
  {
    this.refreshEvent.next(this.asCardWall);
  }

  ShowCardsAsList() {
    this.asCardWall=false;
    this.refreshEvent.next(this.asCardWall);
  }

  ShowCardsAsDeck() {
    this.asCardWall=true;
    this.refreshEvent.next(this.asCardWall);
  }
}
