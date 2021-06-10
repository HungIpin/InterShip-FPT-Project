import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../authentication/authentication.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  public username;
  public password;

  a:boolean;
  constructor(
    private authenticationService: AuthenticationService,
    private router: Router
  ) { }

  ngOnInit(): void {
  }

  login = () => {
    console.log(this.username + this.password);
    this.authenticationService.login(this.username, this.password).subscribe(
      (data) => {
        console.log(data);
        console.log(data.isActive);
        this.a = data.isActive;
        if (this.a) {
          if (data != null && data.username) {
            localStorage.setItem('username', data.username);
            localStorage.setItem('password', data.password);
            console.log(this.authenticationService);
            console.log('Login Success');
            if (this.authenticationService.currentAccountValue.role == "user")
              this.router.navigateByUrl('home');
            else
              this.router.navigateByUrl('dashboard');
  
          }
          else {
            console.log('Login fail');
          }
        }
        else {
          
          this.authenticationService.logout();
            alert('Lock data');
        }
        
      },
      (error) => console.error(error)
    )
  }
}
