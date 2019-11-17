import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor() { }

  loggedIn()
  {
    const token = localStorage.getItem('token');
    return !!token;
  }
  ngOnInit() {
  }

  loadLogin() {
    //Load login component
  }
}
