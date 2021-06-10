import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})
export class QuestionService {

  private urlAPI = 'https://localhost:44374/api/Questions/';
  constructor(private http: HttpClient) { }

  getQuestions = async () => {
    try {
      return await this.http.get(this.urlAPI + "").toPromise();
    }
    catch (e) {
      console.log(e);
    }
  }

  postQuestion = async (question) => {
    try 
    {
      console.log(question);
      question.questionSetting.createdDate = new Date();
      return await this.http.post(this.urlAPI, question).toPromise();
    }
    catch (e) {
      console.log(e);
    }
  }
  deleteQuestion= async(id)=>{
    try {
      return await this.http.delete(this.urlAPI + id).toPromise();
    }
    catch (e) {
      console.log(e);
    }
  }
  //
  updateQuestion = async (id, questions) => {
    try {
      return await this.http.put(this.urlAPI + id, questions).toPromise();
    }
    catch (e) {
      console.log(e);
    }
  }

}


