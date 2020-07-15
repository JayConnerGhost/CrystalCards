import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {AlertifyService} from '../services/alertify.service';
import {CardService} from '../services/card.service';
import {SignalingService} from '../services/signaling.service';

@Component({
  selector: 'app-edit-card-idea',
  templateUrl: './edit-card-idea.component.html',
  styleUrls: ['./edit-card-idea.component.css']
})
export class EditCardIdeaComponent implements OnInit {
  editCardForm: FormGroup;
  constructor(   public fb: FormBuilder,
                 private alertifyService: AlertifyService,
                 private cardService: CardService,
                 private signalingService: SignalingService,) { }

  ngOnInit(): void {
    this.reactiveForm();
  }
  public errorHandling = (control: string, error: string) => {
    return this.editCardForm.controls[control].hasError(error);
  }

  public submitForm(){

  }

  reactiveForm() {
    this.editCardForm = this.fb.group({
      Description: ['', [Validators.required]],
      Title: ['', [Validators.required]],
    });
  }


}
