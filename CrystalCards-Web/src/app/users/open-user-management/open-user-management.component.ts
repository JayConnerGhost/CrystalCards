import {Component, Inject, OnInit} from '@angular/core';
import {ApiService} from "../../services/api.service";
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {CustomRole, User} from "../../User";


class DisplayUser {
  username: string;
  roles:string;
}

@Component({
    selector: 'app-open-user-management',
  templateUrl: './open-user-management.component.html',
  styleUrls: ['./open-user-management.component.css']
})
export class OpenUserManagementComponent implements OnInit {

  displayedColumns: string[] = ['username','roles'];
  dataSource = null;
  constructor(private apiService: ApiService,
              public dialogRef: MatDialogRef<OpenUserManagementComponent>,
              @Inject(MAT_DIALOG_DATA) public data: any) { }

  ngOnInit() {
   this.loadUsers();
   this.displayUsers();
  }

  loadUsers() {
    this.apiService.getUsers().subscribe(res=>{

      let displayUsers=res.map(user=> {
          var u = new DisplayUser();
          u.username = user.username;
          const flattenedArray:CustomRole[] = [].concat(...user.roles);
          let rolesNames="";
          const displayRoleNames=flattenedArray.every(function(role){
            rolesNames=`${rolesNames} ${role.name}`;
            return rolesNames;
          });
          u.roles=rolesNames;
          return u;
        }
      );
      this.dataSource=[...displayUsers];
    });

  }

  displayUsers() {
//TODO grid view

  }
}
