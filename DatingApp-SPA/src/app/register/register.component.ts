import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  model: any = {};

  constructor() { }

  ngOnInit() {
  }

  // register method.  Go to form and set form as NgSubmit  
  // #registerForm = "ngForm" (ngSubmit)="register()" 
  register () {
    console.log(this.model);
  }

  cancel() {
    console.log('canceled');
  }

}
