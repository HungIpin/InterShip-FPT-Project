import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
//import { DialogData } from '../exams/shared/DialogData';
import { Exam } from '../shared/exam.model';
import { Certification } from '../shared/exam.model';
import { HttpClient } from '@angular/common/http';
@Component({
  selector: 'app-dialog-add-exam',
  templateUrl: './dialog-add-exam.component.html',
  styleUrls: ['./dialog-add-exam.component.css']
})
export class DialogAddExamComponent implements OnInit {

  private urlAPI = "https://localhost:44374/api/Certifications";
  certifications: Certification[]
  constructor(
    public dialogRef: MatDialogRef<DialogAddExamComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Exam, private httpClient: HttpClient) { }

  ngOnInit(): void {
    this.convertToISO();
    this.certifications = [];
    this.httpClient.get(this.urlAPI).subscribe((data: Certification[])=>{
      this.certifications = data;
    });
    if (this.data.id == '') this.isEdit = false;
    else this.isEdit = true;
  }
  myDuration: string;
  isEdit: boolean;

  convertToMinute(): number
  {
    let split: string[];
    let hour: string = '0';
    let minute: string = '0';
    let sub = this.myDuration.substr(2, this.myDuration.length - 1);
    if(this.myDuration.includes('H')) 
    {
      split = sub.split("H");
      hour = split[0];
    }
    if (this.myDuration.includes('M')) 
    {
      if (split != null && split.length > 0 ) minute = split[1].split("M")[0];
      else minute = sub.split("M")[0];
    }

    let duration = 60 * Number(hour) + Number(minute);
    return duration;
  }
  convertToISO(): void
  {
    let hour = Math.floor(this.data.duration / 60);
    let minute = this.data.duration % 60;
    this.myDuration = "PT" + hour.toString() + "H" + minute.toString() + "M"; 
  }
  getDuration(): void
  {
    this.data.duration = this.convertToMinute();
  }
}
