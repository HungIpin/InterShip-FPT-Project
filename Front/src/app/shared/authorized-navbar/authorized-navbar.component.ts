import { Component, OnInit, Input } from '@angular/core';
import { Router, NavigationEnd, NavigationStart } from '@angular/router';
import { Location, PopStateEvent } from '@angular/common';
import { AuthenticationService } from '../../authentication/authentication.service';
import { LoginComponent } from '../../login/login.component';
import { Account } from  '../../authentication/Account';

@Component({
    selector: 'app-authorized-navbar',
    templateUrl: './authorized-navbar.component.html',
    styleUrls: ['./navbar.component.scss']
})
export class AuthNavbarComponent implements OnInit {
    public isCollapsed = true;
    private show = true;
    public username:string = '';
    private lastPoppedUrl: string;
    private yScrollStack: number[] = [];
    @Input() role = '';
    @Input() button = '';
    currentAccount: Account;

    constructor(public location: Location, private router: Router,
        private authenticationService: AuthenticationService
        
        ) {
          console.log(this.show);
          this.authenticationService.currentAccount.subscribe(x => this.currentAccount = x);
    }
    

    get isAdmin() {
      if(this.username == ''){
        this.username = this.currentAccount.username;
        this.show = true;
      }
      
      return this.currentAccount && this.currentAccount.role === 'admin';
    }

    get isUser() {
      if(this.username == ''){
        this.username = this.currentAccount.username;
        this.show = true;
      }
      
      return this.currentAccount && this.currentAccount.role === 'user';
    }
   
    ngOnInit(): void  {
      
      this.router.events.subscribe((event) => {
        this.isCollapsed = true;
        if (event instanceof NavigationStart) {
           if (event.url != this.lastPoppedUrl)
               this.yScrollStack.push(window.scrollY);
       } else if (event instanceof NavigationEnd) {
           if (event.url == this.lastPoppedUrl) {
               this.lastPoppedUrl = undefined;
               window.scrollTo(0, this.yScrollStack.pop());
           } else
               window.scrollTo(0, 0);
       }
     });
     this.location.subscribe((ev:PopStateEvent) => {
         this.lastPoppedUrl = ev.url;
     });
    }

    isHome() {
        var titlee = this.location.prepareExternalUrl(this.location.path());

        if( titlee === '/home' ) {
            return true;
        }
        else {
            return false;
        }
    }
    isDocumentation() {
        var titlee = this.location.prepareExternalUrl(this.location.path());
        if( titlee === '/documentation' ) {
            return true;
        }
        else {
            return false;
        }
    }

    public onLogout = () => {
      this.username = '';
      this.show = false;
      this.authenticationService.logout();

      window.location.reload();
      this.router.navigate(['/login']);
      
    }  
    
    public onLogin = () => {
      this.router.navigate(['/login']);
    }

    public onRegister = () => {
      this.router.navigate(['/register']);
    }  
      
}
