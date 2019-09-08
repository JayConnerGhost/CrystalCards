import { Component, OnInit } from '@angular/core';
import { ApiService} from '../../api.service';
import { Card } from 'src/app/card';

@Component({
  selector: 'app-card-list',
  templateUrl: './card-list.component.html',
  styleUrls: ['./card-list.component.css']
})
export class CardListComponent implements OnInit {

  cards: Card[];
  constructor(private apiService: ApiService) { }

  ngOnInit() {
    this.apiService.getCards().subscribe((res)=>{
        this.cards=res;
        console.log(res);
      });  
  }

}
