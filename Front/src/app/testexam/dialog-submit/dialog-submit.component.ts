import { Component, OnInit,Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { Router, NavigationEnd, NavigationStart } from '@angular/router';
import { DialogData } from '../../testexam/dialogdata';

@Component({
  selector: 'app-dialog-submit',
  templateUrl: './dialog-submit.component.html',
  styleUrls: ['./dialog-submit.component.css']
})
export class DialogSubmitComponent implements OnInit {


  constructor(
    public dialogRef: MatDialogRef<DialogSubmitComponent>,private router: Router,
    @Inject(MAT_DIALOG_DATA) public data: DialogData)
  {

  }
  onNoClick(): void {
    this.data.Name = false;
    localStorage.setItem('data', this.data.Name.toString());
    this.dialogRef.close();
  } 

  onSubmitClick():void {
    this.data.Name = true;
    localStorage.setItem('data', this.data.Name.toString());

    this.dialogRef.close();
    //this.router.navigate(['/result']);
    
  } 

  ngOnInit(): void {
  }

}
