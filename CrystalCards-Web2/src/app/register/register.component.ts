import { Component, OnInit, ViewChild } from '@angular/core';
import {FormGroup, FormBuilder, Validators} from '@angular/forms';
import { COMMA, ENTER } from '@angular/cdk/keycodes';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  visible = true;
  selectable = true;
  removable = true;
  addOnBlur = true;
  registerForm: FormGroup;
  readonly separatorKeysCodes: number[] = [ENTER, COMMA];
  constructor(public fb: FormBuilder) { }

  ngOnInit(): void {
    this.reactiveForm();
  }
    // Reset the input value
    if (input) {
      input.value = '';
    }
    submitForm() {
      console.log(this.myForm.value);
    }
    /* Handle form errors in Angular 8 */
    public errorHandling = (control: string, error: string) => {
      return this.registerForm.controls[control].hasError(error);
    }

    /* Reactive form */
    reactiveForm() {
      this.registerForm = this.fb.group({
        FirstName: ['', [Validators.required]],
        SecondName: ['', [Validators.required]],
        Username: [ '',
          [
            Validators.required,
            Validators.minLength(4)
          ]],
        Password: ['', [Validators.required]],




      });
}}
