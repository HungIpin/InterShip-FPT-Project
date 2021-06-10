import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Exam} from '../../app/quizexam/quiz';
import { ArrayPartName } from '../../app/quizexam/arraypart';
import { Question, QuestionChoice } from '../question/shared/question.model';
import { ExamPart, QuestionsInPart } from './model';
import { TestexamService } from './testexam.service';
import { DialogSubmitComponent } from './dialog-submit/dialog-submit.component';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { SafeHtml, DomSanitizer } from '@angular/platform-browser';


@Component({
  selector: 'app-testexam',
  templateUrl: './testexam.component.html',
  styleUrls: ['./testexam.component.css']
})
export class TestexamComponent implements OnInit, OnDestroy {

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

  check: boolean;
  nameserach: number = 0;
  codin: boolean = false;
  partname: number;
  count: number = 0;
  countpart:number = 0;
  isReadOnlyPre:string ='';
  isReadOnlyNext:string = '';

  //Quỳnh fix
  total: number;
  examParts: ExamPart[];
  disableNext: boolean;
  disablePrev: boolean;
  checkok:string;
  score:string;
  constructor(private router: Router, public dialog: MatDialog, private sanitizer: DomSanitizer, private route: ActivatedRoute, private service: TestexamService) {
  }
  ngOnDestroy(): void 
  {
    document.getElementById("timer").innerHTML = "";
  }
  
  async ngOnInit(): Promise<void> {
    this.route.queryParams
    .subscribe(params => {
      this.id = params["id"] || null;
    });
    if(this.nameserach == 0){
      this.isReadOnlyPre = 'a';
      this.isReadOnlyNext = '';
    }
   
    await this.initExam(this.id);
    this.countDown(this.quizs.duration);
  }

  public onnext() 
  {
    this.count = this.count < this.total ? this.count + 1 : this.count;
    if (this.count < this.total && this.count > 1)
    {
      this.disableNext = false;
      this.disablePrev = false;
    }
    else if (this.count == this.total) 
    {
      this.disableNext = true;
      this.disablePrev = false;
    }
  }

  public onprev() 
  {
    this.count = this.count > 1 ? this.count - 1: this.count;
    if (this.count > 1) 
    {
      this.disableNext = false;
      this.disablePrev = false;
    }
    else if (this.count == 1) 
    {
      this.disableNext = false;
      this.disablePrev = true;
    }
  }

  async initExam(id: string)
  {
    this.disablePrev = true;
    this.disableNext = false;

    this.examParts = [];
    this.quizs = await this.service.getexamquuestion(id);

    localStorage.setItem('passscore',this.quizs.passScore.toString());
    this.total = this.quizs.examParts.length;
    this.count = 1;

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


  countDown(duration: number)
  {
    var endDate = new Date(Date.now());
    endDate.setMinutes(endDate.getMinutes() + duration);
    var _this = this;
    var x = setInterval(function() {
      var distance = endDate.getTime() - Date.now();
      var hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
      var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
      var seconds = Math.floor((distance % (1000 * 60)) / 1000);

      document.getElementById("timer").innerHTML = "<div class='text-white text-right'> " + hours + "h " + minutes + "m " + seconds + "s " + "</div>";   
      if (distance < 0) 
      {
        clearInterval(x);
        document.getElementById("timer").innerHTML = "EXPIRED";
        _this.submit();
      }
    }, 1000);

  }

  update(event: any, choice: QuestionChoice)
  {
    this.examParts[this.count - 1].questionsInParts.forEach(element => {
      if (element.questionId == choice.questionId)
      {
        element.question.questionChoices.forEach(e => {
          if (e.choice == choice.choice) e.isCorrect = event.target.checked;
        });
      } 
    });
  }

  async submit()
  {
    let exam: QuestionsInPart[] = [];
    this.examParts.forEach(element => {
      element.questionsInParts.forEach((e: QuestionsInPart) => {
        exam.push(e);
      });
    });
    var result = await this.service.postExam(exam);
    console.log(result);
    this.router.navigate(['/result'], { state: { score: result, passScore: this.quizs.passScore}});
  }

  getHTML(html: string): SafeHtml
  {
    return this.sanitizer.bypassSecurityTrustHtml(html);
  }
  
  openSubmitDialog(): void {
    const dialogRef = this.dialog.open(DialogSubmitComponent, {
      width: '500px',
      data: {Name:this.checkok}
    });
    
    dialogRef.afterClosed().subscribe(() => 
    {
      console.log('The dialog was closed');
      this.checkok = localStorage.getItem('data');
      console.log(this.checkok);
      if(this.checkok == "true")
      {
        this.submit();    
      }
      else
      {
        localStorage.removeItem('data');
      }
    });
  }
}
