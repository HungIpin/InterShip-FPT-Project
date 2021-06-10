import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { async } from 'rxjs/internal/scheduler/async';
import { User } from './user.model';
import { Account } from '../../authentication/Account'; // thiếu cái này
import * as moment from 'moment';

@Injectable({
  providedIn: 'root'
})
export class UsersService {
  private urlAPI = 'https://localhost:44374/api/users';
  private scoreAPI = 'https://localhost:44374/api/examscores';
  private urlAPIaccount = 'https://localhost:44374';


  constructor(private http: HttpClient) {

  }

  gethighestScore = async (id) => {
    try {
      return await this.http.get(this.scoreAPI +'/top/'+id).toPromise();
    }
    catch (e) {
      console.log(e);
    }
  }

  gettakenTimes = async (id) => {
    try {
      return await this.http.get(this.scoreAPI +'/counts/'+id).toPromise();
    }
    catch (e) {
      console.log(e);
    } 
  }

  getusers = async () => {
    try {
      return await this.http.get(this.urlAPI).toPromise();
    }
    catch (e) {
      console.log(e);
    }
  }

  getuser = async(id) =>{
    try 
    {
      const user = await this.http.get(this.urlAPI + "/" + id).toPromise();
      return user;
    }
    catch (e) {
      console.log(e);
    }
  }

  postuser = async (user: User) => {
    try 
    {
      const formData: FormData = new FormData();
      /*if (user.Image)
      {
        formData.append('image-upload', certifications.Image);
      }*/
      formData.append('firstName',user.firstName);
      formData.append('lastName',user.lastName);
      formData.append('doB',(user.doB).toString());
      formData.append('email',user.email);
      formData.append('phone',user.phone);
      formData.append('sex',user.sex);
      formData.append('accountId',(user.accountId).toString());

      console.log(formData);
      const loginUrl = `${this.urlAPIaccount}/api/Users`;
      return await this.http.post(loginUrl, formData).toPromise();
    }
    catch (e) {
      console.log(e);
    }
  }

  deleteusers = async(id) =>{
    try {
      return await this.http.delete(this.urlAPI + "/" + id).toPromise();
    }
    catch (e) {
      console.log(e);
    }
  }
  
  //
  updateusers = async (id, user) => {
    try 
    {
      const formData: FormData = new FormData();
      user.doB = moment(user.doB).format("MM/DD/YYYY");
      formData.append('id',user.id);
      formData.append('firstName',user.firstName);
      formData.append('lastName',user.lastName);
      formData.append('sex',user.sex);
      formData.append('doB',user.doB);

      formData.append('email',user.email);
      formData.append('country',user.country);
      formData.append('phone', user.phone);
      formData.append('accountId', user.accountId);
    
      if (user.image) {
        formData.append('image-upload', user.image);
      }
  
      console.log(formData);
      return await this.http.put(this.urlAPI + "/" + id, formData).toPromise();
    }
    catch (e) {
      console.log(e);
    }
  }
  public addAccount = async (account: Account) => {
    try {
        const loginUrl = `${this.urlAPIaccount}/api/Accounts`;
        return await this.http.post(loginUrl, account).toPromise();
    }
    catch (error) {
      console.log(error);
    }
  }
  blockuser = async (id, user) => {
    try 
    {
      return await this.http.put('https://localhost:44374/api/accounts/' + id, user).toPromise();
    }
    catch (e) {
      console.log(e);
    }
  }
  addSignupUser = async (id) => {
    try 
    {
      const formData: FormData = new FormData();
      formData.append('firstName','');
      formData.append('lastName','');
      formData.append('sex','');
      formData.append('doB','1-1-1999');

      formData.append('email','');
      formData.append('country','Vietnam');
      formData.append('phone', '');
      formData.append('accountId', id);
      formData.append('image-upload','');
    
  
      console.log(formData);
      return await this.http.post(this.urlAPI, formData).toPromise();
    }
    catch (e) {
      console.log(e);
    }
  }
}
