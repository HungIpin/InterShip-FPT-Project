import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Exam } from '../../app/quizexam/quiz';
import {BehaviorSubject, Observable} from 'rxjs'
import { AuthenticationService } from '../authentication/authentication.service';
import { QuestionsInPart } from './model';

@Injectable({
  providedIn: 'root'
})
export class TestexamService {

  private currentExamQuestionSubject: BehaviorSubject<Exam>;
  public currentExamQuestion : Observable<Exam >;

  private urlAPI = 'https://localhost:44374';

  constructor(private  http:HttpClient, private authenticationService: AuthenticationService)
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
      const getUrl = `${this.urlAPI}/api/Exams/GetExamTest/${id}`;
      return await this.http.get(getUrl).toPromise();
    }
    catch (error) {
      console.log(error);
    }
  }
  public postExam = async (exam: QuestionsInPart[]) => {
    try 
    {
      const url = `${this.urlAPI}/api/ExamHistories/${this.authenticationService.currentAccountValue.id}`;
      
      
      return await this.http.post(url, exam).toPromise();
    }
    catch (error) {
      console.log(error);
    }
  }
}
