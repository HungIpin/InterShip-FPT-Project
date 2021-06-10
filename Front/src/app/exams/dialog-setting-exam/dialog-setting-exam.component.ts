import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
//import { DialogData } from '../exams/shared/DialogData';
import { SettingExam } from '../shared/settingexam.model';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-dialog-setting-exam',
  templateUrl: './dialog-setting-exam.component.html',
  styleUrls: ['./dialog-setting-exam.component.css']
})
export class DialogSettingExamComponent implements OnInit {

  constructor(
    public dialogRef: MatDialogRef<DialogSettingExamComponent>,
    @Inject(MAT_DIALOG_DATA) public data: SettingExam, private httpClient: HttpClient) { }

  isEdit: boolean;
  ngOnInit(): void {
    if (this.data.examId == '') this.isEdit = false;
    else this.isEdit = true;
  }

}
