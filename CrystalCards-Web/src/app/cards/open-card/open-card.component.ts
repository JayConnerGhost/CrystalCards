import { Component, OnInit ,EventEmitter,Output} from '@angular/core';
import {Inject} from '@angular/core';
import {MAT_DIALOG_DATA,  MatDialogRef} from '@angular/material/dialog';
import { ApiService } from 'src/app/api.service';
@Component({
  selector: 'app-open-card',
  templateUrl: './open-card.component.html',
  styleUrls: ['./open-card.component.css']
})
export class OpenCardComponent implements OnInit {
  @Output() UpdatePerformed= new EventEmitter();
  constructor(private apiService: ApiService,  public dialogRef: MatDialogRef<OpenCardComponent>,@Inject(MAT_DIALOG_DATA) public data: any) { }

   Description = this.data.description;
   Title = this.data.title;
   Id = this.data.id;
   Points = this.data.points;
  ngOnInit() {
  }

  CardUpdated()  {
    this.UpdatePerformed.emit();
  }

  CloseDialog()  {
    //code in here to close dialog
    this.dialogRef.close();
  }

  onSubmit(f)  {
    console.log("Save");
    console.log(f);
    this.apiService.update(this.Title, this.Description, this.Id, this.Points).subscribe();
  }
}
