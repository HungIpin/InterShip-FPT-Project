import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { DialogData } from '../shared/DialogData';
import { User } from '../shared/user.model';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-dialog-new-user',
  templateUrl: './dialog-new-user.component.html',
  styleUrls: ['./dialog-new-user.component.css']
})
export class DialogNewUserComponent implements OnInit {
  mess: string;
  isEdit: boolean;

  constructor(
    public dialogRef: MatDialogRef<DialogNewUserComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData, private httpClient: HttpClient) { }

  /*onNoClick(): void {
    this.dialogRef.close();
  }*/

  ngOnInit(): void {
    if (this.data.lastName != "")
    {
      this.mess = "Edit user";
      this.isEdit = true;
      this.isEdit = true;
      if (JSON.stringify(this.data.image) != '""' && this.data.image != null)
      {
        var base64 = JSON.stringify(this.data.image).substr(1, JSON.stringify(this.data.image).length - 2);
        var blob: any = this.base64ToBlob(base64, "image/" + this.getImageMime(base64));
        blob.lastModifiedDate = new Date();
        blob.name = this.data.accountId + "/" + this.getImageMime(base64);
        this.data.image =  <File>blob;
      }
    }   
    else {
      this.mess = "Add User";
      this.isEdit = false;
    }
  }
  handleFileInput(files: FileList) {
    console.log(files.item(0));
    if (files.item(0))
      this.data.image = files.item(0);  //Lấy file uplload hình gán vào biến
  }
  base64ToBlob(base64: string, type: string) 
  {
    const binaryString = window.atob(base64);
    const len = binaryString.length;
    const bytes = new Uint8Array(len);
    for (let i = 0; i < len; ++i) {
      bytes[i] = binaryString.charCodeAt(i);
    } 
    return new Blob([bytes], { type: type });
  };
  getImageMime(base64: string): string
  {
    if (base64.charAt(0)=='/') return 'jpg';
    else if (base64.charAt(0)=='R') return "gif";
    else if(base64.charAt(0)=='i') return 'png';
    else return 'image/jpeg';
  }
}
