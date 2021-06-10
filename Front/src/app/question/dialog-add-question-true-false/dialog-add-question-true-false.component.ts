import { Component, OnInit, Inject } from '@angular/core';
import { DialogData, QuestionChoice, QuestionAttachment } from '../shared/dialog-data';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ThemePalette } from '@angular/material/core';
import { QuestionPoolService } from '../../questionpools/shared/questionpool.service';
export interface Task {
  name: string;
  completed: boolean;
  color: ThemePalette;
  subtasks?: Task[];
}
@Component({
  selector: 'app-dialog-add-question-true-false',
  templateUrl: './dialog-add-question-true-false.component.html',
  styleUrls: ['./dialog-add-question-true-false.component.css']
})
export class DialogAddQuestionTrueFalseComponent implements OnInit {
  questionpools: any[]
  task: Task = {
    name: 'All Correct',
    completed: false,
    color: 'primary',
    subtasks: [
      { name: 'A', completed: false, color: 'primary' },
      { name: 'B', completed: false, color: 'primary' },
      { name: 'C', completed: false, color: 'primary' },
      { name: 'D', completed: false, color: 'primary' },
    ]
  }
  allComplete: boolean = false;
  answer: boolean;

  choiceA: QuestionChoice;
  choiceB: QuestionChoice;

  someComplete(): boolean {
    if (this.task.subtasks == null) {
      return false;
    }
    return this.task.subtasks.filter(t => t.completed).length > 0 && !this.allComplete;
  }

  setAll(checked: boolean) {
    this.data.questionChoices.forEach(element => {
      element.isCorrect = checked;
    });
  }
  constructor(
    public dialogRef: MatDialogRef<DialogAddQuestionTrueFalseComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData, private service: QuestionPoolService) {   
    
  }
  handleFileInput(files: FileList) {
    let attachment: QuestionAttachment = {
      name: files.item(0).name,
      questionId: this.data.id,
      attachment: this.fileToByteArray(files.item(0))
    }
    this.data.questionAttachments.push(attachment);
  }
  onNoClick(): void {
    this.dialogRef.close();
  }

  fileToByteArray(file: any) {
    return new Promise((resolve, reject) => {
        try {
            let reader = new FileReader();
            let fileByteArray = [];
            reader.readAsArrayBuffer(file);
            reader.onloadend = (evt) => 
            {
              if (evt.target.readyState == FileReader.DONE) 
              {
                let array = new Uint8Array(<ArrayBuffer>evt.target.result);
                for (var i = 0; i < array.length; i++) fileByteArray.push(array[i]);
              }
              resolve(fileByteArray);
            }
        }
        catch (e) { reject(e);} 
    })
  }
  
  async ngOnInit(): Promise<void> 
  {
    if (this.data.questionText == '')
    {
      this.choiceA = {
      questionId: this.data.id,
      choice: 'True',
      isCorrect: true }

      this.choiceB = {
        questionId: this.data.id,
        choice: 'False',
        isCorrect: false }
      
      this.data.questionChoices.push(this.choiceA);
      this.data.questionChoices.push(this.choiceB);

      this.answer = true;
    }
    else
    {
      if (this.data.questionChoices[0].isCorrect) this.answer = true;
      else this.answer = false;
    }
    await this.getQuestionPool();
  }
  async getQuestionPool() {
    try 
    {
      const res = await this.service.getQuestionPools();
      if (res) 
      {
        this.questionpools = res as any[];
      }
    }
    catch (e) {
      console.log(e);
    }
  }
  update()
  {
    if (this.answer) 
    {
      this.data.questionChoices[0].isCorrect = true;
      this.data.questionChoices[1].isCorrect = false;
    }
    else 
    {
      this.data.questionChoices[0].isCorrect = false;
      this.data.questionChoices[1].isCorrect = true;
    }
  }
}

