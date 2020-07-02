import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup} from '@angular/forms';
import {AuthService} from '../services/auth.service';
import {AlertifyService} from '../services/alertify.service';
import {COMMA, ENTER} from '@angular/cdk/keycodes';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  visible = true;
  selectable = true;
  removable = true;
  addOnBlur = true;
  loginForm: FormGroup;
  readonly separatorKeysCodes: number[] = [ENTER, COMMA];

  constructor(public fb: FormBuilder,
              public auth: AuthService,
              private alertifyService: AlertifyService) {

  }
  if(input) {
    input.value = '';
  }
  ngOnInit(): void {
    this.reactiveForm();
  }
  public errorHandling = (control: string, error: string) => {
    return this.loginForm.controls[control].hasError(error);
  }

  public submitForm() {}

  public reactiveForm(){

  }

}

