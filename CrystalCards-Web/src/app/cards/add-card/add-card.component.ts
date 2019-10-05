import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-add-card',
  templateUrl: './add-card.component.html',
  styleUrls: ['./add-card.component.css']
})
export class AddCardComponent implements OnInit {
  selected : any="Brain_Storming";
  constructor(private toastr: ToastrService) { }

  ngOnInit() {
  }

  addIdea()
  {
    if(this.selected=="detail"){
      this.toastr.success("This is not implemented yet !! - coming soon ");
    }
    else{
      //implement dialog for brainstorming
      //open dialog
      //load component
      
    }
  }

}
