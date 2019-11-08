import { Component, OnInit } from '@angular/core';
import {Projects} from "@angular/cli/lib/config/schema";
import {ProjectApiService} from "../../services/project-api.service";

@Component({
  selector: 'app-list-project',
  templateUrl: './list-project.component.html',
  styleUrls: ['./list-project.component.css']
})
export class ListProjectComponent implements OnInit {

  projects: Projects[];

  displayedColumns: string[] = ['Id', 'Title','load','delete'];
  private dataSource: any;
  constructor(private projectApiService: ProjectApiService) { }
  ngOnInit() {
    this.projectApiService.getProjects().subscribe(rez=>{
      this.projects=rez;
      this.dataSource=this.projects;
    });
  }

}
