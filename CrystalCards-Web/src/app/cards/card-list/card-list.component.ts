import { Component, OnInit, ChangeDetectionStrategy, ChangeDetectorRef  } from "@angular/core";
import { ApiService } from "../../services/api.service";
import { Card } from "src/app/card";
import { OpenCardComponent } from "../open-card/open-card.component";
import { MatDialog } from "@angular/material/dialog";
import { CardsService } from "src/app/services/cards.service";
import { CardPrintComponent } from '../card-print/card-print.component';
import {CardApiService} from "../../services/card-api.service";

@Component({
  selector: "app-card-list",
  templateUrl: "./card-list.component.html",
  styleUrls: ["./card-list.component.css"],

})
export class CardListComponent implements OnInit {
  cards: Card[];
  constructor(
    private apiService: CardApiService,
    private cardService: CardsService,
    public dialog: MatDialog,
    private cd: ChangeDetectorRef
  ) {}

  ngOnInit() {
    this.apiService.getCards().subscribe(res => {
      this.cards = res;
      console.log(res);
    });

    this.cardService.refreshEvent.subscribe(c => {
      this.RefreshCardList();

    });
  }
  RefreshCardList() {
    console.log("getting cards ");
    this.apiService.getCards().subscribe(res => {
      this.cards = null;
      this.cards = res;
    });
    this.cd.detectChanges();
  }

  PrintCard(event){
    //Open print preview dialog
    let card = this.cards.find(x => x.id == event);
    let dialogRef = this.dialog.open(
      CardPrintComponent,

      {
        width: "800px",
        data: {
          //Card data to go in here
          id: card.id,
          title: card.title,
          description: card.description,
          points: card.npPoints,
          actionPoints: card.actionPoints,
          order: card.order,
          links: card.links
        },
        panelClass: "formFieldWidth550"
      }
    );
    dialogRef.afterClosed().subscribe(data => {

    });
    //compose html / css print preview
    //provide print button --npx-print
  }

  DeleteCard(event)  {

      this.apiService.deleteCard(event).subscribe();
      const target = this.cards.find(x => x.id === event);
      const index = this.cards.indexOf(target);
      if (index > -1) {
        this.cards.splice(index, 1);
      }
      //this.RefreshCardList();
  }

  DisplayCard(event) {
    //Here
    let card = this.cards.find(x => x.id == event);
    console.log("pre-dialog", card);
    let dialogRef = this.dialog.open(
      OpenCardComponent,

      {
        width: "800px",
        data: {

          id: card.id,
          title: card.title,
          description: card.description,
          points: card.npPoints,
          actionPoints: card.actionPoints,
          order: card.order,
          links: card.links
        },
        panelClass: "formFieldWidth550"
      }
    );
    dialogRef.afterClosed().subscribe(data => {
      this.RefreshCardList();
    });
  }
}
