import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  // variable to toggle on or off the form
  registerMode = false;

  constructor() { }

  ngOnInit() {
  }

  // method to toggle register variable when someone clicks on
  // the register button;
  registerToggle() {
    this.registerMode = !this.registerMode;
  }

}
