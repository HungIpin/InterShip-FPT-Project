import { Component, OnInit, Inject } from '@angular/core';
import { DialogData } from '../shared/dialog-data';
import { MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';

@Component({
  selector: 'app-dialog-add-part',
  templateUrl: './dialog-add-part.component.html',
  styleUrls: ['./dialog-add-part.component.css']
})
export class DialogAddPartComponent implements OnInit {

  constructor(
    public dialogRef: MatDialogRef<DialogAddPartComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData) { }

    onNoClick(): void {
      this.dialogRef.close();
    }
  
    ngOnInit(): void {
    }

}
