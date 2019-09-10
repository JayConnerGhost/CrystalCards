import { Component, OnInit, EventEmitter, Output} from '@angular/core';
import { ApiService} from '../../api.service';
import { Card } from 'src/app/card';

@Component({
  selector: 'app-card-list',
  templateUrl: './card-list.component.html',
  styleUrls: ['./card-list.component.css']
})
export class CardListComponent implements OnInit {
@Output() CardSelected = new EventEmitter<number>();

  cards: Card[];
  constructor(private apiService: ApiService) { }

  ngOnInit() {
    this.apiService.getCards().subscribe((res)=>{
        this.cards=res;
        console.log(res);
      });
  }

cardTokenClicked(card: Card){
  alert(card.id);
  this.CardSelected.emit(card.id);
}

}
