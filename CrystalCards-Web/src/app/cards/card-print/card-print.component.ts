import { Component, OnInit, Inject, Output } from '@angular/core';
import { NgxPrintModule } from 'ngx-print'
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { OpenCardComponent } from '../open-card/open-card.component';

@Component({
  selector: 'app-card-print',
  templateUrl: './card-print.component.html',
  styleUrls: ['./card-print.component.css']
})
export class CardPrintComponent implements OnInit {

  constructor( public dialogRef: MatDialogRef<OpenCardComponent>,
               @Inject(MAT_DIALOG_DATA) public data: any) { }

               Description = this.data.description;
               Title = this.data.title;
               Id = this.data.id;
               Points = this.data.points;
               NegativePoints = null;
               PositivePoints = null;
               ActionPoints = this.data.actionPoints;
               Order = this.data.order;
               Links = this.data.links;
             @Output() id=this.Id;

  ngOnInit() {
    this.splitPointsToLists(this.Points);
  }

  splitPointsToLists(points) {

    this.NegativePoints = points.filter(p => p.direction === 'Negative');
    this.PositivePoints = points.filter(p => p.direction === 'Positive');

  }

}
