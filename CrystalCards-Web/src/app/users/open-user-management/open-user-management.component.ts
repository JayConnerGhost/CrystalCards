import {Component, Inject, OnInit} from '@angular/core';
import {ApiService} from "../../services/api.service";
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";


@Component({
    selector: 'app-open-user-management',
  templateUrl: './open-user-management.component.html',
  styleUrls: ['./open-user-management.component.css']
})
export class OpenUserManagementComponent implements OnInit {
 users:any=null;
  constructor(private apiService: ApiService,
              public dialogRef: MatDialogRef<OpenUserManagementComponent>,
              @Inject(MAT_DIALOG_DATA) public data: any) { }

  ngOnInit() {
   this.loadUsers();
  }

  loadUsers() {
    this.users=this.apiService.getUsers().subscribe();
  }
}
