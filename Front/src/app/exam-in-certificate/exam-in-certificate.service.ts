import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Exam } from '../exams/shared/exam.model';
import {map} from 'rxjs/operators';
import {BehaviorSubject, Observable} from 'rxjs'

@Injectable({
  providedIn: 'root'
})
export class ExamInCertificateService {

  private currentExamSubject: BehaviorSubject<Exam>;
  public currentExam: Observable<Exam>;

  private urlAPI = 'https://localhost:44374';

  constructor(private  http:HttpClient)
  {
    this.currentExamSubject = new BehaviorSubject<Exam>(
        JSON.parse(localStorage.getItem('currentExam'))
    );
    this.currentExam = this.currentExamSubject.asObservable();
  }
  public get currentProductValue(): Exam{
      return this.currentExamSubject.value;
  }  

  public getexamincertificate = (id: string) => {
    const getUrl = `${this.urlAPI}/api/Exams/Getexamincertificate/:id`;
    const x=getUrl.replace(":id",id);
        return this.http.get<any>(x).pipe(
            map((exams) => {
                if(exams != null)
                {
                    const getexam = [];
                    exams.forEach(element => {
                        getexam.push(element);
                        this.currentExamSubject.next(element);
                    });
                    return getexam;
                }
                else{
                    return null;
                }
        })
        )
  }
}
