import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
    providedIn: 'root'
  })
  export class ScoresService {
    private urlAPI = 'https://localhost:44374/api/ExamScores';
  
    constructor(private http: HttpClient) {
  
    }
  
    getScores = async () => {
      try {
        return await this.http.get(this.urlAPI).toPromise();
      }
      catch (e) {
        console.log(e);
      }
    }
    deleteScores = async(examID)=>{
      try {
        return await this.http.delete(this.urlAPI + examID).toPromise();
      }
      catch (e) {
        console.log(e);
      }
    }
  }