import {ChangeDetectorRef, Component, Inject, OnInit, ViewChild} from '@angular/core';
import {ApiService} from "../../services/api.service";
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {User} from "../../User";
import {MatTable} from "@angular/material/table";
import {MatCheckbox} from "@angular/material/checkbox";
import {CustomRole} from "../../Role";


class DisplayUser {
  username: string;
  roles:string;
  isAdmin:Boolean;
}

@Component({
    selector: 'app-open-user-management',
  templateUrl: './open-user-management.component.html',
  styleUrls: ['./open-user-management.component.css']
})
export class OpenUserManagementComponent implements OnInit {
@ViewChild('table',null) table: MatTable<any>;
  displayedColumns: string[] = ['username','roles','makeAdmin','deleteUsers'];
  dataSource = null;
  constructor(private apiService: ApiService,
              public dialogRef: MatDialogRef<OpenUserManagementComponent>,
              private cd: ChangeDetectorRef,
              @Inject(MAT_DIALOG_DATA) public data: any) { }

  ngOnInit() {
   this.loadUsers();
  }

  close()  {
    this.dialogRef.close();
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
          if(u.roles.includes("Administrator")){
            u.isAdmin=true;
          }
          else
          {
            u.isAdmin=false;
          }
          return u;
        }
      );
      this.dataSource=[...displayUsers];
    });

  }

  deletePerson(username: string) {
    if(username==="Admin"){return;}
    this.apiService.deleteUser(username).subscribe();
    const target = this.dataSource.find(x => x.username === username);
    const index = this.dataSource.indexOf(target);
    if (index > -1) {
      this.dataSource.splice(index, 1);
    }
    this.cd.detectChanges();
    this.table.renderRows();
  }

  changeAdmin(checkboxisadmin: MatCheckbox, username: string) {
    console.log(username);
    console.log(checkboxisadmin);
    this.apiService.changeAdminOnUser(username, checkboxisadmin.checked).subscribe();
    const target = this.dataSource.find(x => x.username === username);
    if(checkboxisadmin.checked){
      target.roles=' Administrator'
    }
    else {
      target.roles='';
    }
    this.cd.detectChanges();
    this.table.renderRows();
  }
}
