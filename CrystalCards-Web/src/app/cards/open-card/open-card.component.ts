import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ApiService } from 'src/app/api.service';
import { NPPoint } from '../NPPoint';
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
  addPositive(newPositive){
    var newPoint = new NPPoint();
    newPoint.description = newPositive.value;
    newPoint.direction = "Positive";
    this.PositivePoints.push(newPoint)
  }

  onPositivePointRemove(id){
    const target= this.PositivePoints.find(x=>x.id===id);
    const index=this.PositivePoints.indexOf(target);
    if(index > -1){
      this.PositivePoints.splice(index,1);
    }
  }

  addNegative(newNegative) {

    var newPoint = new NPPoint();
    newPoint.description = newNegative.value;
    newPoint.direction = "Negative";
    this.NegativePoints.push(newPoint);
  }
  onNegativePointRemove(id){
   const target= this.NegativePoints.find(x=>x.id===id);
   const index=this.NegativePoints.indexOf(target);
   if(index > -1){
     this.NegativePoints.splice(index,1);
   }

  }


  onSubmit(f) {
    const editedPoints = (this.NegativePoints.concat(this.PositivePoints));
    this.apiService.update(this.Title, this.Description, this.Id, editedPoints).subscribe();
  }

  splitPointsToLists(points) {

    this.NegativePoints = points.filter(p => p.direction === 'Negative');
    this.PositivePoints = points.filter(p => p.direction === 'Positive');

  }
}
