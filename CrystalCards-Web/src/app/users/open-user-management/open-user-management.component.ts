import {ChangeDetectorRef, Component, Inject, OnInit, ViewChild} from '@angular/core';
import {ApiService} from "../../services/api.service";
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {CustomRole, User} from "../../User";
import {MatTable} from "@angular/material/table";


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
@ViewChild('table') table: MatTable<any>;
  displayedColumns: string[] = ['username','roles','makeAdmin','deleteUsers'];
  dataSource = null;
  constructor(private apiService: ApiService,
              public dialogRef: MatDialogRef<OpenUserManagementComponent>,
              private cd: ChangeDetectorRef,
              @Inject(MAT_DIALOG_DATA) public data: any) { }

  ngOnInit() {
   this.loadUsers();
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
    this.table.renderRows()
  }
}
