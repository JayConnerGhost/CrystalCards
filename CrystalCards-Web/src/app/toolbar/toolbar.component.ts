import { Component, OnInit } from '@angular/core';
import { ApiService } from '../services/api.service';
import { CardsService } from '../services/cards.service';
import { MatDialog } from '@angular/material';
import { OpenForAddCardComponent } from '../cards/open-for-add-card/open-for-add-card.component';

@Component({
  selector: 'app-toolbar',
  templateUrl: './toolbar.component.html',
  styleUrls: ['./toolbar.component.css']
})
export class ToolbarComponent implements OnInit {
 model:any = {};

  constructor(private apiService: ApiService, private cardService: CardsService, public dialog: MatDialog) { }
  title = 'Crystal Ideas';
  ngOnInit() {
  }

 login(){
   console.log(this.model);
 }


  openAddDialogBrainStorming(event){

    const dialogRef = this.dialog.open(OpenForAddCardComponent,

      {
       width: '800px',
      data:{},
      panelClass : 'formFieldWidth550'
    });
    dialogRef.afterClosed().subscribe(result => {
      //refresh list data .
      this.cardService.refreshData();
    });


  }
}
