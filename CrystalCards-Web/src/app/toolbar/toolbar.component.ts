import { Component, OnInit } from '@angular/core';
import { ApiService } from '../services/api.service';
import { CardsService } from '../services/cards.service';
import { MatDialog } from '@angular/material';
import { OpenForAddCardComponent } from '../cards/open-for-add-card/open-for-add-card.component';
import { AuthService } from '../services/auth.service';
import { faSignOutAlt } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-toolbar',
  templateUrl: './toolbar.component.html',
  styleUrls: ['./toolbar.component.css']
})
export class ToolbarComponent implements OnInit {
 model:any = {};
 faSignOutAlt=faSignOutAlt;
  constructor(private apiService: ApiService,
              private cardService: CardsService,
              private authService: AuthService,
              public dialog: MatDialog) { }
  title = 'Crystal Ideas';
  ngOnInit() {
  }

loggedIn()
{
  const token = localStorage.getItem('token');
  return !!token;
}
logout(){
  localStorage.removeItem('token');
}

 login(){
  this.authService.login(this.model).subscribe(next=>{
    console.log('logged in ');
  },
  error=>{
    console.log('failed to login');
  });
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
