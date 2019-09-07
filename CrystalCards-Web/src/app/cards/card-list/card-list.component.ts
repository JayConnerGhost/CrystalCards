import { Component, OnInit } from '@angular/core';
import { ApiService} from '../../api.service';

@Component({
  selector: 'app-card-list',
  templateUrl: './card-list.component.html',
  styleUrls: ['./card-list.component.css']
})
export class CardListComponent implements OnInit {

  constructor(private apiService: ApiService) { }

  ngOnInit() {
    this.apiService.getCards().subscribe((res)=>{
        console.log(res);
      });  
  }

}
