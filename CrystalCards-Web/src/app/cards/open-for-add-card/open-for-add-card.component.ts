import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ApiService } from 'src/app/services/api.service';
@Component({
  selector: 'app-open-for-add-card',
  templateUrl: './open-for-add-card.component.html',
  styleUrls: ['./open-for-add-card.component.css']
})
export class OpenForAddCardComponent implements OnInit {
Id=0;
Title=null;
Description=null;
  constructor(private apiService: ApiService,
              public dialogRef: MatDialogRef<OpenForAddCardComponent>,
              @Inject(MAT_DIALOG_DATA) public data: any) { }

  ngOnInit() {
  }
  closeDialog() {
    this.dialogRef.close();
  }

  onSubmit(f){
    this.apiService.NewBasic(this.Id, this.Title, this.Description).subscribe();
    this.closeDialog();
  }

}
