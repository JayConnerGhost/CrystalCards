import {Component, Input, OnInit} from '@angular/core';

@Component({
  selector: 'app-card-token',
  templateUrl: './card-token.component.html',
  styleUrls: ['./card-token.component.css']
})
export class CardTokenComponent implements OnInit {
  @Input() Title: string;
  @Input() Description: string;
  @Input() Id : number;

  constructor() { }

  ngOnInit(): void {
  }

}
