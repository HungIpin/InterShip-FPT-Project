import { Component, OnInit } from '@angular/core';
import { Router, NavigationEnd, NavigationStart, ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Exam } from './quiz';
import { ArrayPartName } from './arraypart';
import { QuestionChoice } from './questionschoice';
import { Question } from '../question/shared/question.model';
import { QuizexamService } from'./quizexam.service';
import { stringify } from 'querystring';
import { ExamPart, QuestionsInPart } from '../testexam/model';
import { SafeHtml, DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-quizexam',
  templateUrl: './quizexam.component.html',
  styleUrls: ['./quizexam.component.css']
})
export class QuizexamComponent implements OnInit {
  id = '';
  items: Exam[];
  bool = true;
  quizs;
  partcount: number;
  arraypart: Array<ArrayPartName> = [];
  part: Exam[];
  questions: Question[];
  countquestion:number = 0;

  choice:Array<QuestionChoice>;

  check:boolean;
  nameserach:ArrayPartName;
  codin:boolean = false;

  total: number;
  examParts: ExamPart[];
  disableNext: boolean;
  disablePrev: boolean;
  count: number;

  constructor(private route: ActivatedRoute,private service: QuizexamService, private router: Router, private sanitizer: DomSanitizer) {
  }

  async ngOnInit(): Promise<void> 
  {
    this.route.queryParams
    .subscribe(params => {
      this.id = params["id"] || null;
    });

    this.count = 0;
    await this.initExam(this.id);  
    this.countquestion = this.examParts[0].questionsInParts.length;
  }

  public onpart(event: any) 
  {
    var name = event.target.value;
    for (let i = 0; i < this.examParts.length; i++) 
    {
      const element = this.examParts[i];
      if (element.name == name) {
        this.count = i;
        this.countquestion = element.questionsInParts.length;
      }
     
    }
   
  }

  async initExam(id: string)
  {
    this.disablePrev = true;
    this.disableNext = false;

    this.examParts = [];
    this.quizs = await this.service.getexamquuestion(id);

    this.total = this.quizs.examParts.length;



    this.quizs.examParts.forEach(element => {     
      let part = new ExamPart();
      part.id = element.id;
      part.name = element.name;
      part.examId = element.examId;
      part.questionsInParts = [];

      element.questionsInParts.forEach((e: QuestionsInPart) => {
        part.questionsInParts.push(e)
      });

      this.examParts.push(part);
     
    });

    console.log(this.examParts);
  }
  


  // public getQuiz = async (id) => {
  //   this.quizs = await this.service.getexamquuestion(id);

  //   var dem = this.quizs.examParts.length;

  //   this.partcount = dem

    
  //   if(this.codin == false){
  //     for(let i = 0;i<dem;i++)
  //     {
  //       let a = new ArrayPartName();
  //       var b = this.quizs.examParts[i].name;
  //       a.name = parseInt(b.substring(5,6));
  //       a.id = this.quizs.examParts[i].id;
  //       var count = this.quizs.examParts[i].questionsInParts.length;
  //       let c;
  //       for(let j = 0;j<count;j++)
  //       {
  //         if(j == 0){
  //           let b = this.quizs.examParts[i].questionsInParts[j].id;
  //           c = b;
  //         }
  //         else{
  //           let b = this.quizs.examParts[i].questionsInParts[j].id;
  //           c = c + "," + b;
  //         }        
  //       }
  //       a.idquestion = c;
  //       console.log(a);
  //       this.arraypart.push(a); 
  //       console.log(this.arraypart);
        
  //       this.codin = true;
  //     }
  
  //   }
    
    
  //   var t = this.quizs.examParts.find(x => x.id === this.nameserach.id);
  //   console.log(this.nameserach.id);
  //   this.part = t;
  //   this.questions = t.questionsInParts;
  //   var dem = t.questionsInParts.length;
   
  //   this.countquestion = t.questionsInParts.length;
   
    

  //   var letchoice = this.nameserach.idquestion.split(",")
    
    
  //   console.log(letchoice);
    
    
  //   letchoice.forEach(e => {
  //     var c = t.questionsInParts.find(x => x.id === parseInt(e)).question.questionChoices.length
  //     console.log(c);
  //     for(let i = 0 ;i<c;i++){
  //       let q = new QuestionChoice();
        
  //       q.id = t.questionsInParts.find(x => x.id === parseInt(e)).question.questionChoices[i].id;
  //       q.choice = t.questionsInParts.find(x => x.id === parseInt(e)).question.questionChoices[i].choice;
  //       q.isCorrect = t.questionsInParts.find(x => x.id === parseInt(e)).question.questionChoices[i].isCorrect;
  //       q.questionId = t.questionsInParts.find(x => x.id === parseInt(e)).question.questionChoices[i].questionId;
        
  //       this.choice.push(q);
  
        
  //     }
  //   })

  //   console.log(this.choice);

  //   console.log(this.quizs);
  // }
  getHTML(html: string): SafeHtml
  {
    return this.sanitizer.bypassSecurityTrustHtml(html);
  }
}
