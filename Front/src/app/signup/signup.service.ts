import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { async } from 'rxjs/internal/scheduler/async';
import { Account } from '../authentication/Account';


@Injectable({
  providedIn: 'root'
})
export class SignupService {
  private urlAPI = 'https://localhost:44374';

  constructor( private http: HttpClient) { }

  public addAccount = async (account: Account) => {
    try {
        const loginUrl = `${this.urlAPI}/api/Accounts`;
        return await this.http.post(loginUrl, account).toPromise();
    }
    catch (error) {
      console.log(error);
    }
  }

}
