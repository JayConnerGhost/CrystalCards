import { Component, OnInit } from '@angular/core';
import { ApiService } from './services/api.service';
import { MatDialog } from '@angular/material';
import { OpenForAddCardComponent } from './cards/open-for-add-card/open-for-add-card.component';
import { CardsService } from './services/cards.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent  implements OnInit {

  constructor(private apiService: ApiService,private cardService: CardsService, public dialog: MatDialog) { }


  ngOnInit(): void {}




}
