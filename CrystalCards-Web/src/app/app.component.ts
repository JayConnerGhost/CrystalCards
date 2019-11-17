import { Component, OnInit } from "@angular/core";
import { ApiService } from "./services/api.service";
import { MatDialog } from "@angular/material";
import { OpenForAddCardComponent } from "./cards/open-for-add-card/open-for-add-card.component";
import { CardsService } from "./services/cards.service";
import { AuthService } from "./services/auth.service";
import { JwtHelperService } from "@auth0/angular-jwt";

import { ToolbarComponent } from './toolbar/toolbar.component';

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
  styleUrls: ["./app.component.css"]
})
export class AppComponent implements OnInit {
  jwtHelper = new JwtHelperService();
  constructor(
    private apiService: ApiService,
    private cardService: CardsService,
    private authService: AuthService,
    public dialog: MatDialog
  ) {}

  ngOnInit() {
    const token = localStorage.getItem('token');
    if (token) {
      this.authService.decodedToken = this.jwtHelper.decodeToken(token);
    }
  }

  onResize($event) {

  }
}
