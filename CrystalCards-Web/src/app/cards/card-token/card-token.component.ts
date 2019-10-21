import { Component, OnInit, EventEmitter, Output, Input} from '@angular/core';
import { ApiService} from '../../services/api.service';
import { Card } from 'src/app/card';
import { _MatTabHeaderMixinBase } from '@angular/material/tabs/typings/tab-header';

@Component({
  selector: 'app-card-token',
  templateUrl: './card-token.component.html',
  styleUrls: ['./card-token.component.scss']
})
export class CardTokenComponent implements OnInit {
  @Output() CardSelected = new EventEmitter<number>();
  @Output() CardRequestDelete = new EventEmitter<number>();
  @Input() Title;
  @Input() Description;
  @Input() Id;
  cards: Card[];
  constructor(private apiService: ApiService) { }

  ngOnInit() {
    this.apiService.getCards().subscribe((res)=>{
        this.cards=res;

      });
    }

    cardEditClicked(){
        this.CardSelected.emit(this.Id);
      }
    cardDeleteClicked(){
      this.CardRequestDelete.emit(this.Id);
    }
}
