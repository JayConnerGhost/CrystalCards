import { Component, OnInit , AfterViewInit} from '@angular/core';
import { jsPlumb } from 'jsplumb';
import {ApiService} from "../../services/api.service";
import {ProjectApiService} from "../../services/project-api.service";
@Component({
  selector: 'app-project-picture',
  templateUrl: './project-picture.component.html',
  styleUrls: ['./project-picture.component.css']
})
export class ProjectPictureComponent implements OnInit {
  jsPlumbInstance;
  projects;
  cards;
  constructor(private apiService:ProjectApiService) { }

  ngOnInit() {
    this.jsPlumbInstance = jsPlumb.getInstance();
    this.loadProjects();
    this.wireUp()
  }


  wireUp(){
 //code in here to wire up the map
  };


  private loadProjects() {
    //code in here to load project list
    this.apiService.getProjects().subscribe(res=>{
      this.projects=res;
    });
  }

  private loadProjectCards(projectId:number)
  {
  //get cards for project Id
  }

  private showCards(){

  }

}
