import {Component, OnInit, ChangeDetectorRef} from '@angular/core';
import {MatDialog} from '@angular/material/dialog';
import {AddCardComponent} from '../add-card/add-card.component';

@Component({
  selector: 'app-toolbar',
  templateUrl: './toolbar.component.html',
  styleUrls: ['./toolbar.component.css'],
})
export class ToolbarComponent implements OnInit {
  constructor(private dialog: MatDialog,
              private dt: ChangeDetectorRef) {}

  ngOnInit(): void {}

  addIdea() {
    const dialogRef = this.dialog.open(AddCardComponent,
      {
        width: '600px',
        panelClass: 'formFieldWidth550'
      });

  }
}
