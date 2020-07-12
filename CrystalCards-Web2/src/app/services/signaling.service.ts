import { BehaviorSubject } from 'rxjs';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class SignalingService {

  refreshEvent: BehaviorSubject<boolean>;

  constructor() {
    this.refreshEvent = new BehaviorSubject(true);
  }

  refreshData()
  {
    this.refreshEvent.next(true);
  }
}
