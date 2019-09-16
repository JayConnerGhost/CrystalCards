import { Component, OnInit, Input } from '@angular/core';
import {NgForm }from '@angular/forms';
import { ApiService } from '../../../../api.service';

@Component({
  selector: 'app-idea',
  templateUrl: './idea.component.html',
  styleUrls: ['./idea.component.css']
})
export class IdeaComponent implements OnInit {
  @Input() Title;
  @Input() Description;
  @Input() Id;
  constructor(private apiService :ApiService) { }

  ngOnInit() {
  }

  onSubmit(f)
  {
    console.log(f);
    this.apiService.updateIdea(this.Title,this.Description,this.Id);
  }
}
