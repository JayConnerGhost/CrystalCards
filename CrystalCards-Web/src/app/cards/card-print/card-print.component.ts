import { Component, OnInit, Inject, Output } from '@angular/core';
import { NgxPrintModule } from 'ngx-print'
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { OpenCardComponent } from '../open-card/open-card.component';
import { ApiService } from 'src/app/services/api.service';
import { AuthService } from 'src/app/services/auth.service';
import { ConfigService } from 'src/app/services/config.service';

@Component({
  selector: 'app-card-print',
  templateUrl: './card-print.component.html',
  styleUrls: ['./card-print.component.css']
})
export class CardPrintComponent implements OnInit {


  constructor(
    private apiService: ApiService,
    private authService: AuthService,
    private configService:ConfigService,
    public dialogRef: MatDialogRef<OpenCardComponent>,
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
               images: any = null;
             @Output() id=this.Id;

  ngOnInit() {
    this.splitPointsToLists(this.Points);
    this.getImageURLs();
  }
  getImageURLs() {
      this.apiService.GetImageURLs(this.authService.decodedToken.unique_name,this.Id).subscribe(res => {
      this.images = res.map(x=> `${this.configService.Images}/${this.authService.decodedToken.unique_name}/${this.id}/${x}`);
    });
  }

  splitPointsToLists(points) {

    this.NegativePoints = points.filter(p => p.direction === 'Negative');
    this.PositivePoints = points.filter(p => p.direction === 'Positive');

  }

}
