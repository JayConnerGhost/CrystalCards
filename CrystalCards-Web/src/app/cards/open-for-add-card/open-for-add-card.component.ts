import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ApiService } from 'src/app/api.service';
@Component({
  selector: 'app-open-for-add-card',
  templateUrl: './open-for-add-card.component.html',
  styleUrls: ['./open-for-add-card.component.css']
})
export class OpenForAddCardComponent implements OnInit {

  constructor(private apiService: ApiService,
    public dialogRef: MatDialogRef<OpenForAddCardComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) { }

  ngOnInit() {
  }

}
