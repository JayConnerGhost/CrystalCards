import { Component, OnInit,Output, EventEmitter } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-add-card',
  templateUrl: './add-card.component.html',
  styleUrls: ['./add-card.component.css']
})
export class AddCardComponent implements OnInit {
  selected : any="Brain_Storming";
  @Output() OpenAddDialog = new EventEmitter<void>();
  constructor(private toastr: ToastrService) { }

  ngOnInit() {
  }

  addIdea()
  {
      this.OpenAddDialog.emit();
  }

}
