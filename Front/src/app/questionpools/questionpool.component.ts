import { Component, OnInit } from '@angular/core';
import { QuestionPool } from './shared/questionpool.model';
import { QuestionPoolService } from './shared/questionpool.service';
import { MatDialog } from '@angular/material/dialog';
import { DialogAddQuestionPoolComponent } from './dialog-add-question-pool/dialog-add-question-pool.component';

@Component({
    selector: 'app-questionpool',
    templateUrl: './questionpool.component.html',
    styleUrls: ['./questionpool.component.css']
  })
  export class QuestionPoolComponent implements OnInit {
    questionpools: Array<QuestionPool>
    public Name: string = ''
    public Description: string = ''
    public Id: number
    public ParentPoolId : number
    public dataset: any[]
    constructor(private service: QuestionPoolService, public dialog: MatDialog) {
        
    }
    ngOnInit(): void {
        this.reload();
    }
    openDialog(): void {
        const dialogRef = this.dialog.open(DialogAddQuestionPoolComponent, {
          width: '500px',
          data: { Name: this.Name, Description: this.Description, Id: this.Id ,ParentPoolId : this.ParentPoolId}
        });
        dialogRef.afterClosed().subscribe(result => { 
          console.log('The dialog was closed');
          console.log(result);
          this.createNewQuestionPool(result);
        });
      }
      openDialogEdit(Id): void {
        //Tìm skill thông qua id
        const questionpool = this.questionpools.find((questionpool) => questionpool.Id == Id);
    
        console.log(questionpool);
        const dialogRef = this.dialog.open(DialogAddQuestionPoolComponent, {
          width: '500px',
          data: { id:questionpool.Id,name: questionpool.Name, description: questionpool.Description}
        });
    
        dialogRef.afterClosed().subscribe(result => {
          console.log('The dialog was closed');
          console.log(result);
          this.editQuestionPool(result);
        });
      }
      public editQuestionPool = async (questionpool) => {
        try {
          if (questionpool) {
            const result = await this.service.updateQuestionPool(questionpool.Id, questionpool);
            console.log(result);
            if (result)
              alert('Edit sucessfully');
          }
          this.reload();
        }
        catch (e) {
          alert('Edit failed');
        }
      };
    public deleteQuestionPool = async (id) => {
        try {
          const questionpool = await this.service.deleteQuestionPool(id);
          alert('delete successfully');
          this.reload();
        }
        catch (e) {
          console.log(e);
        }
    
      }    
  public createNewQuestionPool = async (questionpool) => {
    try {
      if (questionpool) {
        const result = await this.service.postQuestionPool(questionpool);
        console.log(result);
        if (result)
          alert('Add sucessfully');
      }
      this.reload();
    }
    catch (e) {
      alert('Add failed');
    }
  };
  public getQuestionPools = async () => {
    const list = await this.service.getQuestionPools() as any;

    if (list) {
      for (let i = 0; i < list.length; i++) {
        let questionpool = new QuestionPool();
        questionpool.Id = list[i].id;
        questionpool.Name = list[i].name;
        questionpool.CreatedDate=list[i].createdDate;
        questionpool.Description = list[i].description;
        questionpool.AccountId = list[i].accountId;
        questionpool.ParentPoolId = list[i].parentPoolId;
        this.questionpools.push(questionpool);
      }
    }
    console.log(this.questionpools)

    return this.questionpools;
  }
  private reload = async () => {
    this.questionpools = new Array<QuestionPool>();
    this.dataset = await this.getQuestionPools();
  }
  pushQuestionPool(id): void {
    console.log(id);
    const questionpool = this.questionpools.find((questionpool) => questionpool.Id === id);
    this.Id = questionpool.Id;
    this.Name = questionpool.Name;
    this.Description = questionpool.Description;
  }

  clearQuestionPool(): void {
    this.Name = '';
    this.Description = '';
  }
}