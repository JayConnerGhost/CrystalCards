import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {AuthService} from '../services/auth.service';
import {AlertifyService} from '../services/alertify.service';
import {COMMA, ENTER} from '@angular/cdk/keycodes';
import {Router} from '@angular/router';

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
              private alertifyService: AlertifyService,
              public router: Router) {

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

  public submitForm() {
    const Username = this.loginForm.get('Username');
    const Password = this.loginForm.get('Password');
    this.auth.login(Username.value, Password.value).subscribe(() => {
      this.alertifyService.success('Login successful');
      this.router.navigate(['']);

    },
      error => {
        this.alertifyService.error('Login Failed');
      });
  }

 reactiveForm() {
   this.loginForm = this.fb.group({
     Username: ['',
       [
         Validators.required,
         Validators.minLength(4)

       ]],
     Password: ['', [Validators.required]],
   });
 }
}

