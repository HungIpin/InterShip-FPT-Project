import { Component, OnInit, Inject } from '@angular/core';
import { DialogData } from '../shared/dialog-data';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { QuestionPool } from '../shared/questionpool.model';
import { QuestionPoolService } from '../shared/questionpool.service';// này sao ấy nhỉ

@Component({
  selector: 'app-dialog-add-question-pool',
  templateUrl: './dialog-add-question-pool.component.html',
  styleUrls: ['./dialog-add-question-pool.component.css']
})
export class DialogAddQuestionPoolComponent implements OnInit {
  questionpools: Array<QuestionPool>
  constructor(
    public dialogRef: MatDialogRef<DialogAddQuestionPoolComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData, public service: QuestionPoolService) {

    this.getQuesstionPool();
    data.createdDate = new Date();
    data.parentPoolId = null;
    data.accountId = 1;
  }

  async getQuesstionPool() {
    try {
      const res = await this.service.getQuestionPools();

      if (!res) {
        this.questionpools = new Array<QuestionPool>();
      }
      else {
        this.questionpools = res as Array<QuestionPool>;
      }

      console.log(this.questionpools);
    }
    catch (e) {
      console.log(e);
    }
  }

  onNoClick(): void {
    this.dialogRef.close();
  }
  onChange(event) {
    this.data.parentPoolId = event.value;
  }

  ngOnInit(): void {
  }
}
