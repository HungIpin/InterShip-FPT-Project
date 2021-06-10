import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Account } from './Account';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map } from 'rxjs/operators';
//import { Console } from 'console';

@Injectable({
    providedIn: 'root',
})

export class AuthenticationService {
   private currentAccountSubject: BehaviorSubject<Account>;
   public currentAccount: Observable<Account>;
   private urlAPI = 'https://localhost:44374';
   constructor(private http: HttpClient) {
     this.currentAccountSubject = new BehaviorSubject<Account>(
     JSON.parse(localStorage.getItem('currentAccount'))
   );
    this.currentAccount = this.currentAccountSubject.asObservable();
   }
  
   public get currentAccountValue(): Account {
        return this.currentAccountSubject.value;
   }

  
   public login = (username: string, password: string) => {
            console.log(username);
            console.log(password);
            const loginUrl = `${this.urlAPI}/api/Accounts/Signin`;
            console.log(loginUrl);
            return this.http
            .post<any>(loginUrl, {
                username,
                password
        })
        .pipe(
        map((account) => {
        // console.log(user);
            if (account != null){
                const newAccount = {} as Account;
                newAccount.id = account.id;
                newAccount.username = account.username;
                newAccount.password = account.password;
                newAccount.role = account.role;
                newAccount.isactive = account.isActive;
                localStorage.setItem('currentAccount', JSON.stringify(newAccount));
                this.currentAccountSubject.next(newAccount);
                return account;
            } 
            else {
                return null;
            }
        })
        );
   }
  
   public logout = () => {
    localStorage.removeItem('username');
    localStorage.removeItem('password');
    localStorage.removeItem('currentAccount');
    this.currentAccountSubject.next(null);
   }
}