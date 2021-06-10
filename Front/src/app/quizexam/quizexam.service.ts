import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Exam } from './quiz';
import {map} from 'rxjs/operators';
import {BehaviorSubject, Observable} from 'rxjs'

@Injectable({
  providedIn: 'root'
})
export class QuizexamService {

  private currentExamQuestionSubject: BehaviorSubject<Exam>;
  public currentExamQuestion : Observable<Exam >;

  private urlAPI = 'https://localhost:44374';

  constructor(private  http:HttpClient)
  {
    this.currentExamQuestionSubject = new BehaviorSubject<Exam >(
        JSON.parse(localStorage.getItem('currentExamQuestion'))
    );
    this.currentExamQuestion  = this.currentExamQuestionSubject.asObservable();
  }
  public get currentProductValue(): Exam {
      return this.currentExamQuestionSubject.value;
  }  

  public getexamquuestion = async (id: string) => {
    try {
      const getUrl = `${this.urlAPI}/api/Exams/GetExamTestAdmin/${id}`;
      return await this.http.get(getUrl).toPromise();
    }
    catch (error) {
      console.log(error);
    }
  }
}
