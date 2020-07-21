import {Component, Inject, OnInit, Output} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {AlertifyService} from '../services/alertify.service';
import {CardService} from '../services/card.service';
import {SignalingService} from '../services/signaling.service';
import {COMMA, ENTER} from '@angular/cdk/keycodes';

@Component({
  selector: 'app-edit-card',
  templateUrl: './edit-card.component.html',
  styleUrls: ['./edit-card.component.css']
})
export class EditCardComponent implements OnInit {
  visible = true;
  selectable = true;
  removable = true;
  addOnBlur = true;
  readonly separatorKeysCodes: number[] = [ENTER, COMMA];
  Id: number;
  Title: string;
  Description: string;
  editCardForm: any;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    public dialogRef: MatDialogRef<EditCardComponent>,
    public fb: FormBuilder,
    private alertifyService: AlertifyService,
    private cardService: CardService,
    private signalingService: SignalingService,
  ) {
  }

  ngOnInit(): void {
    console.log("pointer ", this.data.Id);
    this.Id = this.data.Id;
    this.Title = this.data.Title;
    this.Description = this.data.Description;
    this.reactiveForm();
  }

  public errorHandling = (control: string, error: string) => {
    return this.editCardForm.controls[control].hasError(error);
  }

  public submitForm() {
    console.log(this.Id);
    console.log(this.Title);
    console.log(this.Description);
  }

  public close(){

  }

  reactiveForm() {
    this.editCardForm = this.fb.group({
      Description: [this.Description, [Validators.required]],
      Title: [this.Title, [Validators.required]],
    });
  }


}

