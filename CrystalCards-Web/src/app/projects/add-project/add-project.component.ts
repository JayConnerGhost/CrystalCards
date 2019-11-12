import { Component, OnInit } from '@angular/core';
import {ProjectApiService} from "../../services/project-api.service";
import {NgForm} from "@angular/forms";
import {MatDialogRef} from "@angular/material/dialog";

@Component({
  selector: 'app-add-project',
  templateUrl: './add-project.component.html',
  styleUrls: ['./add-project.component.css']
})
export class AddProjectComponent implements OnInit {

  constructor(
    private projectApiService: ProjectApiService,
    public dialogRef: MatDialogRef<AddProjectComponent>
  ) { }
  Id=0;
  Title=null;
  ngOnInit() {
  }

  onSubmit(f: NgForm) {
    this.projectApiService.AddProject(this.Id,this.Title).subscribe();
    this.dialogRef.close();
  }
}
