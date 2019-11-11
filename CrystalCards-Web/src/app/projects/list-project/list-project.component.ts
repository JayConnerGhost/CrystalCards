import {ChangeDetectorRef, Component, OnInit, ViewChild} from '@angular/core';
import {Projects} from "@angular/cli/lib/config/schema";
import {ProjectApiService} from "../../services/project-api.service";
import {ProjectService} from "../../services/project.service";
import {MatTable} from "@angular/material/table";

@Component({
  selector: 'app-list-project',
  templateUrl: './list-project.component.html',
  styleUrls: ['./list-project.component.css']
})
export class ListProjectComponent implements OnInit {
  @ViewChild('table',null) table: MatTable<any>;
  projects: Projects[];

  displayedColumns: string[] = ['Id', 'Title','load','delete'];
  private dataSource: any;

  constructor(
    private projectApiService: ProjectApiService,
    private projectService: ProjectService,
    private cd: ChangeDetectorRef,
  ) { }

  ngOnInit() {
    this.projectApiService.getProjects().subscribe(rez=>{
      this.projects=rez;
      this.dataSource=this.projects;
    });
  }

  loadProjectContext(id: any) {

    this.projectService.LoadProjectData(id)
  }

  deleteProject(id: any) {

    this.projectApiService.deleteProject(id).subscribe();
    //splice array to remove deleted project
    const target = this.dataSource.find(x => x.id === id);
    const index = this.dataSource.indexOf(target);
    if (index > -1) {
      this.dataSource.splice(index, 1);
    }
    this.cd.detectChanges();
    this.table.renderRows();
  }
}
