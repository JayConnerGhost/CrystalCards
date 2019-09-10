import { Component, OnInit, EventEmitter, Output, Input} from '@angular/core';
import { ApiService} from '../../api.service';
import { Card } from 'src/app/card';

@Component({
  selector: 'app-card-token',
  templateUrl: './card-token.component.html',
  styleUrls: ['./card-token.component.css']
})
export class CardTokenComponent implements OnInit {
  @Output() CardSelected = new EventEmitter<number>();
  @Input() Title;
  @Input() Description;
  @Input() Id;
  cards: Card[];
  constructor(private apiService: ApiService) { }

  ngOnInit() {
    this.apiService.getCards().subscribe((res)=>{
        this.cards=res;
        console.log(res);
      });
    }

      cardTokenClicked(){
        alert(this.Id);
        this.CardSelected.emit(this.Id);
      }
}
