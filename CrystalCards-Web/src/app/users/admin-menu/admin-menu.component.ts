import { Component, OnInit, Output, EventEmitter, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from 'src/app/services/api.service';
import { OpenForAddCardComponent } from 'src/app/cards/open-for-add-card/open-for-add-card.component';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialog } from '@angular/material/dialog';
import { OpenUserManagementComponent } from '../open-user-management/open-user-management.component';

@Component({
  selector: 'app-admin-menu',
  templateUrl: './admin-menu.component.html',
  styleUrls: ['./admin-menu.component.css']
})
export class AdminMenuComponent implements OnInit {

  constructor(public dialog: MatDialog,) { }

  ngOnInit() {
  }
  openUserList() {
// Open dialog
this.dialog.open(OpenUserManagementComponent,
    {
      width: "800px",
      maxHeight: "600px"
    }
  );
// Load user component
//Load Users

  }

}
