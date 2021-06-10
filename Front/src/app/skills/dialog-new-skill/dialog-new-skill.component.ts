import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { DialogData } from '../shared/DialogData';

@Component({
  selector: 'app-dialog-new-skill',
  templateUrl: './dialog-new-skill.component.html',
  styleUrls: ['./dialog-new-skill.component.css']
})
export class DialogNewSKillComponent implements OnInit {
  mess: string;
  isEdit: boolean;

  constructor(
    public dialogRef: MatDialogRef<DialogNewSKillComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData) { }

  onNoClick(): void {
    this.dialogRef.close();
  }

  handleFileInput(files: FileList) {
    console.log(files.item(0));
    if (files.item(0))
      this.data.Image = files.item(0);  //Lấy file uplload hình gán vào biến
  }

  ngOnInit(): void 
  {
    if (JSON.stringify(this.data.Image) != '""')
    {
      var base64 = JSON.stringify(this.data.Image).substr(1, JSON.stringify(this.data.Image).length - 2);
      var blob: any = this.base64ToBlob(base64, "image/" + this.getImageMime(base64));
      blob.lastModifiedDate = new Date();
      blob.name = this.data.Name + "/" + this.getImageMime(base64);
      this.data.Image =  <File>blob;
      this.mess = "Edit Skill";
      this.isEdit = true;
    }
    else 
    {
      this.mess = "Add Skill";
      this.isEdit = false;
    }
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
