import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { async } from 'rxjs/internal/scheduler/async';
import { Exam } from './exam.model';
import { SettingExam } from './settingexam.model';

@Injectable({
  providedIn: 'root'
})
export class ExamsService {
  private urlAPI = 'https://localhost:44374/api/exams';
  private urlAPIse = "https://localhost:44374/api/ExamSettings";
  private urlAPIseexamid = "https://localhost:44374";

  constructor(private http: HttpClient) {

  }

  public gettruefalseidexam = async (id) => {
    try {
        const loginUrl = `${this.urlAPIseexamid}/api/ExamSettings/${id}`;
        return await this.http.get(loginUrl).toPromise();
    }
    catch (error) {
      console.log(error);
    }
  }

  getexams = async () => {
    try {
      return await this.http.get(this.urlAPI).toPromise();
    }
    catch (e) {
      console.log(e);
    }
  }

  getsettings = async () => {
    try {
      return await this.http.get(this.urlAPIse).toPromise();
    }
    catch (e) {
      console.log(e);
    }
  }
  
  postexam = async (exam: Exam) => {
    try {
      exam.createdDate = new Date();
      return await this.http.post(this.urlAPI, exam).toPromise();
    }
    catch (e) {
      console.log(e);
    }
  }

  postsetting = async (setting: SettingExam) => {
    try {
      return await this.http.post(this.urlAPIse, setting).toPromise();
    }
    catch (e) {
      console.log(e);
    }
  }
  
  deleteexams = async (id) =>{
    try {
      return await this.http.delete(this.urlAPI + "/" + id).toPromise();
    }
    catch (e) {
      console.log(e);
    }
  }
  
  //
  updatexams = async (id, exam) => {
    try {
      return await this.http.put(this.urlAPI + "/" + id, exam).toPromise();
    }
    catch (e) {
      console.log(e);
    }
  }
  
  updatesetting = async (id, setting) => {
    try {
      return await this.http.put(this.urlAPIse + "/" + id, setting).toPromise();
    }
    catch (e) {
      console.log(e);
    }
  }
}
