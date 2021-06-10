import { Component, OnInit, ViewEncapsulation, ViewChild, ChangeDetectorRef, OnDestroy } from '@angular/core';
import { Question } from './shared/question.model';
import { QuestionService } from './shared/question.service';
import { MatDialog } from '@angular/material/dialog';
import { DialogAddQuestionComponent } from './dialog-add-question/dialog-add-question.component';
import { DialogAddQuestionTrueFalseComponent } from './dialog-add-question-true-false/dialog-add-question-true-false.component';
import { DialogAddQuestionNumbericComponent } from './dialog-add-question-numberic/dialog-add-question-numberic.component';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
import { Subject } from 'rxjs';
import { DataTableDirective } from 'angular-datatables';

@Component({
  selector: 'app-question',
  templateUrl: './question.component.html',
  styleUrls: ['./question.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class QuestionComponent implements OnInit, OnDestroy {

  dtOptions: DataTables.Settings = {};
  dtTrigger: Subject<any> = new Subject();
  @ViewChild(DataTableDirective) dtElement: DataTableDirective;

  questions: Array<Question>
  settings: Object
  question: Question;
  
  public Id: number
  public QuestionTypeId: number
  public QuestionPoolId: number
  public QuestionText: string = ''
  public SelectionSettingId: number
  public PointValue : number
  public dataset: any[]
  isEdit: boolean;
  constructor(private service: QuestionService, private chRef : ChangeDetectorRef, public dialog: MatDialog, private sanitizer: DomSanitizer) {
  }
  ngOnDestroy(): void 
  {
    this.dtTrigger.unsubscribe();
  }

  async ngOnInit(): Promise<void> {

    this.dtOptions = {
      pagingType: 'full_numbers',
      lengthMenu: [4, 8, 12],
      autoWidth: true };

    await this.reload();
    this.chRef.detectChanges();
    this.dtTrigger.next();
  }

  private reload = async () => {
    this.isEdit = false;
    this.clearData();
    this.questions = await this.getQuestion();
  }

  public getQuestion = async () => {
    const listContainer = await this.service.getQuestions() as Question[];
    console.log(listContainer);
    return listContainer;
  }

  openAddQuestion(): void {
    this.question.questionTypeId = 1;
    const dialogRef = this.dialog.open(DialogAddQuestionComponent, {
      width: '600px',
      data: this.question
    });
    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      console.log(result);
      if (!this.isEdit) this.createQuestion(result);   
      else this.updateQuestion(result);
    });
  }
  openAddQuestionTrueFalse(): void {
    this.question.questionTypeId = 2;
    const dialogRef = this.dialog.open(DialogAddQuestionTrueFalseComponent, {
      width: '600px',
      data: this.question
    });
    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      console.log(result);
      if (!this.isEdit) this.createQuestion(result);   
      else this.updateQuestion(result); 
    });
  }
  openAddQuestionNumberic(): void {
    const dialogRef = this.dialog.open(DialogAddQuestionNumbericComponent, {
      width: '600px',
      data: {Id:this.Id,QuestionText:this.QuestionText,QuestionTypeId:this.QuestionTypeId}
    });
    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      console.log(result);
      this.createQuestion(result);   
    });
  }
  public createQuestion = async (question) => {
    try 
    {
      if (question)
      {
        const result = await this.service.postQuestion(question) as Question[];
        console.log(result);
        alert('Add sucessfully');
        await this.reload();
        this.rerender();
      }    
    }
    catch (e) {
      alert('Add failed');
    }
  };
  public updateQuestion = async (question) => {
    try 
    {
      if (question)
      {
        const result = await this.service.updateQuestion(question.id, question) as Question[];
        console.log(result);
        alert('Update sucessfully');
        await this.reload();
        this.rerender();
      }    
    }
    catch (e) {
      alert('Update failed');
    }
  };

  public editQuestion = async (question) => {
    try {
      if (question)
      {
      const result = await this.service.updateQuestion(question.ID,question);
      console.log(result);
      if (result)
        alert('Add sucessfully');
      this.reload();
      }
      
    }
    catch (e) {
      alert('Add failed');
    }
  };

  public deleteQuestion = async (id) => {
    var r = confirm("Are you sure you want to delete this question?");
    if (r)
    {
      try 
      {
        var result = await this.service.deleteQuestion(id);
        if (result)
        {
          alert('Delete successfully');
          this.questions = this.questions.filter(q => q.questionSetting.questionId != id);
          this.rerender();
        }
        
      }
      catch (e) {
        console.log(e);
      }
    }
  }

  pushData(question: Question): void {
    console.log(question);
    this.question = JSON.parse(JSON.stringify(question));
  }
  clearData(): void {
    this.question = {
      id: 0,
      questionText: '',
      questionPoolId: 1,
      questionTypeId: 1,
      questionChoices: [],
      questionSetting: {
        questionId: 0,
        createdDate: null,
        correctFb: '',
        inCorrectFb: '',
        displayPoint: false,
        deductedPoints: 0,
        pointValue: 0
      },
      selectionSettingId: 1,
      questionAttachments: []
    }
  }
  openEditDialog()
  {
    this.isEdit = true;
    if (this.question.questionTypeId == 1) this.openAddQuestion();
    if (this.question.questionTypeId == 2) this.openAddQuestionTrueFalse();
  }
  getHTML(html: string): SafeHtml
  {
    return this.sanitizer.bypassSecurityTrustHtml(html);
  }
  rerender() 
  {
    this.dtElement.dtInstance.then((dtInstance : DataTables.Api) => 
    {
      dtInstance.destroy();
      this.dtTrigger.next();
    });
  }
}