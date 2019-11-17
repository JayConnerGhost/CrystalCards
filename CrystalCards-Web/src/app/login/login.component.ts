import {ChangeDetectorRef, Component, OnInit} from '@angular/core';
import {ApiService} from "../services/api.service";
import {CardsService} from "../services/cards.service";
import {AuthService} from "../services/auth.service";
import {Router} from "@angular/router";
import {AlertifyService} from "../services/alertify.service";
import {MatDialog} from "@angular/material/dialog";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  model:any = {};
  constructor(private apiService: ApiService,
              private cardService: CardsService,
              public authService: AuthService,
              private router: Router,
              private alertify: AlertifyService,
              public dialog: MatDialog,
              private dt: ChangeDetectorRef) { }

  ngOnInit() {
  }

  login(){
    this.authService.login(this.model).subscribe(next=>{
        this.alertify.success("successfully logged in ");
        this.dt.detectChanges();
        this.router.navigate(['/cards'])
      },
      error=>{
        this.alertify.error("failed to login");
      });
  }
}
