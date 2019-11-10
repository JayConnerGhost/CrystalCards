import { Component, OnInit } from '@angular/core';
import {MatDialog} from "@angular/material/dialog";
import {OpenUserManagementComponent} from "../open-user-management/open-user-management.component";
import {AddProjectComponent} from "../../projects/add-project/add-project.component";
import {ListProjectComponent} from "../../projects/list-project/list-project.component";
import {ProjectService} from "../../services/project.service";

@Component({
  selector: 'app-general-menu',
  templateUrl: './general-menu.component.html',
  styleUrls: ['./general-menu.component.css']
})
export class GeneralMenuComponent implements OnInit {

  constructor(public dialog: MatDialog,
              public projectService: ProjectService
              ) { }

  ngOnInit() {
  }

  addProject() {
    this.dialog.open(AddProjectComponent,
      {
        width: "400px",
        maxHeight: "600px"
      }
    );
  }

  listProjects() {
    this.dialog.open(ListProjectComponent,
      {
        width: "800px",
        maxHeight: "600px"
      }
    );
  }

  LoadGeneral() {
    //Code in here to load all card to wall and ignore projects
    this.projectService.LoadGeneralData();
  }
}
