import {Component, Inject, OnInit} from '@angular/core';
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


  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    public dialogRef: MatDialogRef<EditCardComponent>,
    public fb: FormBuilder,
    private alertifyService: AlertifyService,
    private cardService: CardService,
    private signalingService: SignalingService,
  ) { }

  ngOnInit(): void {
    console.log("data",this.data.IdeaId);
    console.log("data",this.data.Title);
    console.log("data",this.data.Description);
    //TODO- set up idea card
  }
  close() {
    this.dialogRef.close();

  }


  }
