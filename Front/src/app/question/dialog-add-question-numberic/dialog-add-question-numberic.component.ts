import { Component, OnInit, Inject } from '@angular/core';
import { DialogData } from '../shared/dialog-data';
import { MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';

@Component({
  selector: 'app-dialog-add-question-numberic',
  templateUrl: './dialog-add-question-numberic.component.html',
  styleUrls: ['./dialog-add-question-numberic.component.css']
})
export class DialogAddQuestionNumbericComponent implements OnInit {

  constructor(
    public dialogRef: MatDialogRef<DialogAddQuestionNumbericComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData) { }

    onNoClick(): void {
      this.dialogRef.close();
    }
  
    ngOnInit(): void {
    }
}
