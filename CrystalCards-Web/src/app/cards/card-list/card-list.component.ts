import { Component, OnInit, ChangeDetectionStrategy, ChangeDetectorRef  } from "@angular/core";
import { ApiService } from "../../services/api.service";
import { Card } from "src/app/card";
import { OpenCardComponent } from "../open-card/open-card.component";
import { MatDialog } from "@angular/material/dialog";
import { CardsService } from "src/app/services/cards.service";
import { CardPrintComponent } from '../card-print/card-print.component';
import {CardApiService} from "../../services/card-api.service";
import {ProjectService} from "../../services/project.service";
import {ProjectApiService} from "../../services/project-api.service";
import {AssignProjectComponent} from "../../projects/assign-project/assign-project.component";

@Component({
  selector: "app-card-list",
  templateUrl: "./card-list.component.html",
  styleUrls: ["./card-list.component.css"],

})
export class CardListComponent implements OnInit {
  cards: Card[];
  constructor(
    private apiService: CardApiService,
    private projectApiService:ProjectApiService,
    private cardService: CardsService,
    private projectService: ProjectService,
    public dialog: MatDialog,
    private cd: ChangeDetectorRef
  ) {}
 context="General";
asCardWall:boolean;
  ngOnInit() {
    this.apiService.getCards().subscribe(res => {
      this.cards = res;

    });
    this.projectService.loadGeneralEvent.subscribe(p=>{
      this.RefreshCardList();
      this.context="General";
    });
    this.cardService.refreshEvent.subscribe(c => {

      this.RefreshCardList();
      });
    this.projectService.LoadProjectEvent.subscribe(event =>{

    this.LoadProjectCards(event);
    });

  }
  LoadProjectCards(event){
if(event===-1){return;}
    this.cards = null;
    this.projectApiService.GetCardsForProject(event).subscribe(res=>{
      this.cards=null;
      this.cards=res.cards;
      this.context=res.title;
    });
    this.cd.detectChanges();
  }
  RefreshCardList() {
    this.asCardWall=this.cardService.asCardWall;
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

  AssignToProject(cardId: number) {

    let dialogRef = this.dialog.open(
      AssignProjectComponent,
      {
        width: "400px",
        data: {
          id: cardId,
        },

      });
  }
}
