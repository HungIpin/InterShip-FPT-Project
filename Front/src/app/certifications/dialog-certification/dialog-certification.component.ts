import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { DialogData } from '../shared/dialog-data';
import { SkillsService } from '../../skills/shared/skill.service'
import { skillDBmodel } from './model-dropbox'
import { SkillinCertification} from '../shared/certification.model';
import * as $ from 'jquery';

@Component({
  selector: 'app-dialog-certification',
  templateUrl: './dialog-certification.component.html',
  styleUrls: ['./dialog-certification.component.css']
})
export class DialogCertificationComponent implements OnInit {
  skills: skillDBmodel[]
  mess: string
  isEdit: boolean;
  constructor(
    public dialogRef: MatDialogRef<DialogCertificationComponent>,
    private skillService: SkillsService,
    @Inject(MAT_DIALOG_DATA) public data: DialogData) { }
    
    onNoClick(): void {
    this.dialogRef.close();
  }
  handleFileInput(files: FileList) {
    console.log(files.item(0));
    if (files.item(0))
      this.data.Image = files.item(0);  //Lấy file uplload hình gán vào biến
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

  public getSkills = async () => {
    const list = await this.skillService.getSkills() as any;
    // this.skills = list;
    // sessionStorage.setItem("skill", JSON.stringify(list));
    if (list) 
    {
      for (let i = 0; i < list.length; i++) {
        let skill = new skillDBmodel();
        skill.Id = list[i].id;
        skill.Name = list[i].name;
        this.skills.push(skill);
      }
    }
  }

  async ngOnInit(): Promise<void> {
    if (this.data.Id != "")
    {
      this.mess = "Edit Certificate";
      this.isEdit = true;
      if (JSON.stringify(this.data.Image) != '""' && this.data.Image != null)
      {
        var base64 = JSON.stringify(this.data.Image).substr(1, JSON.stringify(this.data.Image).length - 2);
        var blob: any = this.base64ToBlob(base64, "image/" + this.getImageMime(base64));
        blob.lastModifiedDate = new Date();
        blob.name = this.data.Name + "/" + this.getImageMime(base64);
        this.data.Image =  <File>blob;
        this.mess = "Edit Certificate";
        this.isEdit = true;
      }
    }   
    else {
      this.mess = "Add Certificate";
      this.isEdit = false;
    }
    this.skills = [];
    await this.getSkills();
    sessionStorage.setItem("skills", JSON.stringify(this.skills))
  }
  onChange(event: any)
  {
    let target = event.source.selected._element.nativeElement;
    let value = event.value;
    let text = target.innerText.trim();
    let skillinCer: SkillinCertification;
    skillinCer = this.data.SkillinCertifications.find(m => m.skillId == value && m.certificationId == this.data.Id);
    if (!skillinCer)
    {
      skillinCer = {skillId: value, certificationId: this.data.Id, name: text};
      this.data.SkillinCertifications.push(skillinCer);
    }    
  }
  deleteSkill(Id: string)
  {
    // var button = event.target as Element;
    // var Id = button.id;
    // button.closest("div").remove();
    this.data.SkillinCertifications = this.data.SkillinCertifications.filter(m => m.skillId != Id && m.certificationId == this.data.Id)
  }
}
