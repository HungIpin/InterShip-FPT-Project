import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})
export class QuestionPoolService {

  private urlAPI = 'https://localhost:44374/api/QuestionPools/';
  constructor(private http: HttpClient) { }

  getQuestionPools = async () => {
    try {
      return await this.http.get(this.urlAPI + "").toPromise();
    }
    catch (e) {
      console.log(e);
    }
  }

  postQuestionPool = async (questionpools) => {
    try {
      console.log(questionpools);
      return await this.http.post(this.urlAPI, questionpools).toPromise();
    }
    catch (e) {
      console.log(e);
    }
  }
  deleteQuestionPool= async(id)=>{
    try {
      return await this.http.delete(this.urlAPI + id).toPromise();
    }
    catch (e) {
      console.log(e);
    }
  }
  //
  updateQuestionPool = async (id, questionpools) => {
    try {
      return await this.http.put(this.urlAPI + id, questionpools).toPromise();
    }
    catch (e) {
      console.log(e);
    }
  }

}


