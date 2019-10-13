import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { AlertifyService } from '../services/alertify.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
 model:any ={};

  constructor(private authService: AuthService,
              private alertify: AlertifyService,) { }

  ngOnInit() {
  }
register(){
 this.authService.register(this.model).subscribe(()=>{
   this.alertify.success("Registration succesful");
 }, error=> {
   this.alertify.error("registraton failed");
 });
}

}
