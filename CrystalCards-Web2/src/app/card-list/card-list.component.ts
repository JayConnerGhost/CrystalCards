import { Component, OnInit } from '@angular/core';
import {SignalingService} from '../services/signaling.service';
import {CardService} from '../services/card.service';
import {Observable} from 'rxjs';
import {Card} from '../card';
import {AlertifyService} from '../services/alertify.service';

@Component({
  selector: 'app-card-list',
  templateUrl: './card-list.component.html',
  styleUrls: ['./card-list.component.css']
})
export class CardListComponent implements OnInit {

  cards: any;

  constructor(private signalingService: SignalingService,
              private cardService: CardService,
              private alertifyService: AlertifyService
              ) { }

  ngOnInit(): void {
    this.signalingService.refreshEvent.subscribe(c => {

      this.RefreshCardList();
    });
  }


  private RefreshCardList() {
    this.cardService.Get().subscribe((data) => {
      this.cards = data;
      this.alertifyService.success('Idea wall Rebuilt');
    },
      error => {
        this.alertifyService.error('Error Rebuilding Idea Wall');
      });
  }
}
