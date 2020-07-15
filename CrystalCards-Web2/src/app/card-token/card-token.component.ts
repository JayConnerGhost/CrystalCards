import {Component, Input, OnInit} from '@angular/core';
import {MatDialog} from '@angular/material/dialog';
import {EditCardComponent} from '../edit-card/edit-card.component';

@Component({
  selector: 'app-card-token',
  templateUrl: './card-token.component.html',
  styleUrls: ['./card-token.component.css']
})
export class CardTokenComponent implements OnInit {
  @Input() Title: string;
  @Input() Description: string;
  @Input() Id: number;

  constructor(private dialog: MatDialog) { }

  ngOnInit(): void {

  }

  openCard(Id: number) {
    const dialogRef = this.dialog.open(EditCardComponent,
      {
        width: '600px',
        panelClass: 'formFieldWidth550',
        data: {
            IdeaId: Id,
            Title: this.Title,
            Description: this.Description
        }
      },
     );
  }
}
