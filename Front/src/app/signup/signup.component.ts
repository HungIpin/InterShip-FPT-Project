import { Component, OnInit } from '@angular/core';
import { Account } from '../authentication/Account';
import { SignupService } from './signup.service';
import { UsersService } from '../users/shared/user.service';
import { Router, NavigationEnd, NavigationStart } from '@angular/router';
import { User } from '../users/shared/user.model';

@Component({
    selector: 'app-signup',
    templateUrl: './signup.component.html',
    styleUrls: ['./signup.component.scss']
})
export class SignupComponent implements OnInit {
    test : Date = new Date();
    focus;
    focus1;
    focus2;

  //Two way binding
  public Username:string;
  public Password:string;
  public Role: string = ''
  public Active: boolean;


  constructor( private router: Router,private service: SignupService, private userservice: UsersService) {
    let account = new Account();
  }

  ngOnInit(): void {
    
  }

  public createNewAccount = async () => {
    try {
      let account = new Account();
      account.username = this.Username;
      account.password = this.Password;
      alert(this.Username + this.Password);
      account.role = "user";
      account.isactive = true;
      const result = await this.service.addAccount(account) as any;
      this.userservice.addSignupUser(result.id);
      console.log(result.id);
      alert('Add sucessfully');
     
      this.router.navigateByUrl('/login');
      
      
    }
    catch (e) {
      alert('Add failed');
    }
  };
}
