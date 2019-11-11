import {Component, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {OpenCardComponent} from "../../cards/open-card/open-card.component";
import {ProjectApiService} from "../../services/project-api.service";
import {NgForm} from "@angular/forms";

@Component({
  selector: 'app-assign-project',
  templateUrl: './assign-project.component.html',
  styleUrls: ['./assign-project.component.css']
})
export class AssignProjectComponent implements OnInit {

  constructor(
    private dialogRef: MatDialogRef<OpenCardComponent>,
    private projectApiService:ProjectApiService,
    @Inject(MAT_DIALOG_DATA) public data: any) { }
  Id = this.data.id;
  selectedProject:any;
  projects:any;
  ngOnInit() {
   this.projectApiService.getProjects().subscribe(res=>{
     this.projects=res;
   });
  }


  onSubmit(f: NgForm) {
    console.log(this.selectedProject);
    this.projectApiService.AssignCardToProject(this.Id,this.selectedProject).subscribe();
    this.dialogRef.close();
  }
}
