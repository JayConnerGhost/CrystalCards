import {Component, EventEmitter, Input, OnInit, Output, ViewChild} from '@angular/core';
import {MatTable, MatTableDataSource} from "@angular/material/table";
import {Card} from "../../card";
import {MatSort} from "@angular/material/sort";

@Component({
  selector: 'app-cards-list-view',
  templateUrl: './cards-list-view.component.html',
  styleUrls: ['./cards-list-view.component.css']
})
export class CardsListViewComponent implements OnInit {
  @ViewChild('table',null) table: MatTable<any>;
  @Input() Data;
  @ViewChild(MatSort, {static: true}) sort: MatSort;
  @Output() CardSelected = new EventEmitter<number>();
  @Output() CardRequestDelete = new EventEmitter<number>();
  @Output() CardRequestPrint = new EventEmitter<number>();
  @Output() CardRequestAddToProject = new EventEmitter<number>();

  dsCards:MatTableDataSource<Card>;
  displayedColumns: string[] = ['Id', 'title', 'Edit','Print', 'Project', 'Delete'];
  constructor() {
      }

  ngOnInit() {
    this.dsCards=new MatTableDataSource<Card>(this.Data);
    this.dsCards.sort = this.sort;
  }

  editCard(id: any) {
    this.CardSelected.emit(id);
  }

  printCard(id: any) {
    this.CardRequestPrint.emit(id)
  }

  cardToProject(id: any) {
    this.CardRequestAddToProject.emit(id);
  }

  deleteCard(id: any) {
    this.CardRequestDelete.emit(id);
    const target = this.Data.find(x => x.id === id);
    const index = this.Data.indexOf(target);
    if (index > -1) {
      this.Data.splice(index, 1);
    }
    this.table.renderRows();
  }
}
