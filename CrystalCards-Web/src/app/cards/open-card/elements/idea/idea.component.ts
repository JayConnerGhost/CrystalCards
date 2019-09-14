import { Component, OnInit, Input } from '@angular/core';
import {NgForm }from '@angular/forms';

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

  onSubmit(f)
  {
    console.log(f);
  }
}
