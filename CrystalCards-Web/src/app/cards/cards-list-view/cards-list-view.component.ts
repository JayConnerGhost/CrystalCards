import {Component, EventEmitter, Input, OnInit, Output, ViewChild} from '@angular/core';
import {MatTable} from "@angular/material/table";

@Component({
  selector: 'app-cards-list-view',
  templateUrl: './cards-list-view.component.html',
  styleUrls: ['./cards-list-view.component.css']
})
export class CardsListViewComponent implements OnInit {
  @ViewChild('table',null) table: MatTable<any>;
  @Input() Data;
  @Output() CardSelected = new EventEmitter<number>();
  @Output() CardRequestDelete = new EventEmitter<number>();
  @Output() CardRequestPrint = new EventEmitter<number>();
  @Output() CardRequestAddToProject = new EventEmitter<number>();

  displayedColumns: string[] = ['Id', 'Title', 'Edit','Print', 'Project', 'Delete'];
  constructor() { }

  ngOnInit() {

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
