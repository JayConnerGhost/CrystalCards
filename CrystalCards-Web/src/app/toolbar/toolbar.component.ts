import {ChangeDetectorRef, Component, OnInit} from '@angular/core';
import { ApiService } from '../services/api.service';
import { CardsService } from '../services/cards.service';
import { MatDialog } from '@angular/material';
import { OpenForAddCardComponent } from '../cards/open-for-add-card/open-for-add-card.component';
import { AuthService } from '../services/auth.service';
import { faSignOutAlt } from '@fortawesome/free-solid-svg-icons';
import { AlertifyService } from '../services/alertify.service';
import { Router } from '@angular/router';

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
              public authService: AuthService,
              private router: Router,
              private alertify: AlertifyService,
              public dialog: MatDialog,
              private dt: ChangeDetectorRef
  ) { }
  title = 'Crystal Ideas';
  ngOnInit() {
  }

IsAdmin(){
  return this.authService.IsAdmin();
}

loggedIn()
{
 return this.authService.loggedIn();
}
logout(){
  localStorage.removeItem('token');
  this.alertify.message("you were logged out");
  this.router.navigateByUrl('');
  this.dt.detectChanges();
}

 login(){
  this.authService.login(this.model).subscribe(next=>{
   this.alertify.success("successfully logged in ");
      this.dt.detectChanges()
  },
  error=>{
    this.alertify.error("failed to login");
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
  ShowAsDeck() {
    this.cardService.ShowCardsAsDeck();
  }
  ShowAsList() {
    this.cardService.ShowCardsAsList();
  }


}
