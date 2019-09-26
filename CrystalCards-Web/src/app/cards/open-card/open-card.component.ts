import { Component, OnInit ,EventEmitter,Output} from '@angular/core';
import {Inject} from '@angular/core';
import {MAT_DIALOG_DATA,  MatDialogRef} from '@angular/material/dialog';
@Component({
  selector: 'app-open-card',
  templateUrl: './open-card.component.html',
  styleUrls: ['./open-card.component.css']
})
export class OpenCardComponent implements OnInit {
  @Output() UpdatePerformed= new EventEmitter();
  constructor(  public dialogRef: MatDialogRef<OpenCardComponent>,@Inject(MAT_DIALOG_DATA) public data: any) { }

  ngOnInit() {
  }

  CardUpdated()
  {
    this.UpdatePerformed.emit();
  }

  CloseDialog()
  {
    //code in here to close dialog
    this.dialogRef.close();
  }
}
