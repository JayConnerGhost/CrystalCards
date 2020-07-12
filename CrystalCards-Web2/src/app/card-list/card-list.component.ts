import { Component, OnInit } from '@angular/core';
import {SignalingService} from '../services/signaling.service';

@Component({
  selector: 'app-card-list',
  templateUrl: './card-list.component.html',
  styleUrls: ['./card-list.component.css']
})
export class CardListComponent implements OnInit {

  constructor(private signalingService: SignalingService) { }

  ngOnInit(): void {
    this.signalingService.refreshEvent.subscribe(c => {

      this.RefreshCardList();
    });
  }


  private RefreshCardList() {
    //TODO: code in here to reload cardwall
  }
}
