import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-idea',
  templateUrl: './idea.component.html',
  styleUrls: ['./idea.component.css']
})
export class IdeaComponent implements OnInit {
  @Input() Title;
  @Input() Description;
  @Input() Id;
  constructor() { }

  ngOnInit() {
  }

}
