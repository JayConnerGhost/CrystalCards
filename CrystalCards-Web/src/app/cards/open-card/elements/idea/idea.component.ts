import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
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
  @Output() UpdatePerformed = new EventEmitter();
  constructor(private apiService: ApiService) { }

  ngOnInit() {
  }

  onSubmit(f)
  {
      this.apiService.updateIdea(this.Title, this.Description, this.Id).subscribe();
      this.UpdatePerformed.emit();
  }
}
