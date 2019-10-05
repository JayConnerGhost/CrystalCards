import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { MatDialog } from '@angular/material/dialog';
import { ApiService } from 'src/app/api.service';
import { OpenForAddCardComponent } from '../open-for-add-card/open-for-add-card.component';

@Component({
  selector: 'app-add-card',
  templateUrl: './add-card.component.html',
  styleUrls: ['./add-card.component.css']
})
export class AddCardComponent implements OnInit {
  selected : any="Brain_Storming";
  constructor(private toastr: ToastrService,private apiService: ApiService, public dialog: MatDialog) { }

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
      let dialogRef = this.dialog.open(OpenForAddCardComponent,
        {
         width: '800px',
        data:{
            },
        panelClass : "formFieldWidth550"
      });

    }
  }

}
