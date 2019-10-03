import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ApiService } from 'src/app/api.service';
@Component({
  selector: 'app-open-card',
  templateUrl: './open-card.component.html',
  styleUrls: ['./open-card.component.css']
})
export class OpenCardComponent implements OnInit {
  @Output() UpdatePerformed = new EventEmitter();
  constructor(
    private apiService: ApiService,
    public dialogRef: MatDialogRef<OpenCardComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) { }

  Description = this.data.description;
  Title = this.data.title;
  Id = this.data.id;
  Points = this.data.points;
  NegativePoints = null;
  PositivePoints = null;
  Order = this.data.order;


  ngOnInit() {
    this.splitPointsToLists(this.Points);
  }

  CardUpdated() {
    this.UpdatePerformed.emit();
  }

  CloseDialog() {
    this.dialogRef.close();
  }

  addNegative(newNegative){
console.log(newNegative.value);
  }

  onSubmit(f) {
    this.apiService.update(this.Title, this.Description, this.Id, this.Points).subscribe();
  }

  splitPointsToLists(points) {

    this.NegativePoints = points.filter(p => p.direction === 'Negative');
    this.PositivePoints = points.filter(p => p.direction === 'Positive');

  }
}
