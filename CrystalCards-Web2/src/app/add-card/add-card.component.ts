import { Component, OnInit } from '@angular/core';
import {FormGroup, FormBuilder, Validators} from '@angular/forms';
import {AlertifyService} from '../services/alertify.service';
import {COMMA, ENTER} from '@angular/cdk/keycodes';
import {MatDialogRef} from '@angular/material/dialog';
import {CardService} from '../services/card.service';
import {SignalingService} from '../services/signaling.service';

@Component({
  selector: 'app-add-card',
  templateUrl: './add-card.component.html',
  styleUrls: ['./add-card.component.css']
})
export class AddCardComponent implements OnInit {
  visible = true;
  selectable = true;
  removable = true;
  addOnBlur = true;
  readonly separatorKeysCodes: number[] = [ENTER, COMMA];
  addCardForm: FormGroup;

  constructor(public fb: FormBuilder,
              private alertifyService: AlertifyService,
              private cardService: CardService,
              private signalingService: SignalingService,
              public dialogRef: MatDialogRef<AddCardComponent>) { }

  ngOnInit(): void {
    this.reactiveForm();
  }

  public errorHandling = (control: string, error: string) => {
    return this.addCardForm.controls[control].hasError(error);
  }
  if(input) {
    input.value = '';
  }

  submitForm() {
    const Title = this.addCardForm.get('Title').value;
    const Description = this.addCardForm.get('Description').value;
    this.cardService.New(Title, Description).subscribe(()=>{
      this.signalingService.refreshData();
      this.alertifyService.success('Idea Created');
    },
      error => {
      this.alertifyService.error('An error occured');
      });

  }
  reactiveForm() {
    this.addCardForm = this.fb.group({
      Description: ['', [Validators.required]],
      Title: ['', [Validators.required]],
    });
  }

  close() {
    this.dialogRef.close();

  }
}
