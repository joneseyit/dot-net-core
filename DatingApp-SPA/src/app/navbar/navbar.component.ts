import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';


@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  model:any = {};
  constructor(private authService: AuthService) { }

  ngOnInit(): void {
  }

  login(): void {
    console.log(this.model);
    this.authService.login(this.model).subscribe(next => {
      console.log('Login successful.  Yay!');
    }, error => {
      console.log('Failed to login');
    });
  }

}
