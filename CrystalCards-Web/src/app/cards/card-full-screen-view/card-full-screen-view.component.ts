import {Component, Inject, OnInit, Output} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {OpenCardComponent} from "../open-card/open-card.component";
import {ApiService} from "../../services/api.service";
import {AuthService} from "../../services/auth.service";
import {ConfigService} from "../../services/config.service";

@Component({
  selector: 'app-card-full-screen-view',
  templateUrl: './card-full-screen-view.component.html',
  styleUrls: ['./card-full-screen-view.component.css']
})
export class CardFullScreenViewComponent implements OnInit {

  constructor( public dialogRef: MatDialogRef<OpenCardComponent>,
               @Inject(MAT_DIALOG_DATA) public data: any,
               public apiService:ApiService,
               public authService:AuthService,
               public configService:ConfigService
               ) { }

  Description = this.data.description;
  Title = this.data.title;
  Id = this.data.id;
  Points = this.data.points;
  NegativePoints = null;
  PositivePoints = null;
  ActionPoints = this.data.actionPoints;
  Order = this.data.order;
  Links = this.data.links;
  images: any = null;
  @Output() id=this.Id;

  ngOnInit() {
    this.splitPointsToLists(this.Points);
    this.getImageURLs();
  }

  splitPointsToLists(points) {

    this.NegativePoints = points.filter(p => p.direction === 'Negative');
    this.PositivePoints = points.filter(p => p.direction === 'Positive');

  }

  getImageURLs() {
    this.apiService.GetImageURLs(this.authService.decodedToken.unique_name, this.Id).subscribe(res => {
      this.images = res.map(x => `${this.configService.Images}/${this.authService.decodedToken.unique_name}/${this.id}/${x}`);
    });
  }

  print() {

  }

  close() {
    this.dialogRef.close();
  }
}
