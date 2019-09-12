import { Component, OnInit} from '@angular/core';
import { ApiService } from '../../api.service';
import { Card } from 'src/app/card';
import { OpenCardComponent } from '../open-card/open-card.component';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-card-list',
  templateUrl: './card-list.component.html',
  styleUrls: ['./card-list.component.css']
})
export class CardListComponent implements OnInit {


  cards: Card[];
  constructor(private apiService: ApiService, public dialog: MatDialog) { }

  ngOnInit() {
    this.apiService.getCards().subscribe((res) => {
      this.cards = res;
      console.log(res);
    });
  }
  DisplayCard(event) {
    //Here 
    let card = this.cards.find(x=>x.id==event);
    
    let dialogRef = this.dialog.open(OpenCardComponent,
    
      {
      height: '600px',
      width: '800px',
      data:{
        //Card data to go in here 
          id:event,
          title:card.title,
          description:card.description   
      },
    });
  }



}
