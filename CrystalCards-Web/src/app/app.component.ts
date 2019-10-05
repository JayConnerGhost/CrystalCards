import { Component, OnInit } from '@angular/core';
import { ApiService } from './api.service';
import { MatDialog } from '@angular/material';
import { OpenForAddCardComponent } from './cards/open-for-add-card/open-for-add-card.component';
import { CardsService } from './cards.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent  implements OnInit {
  title = 'Crystal Ideas';
  constructor(private apiService: ApiService,private cardService: CardsService, public dialog: MatDialog) { }


  ngOnInit(): void {}


  openAddDialogBrainStorming(event){

    let dialogRef = this.dialog.open(OpenForAddCardComponent,

      {
       width: '800px',
      data:{},
      panelClass : "formFieldWidth550"
    });
    dialogRef.afterClosed().subscribe(result => {
      //refresh list data .
      this.cardService.refreshData();
    });


  }

}
