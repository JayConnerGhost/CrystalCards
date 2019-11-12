import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { Inject } from '@angular/core';
import {MAT_DIALOG_DATA, MatDialog, MatDialogConfig, MatDialogRef} from '@angular/material/dialog';
import { ApiService } from 'src/app/services/api.service';
import { NPPoint } from '../NPPoint';
import { ActionPoint } from '../ActionPoint';
import { UrlLink } from '../Link';
import {CardApiService} from "../../services/card-api.service";
import {CardFullScreenViewComponent} from "../card-full-screen-view/card-full-screen-view.component";
@Component({
  selector: 'app-open-card',
  templateUrl: './open-card.component.html',
  styleUrls: ['./open-card.component.css']
})
export class OpenCardComponent implements OnInit {
  @Output() UpdatePerformed = new EventEmitter();
  constructor(
    private apiService: CardApiService,
    public dialogRef: MatDialogRef<OpenCardComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    public dialog: MatDialog,
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
@Output() id=this.Id;

  ngOnInit() {
    this.splitPointsToLists(this.Points);
    console.log(this.Links);
  }


  CardUpdated() {
    this.UpdatePerformed.emit();
  }

  CloseDialog() {
    this.dialogRef.close();
  }
  addPositive(newPositive) {
    var newPoint = new NPPoint();
    newPoint.description = newPositive.value;
    newPositive.value = '';
    newPoint.direction = "Positive";
    this.PositivePoints.push(newPoint)
  }

  onPositivePointRemove(id) {
    const target = this.PositivePoints.find(x => x.id === id);
    const index = this.PositivePoints.indexOf(target);
    if (index > -1) {
      this.PositivePoints.splice(index, 1);
    }
  }

  addNegative(newNegative) {

    var newPoint = new NPPoint();
    newPoint.description = newNegative.value;
    newNegative.value = '';
    newPoint.direction = "Negative";
    this.NegativePoints.push(newPoint);
  }
  onNegativePointRemove(id) {
    const target = this.NegativePoints.find(x => x.id === id);
    const index = this.NegativePoints.indexOf(target);
    if (index > -1) {
      this.NegativePoints.splice(index, 1);
    }

  }

  addActionPoint(actionPoint){
    var newActionPoint=new ActionPoint();
    newActionPoint.description=actionPoint.value;
    actionPoint.value = '';
    this.ActionPoints.push(newActionPoint);
  }

  onActionPointRemove(id){
    const target = this.ActionPoints.find(x=>x.id===id);
    const index = this.ActionPoints.indexOf(target);
    if(index >-1)
    {
      this.ActionPoints.splice(index,1);
    }
  }

  addLink(LinkDescription, LinkUrl) {
    var newLink = new UrlLink();
    newLink.description = LinkDescription.value;
    newLink.Url = LinkUrl.value;
    LinkDescription.value = '';
    LinkUrl = '';
    this.Links.push(newLink);
    console.log(newLink);
  }

  onLinkRemove(id){
    const target = this.Links.find(x=>x.id===id);
    const index = this.Links.indexOf(target);
    if(index >-1)
    {
      this.Links.splice(index,1);
    }
  }

  onSubmit(f) {
    const editedPoints = (this.NegativePoints.concat(this.PositivePoints));
    this.apiService.update(this.Title, this.Description, this.Id, editedPoints, this.ActionPoints, this.Links).subscribe();
  }

  splitPointsToLists(points) {

    this.NegativePoints = points.filter(p => p.direction === 'Negative');
    this.PositivePoints = points.filter(p => p.direction === 'Positive');

  }

  fullScreen() {
    //Open card in full screen layout - readonly
    let config=new MatDialogConfig();
    config = {
      position: {
        top: '10px',
        right: '10px'
      },
      height: '98%',
      width: '100vw',
      panelClass: 'full-screen-modal',
    };

    const dialog=this.dialog.open(CardFullScreenViewComponent,config);



    //TODO
    //Open Dialog full screen
    //Add Leave Full screen
    //Add CLose button
    //Add print button
    //Layout content as a document
  }
}
