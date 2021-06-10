import { Component, OnInit, ViewChild, ChangeDetectorRef, OnDestroy } from '@angular/core';
import { SkillsService } from '../skills/shared/skill.service';
import { NgbRatingConfig } from '@ng-bootstrap/ng-bootstrap';
import { Skill } from '../skills/shared/skill.model';
import { async } from 'rxjs/internal/scheduler/async';
import { MatDialog } from '@angular/material/dialog';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DialogNewSKillComponent } from './dialog-new-skill/dialog-new-skill.component';
import { DomSanitizer } from '@angular/platform-browser';
import { Subject } from 'rxjs';
import { DataTableDirective } from 'angular-datatables';
//import {MatDialog, MatDialogConfig} from '@angular/material/dialog';
@Component({
  selector: 'app-skills',
  templateUrl: './skill.component.html',
  styleUrls: ['./skill.component.css']
})
export class SkillsComponent implements OnInit, OnDestroy {
  skills: Array<Skill>
  //nap du lieu cho nay
  settings: Object
  //Two way binding
  public Name: string = ''
  public Description: string = ''
  public ID: string = ''
  public Image: File
  public dataset: any[]

  dtOptions: DataTables.Settings = {};
  dtTrigger: Subject<any> = new Subject();
  @ViewChild(DataTableDirective) dtElement: DataTableDirective;
  
  constructor(private service: SkillsService, public dialog: MatDialog,private sanitizer: DomSanitizer, private chRef : ChangeDetectorRef) {
  }
  ngOnDestroy(): void {
    this.dtTrigger.unsubscribe();
  }

  async ngOnInit(): Promise<void> {
    this.dtOptions = {
      pagingType: 'full_numbers',
      lengthMenu: [4, 8, 12],
      autoWidth: true };
    await this.reload();
    this.chRef.detectChanges();
    this.dtTrigger.next();
  }


//add
  openDialog(): void {
    const dialogRef = this.dialog.open(DialogNewSKillComponent, {
      width: '250px',
      data: { Name: this.Name, Description: this.Description, Id: this.ID }
    });

    dialogRef.afterClosed().subscribe(result => { 
      console.log('The dialog was closed');
      console.log(result); //{Name: "123213", Description: "321312"}
      this.createNewSkill(result);

    });
  }
//edit
  openDialog2(Id): void {
    //Tìm skill thông qua id
    const skill = this.skills.find((skill) => skill.Id == Id);

    console.log(skill);
    const dialogRef = this.dialog.open(DialogNewSKillComponent, {
      width: '250px',
      data: { Name: skill.Name, Description: skill.Description, Id: skill.Id, Image : skill.Image}
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      console.log(result); //{Name: "123213", Description: "321312"}
      this.editSkill(result);
    });
  }

  pushData(id): void {
    console.log(id);
    const skill = this.skills.find((skill) => skill.Id === id);
    this.ID = skill.Id;
    this.Name = skill.Name;
    this.Description = skill.Description;
    this.Image = skill.Image; 
  }
  clearSkill(id): void {
    console.log(id);
    this.ID = '';
    this.Name = '';
    this.Description = '';
    this.Image = null;
  }

  private reload = async () => {
    this.skills = new Array<Skill>();
    this.dataset = await this.getSkills();
  }

  public getSkills = async () => {
    const list = await this.service.getSkills() as any;

    if (list) {
      for (let i = 0; i < list.length; i++) {
        let skill = new Skill();
        skill.Id = list[i].id;
        skill.Name = list[i].name;
        skill.Image = list[i].image;
        skill.Description = list[i].description;
        this.skills.push(skill);
      }
    }
    console.log(this.skills)
    return this.skills;
  }
  // public deleteSkill = async (id) => {
  //   try {
  //     const skill = await this.service.deleteSkills(id);
  //     alert('delete successfully');
  //     this.reload();
  //   }
  //   catch (e) {
  //     console.log(e);
  //   }

  // }

  public createNewSkill = async (skill) => {
    try 
    {
      if (skill) 
      {
        const result: any = await this.service.postSkills(skill);
        console.log(result);
        if (result) 
        {
          alert('Add sucessfully');
          let data: Skill = {
            Id: result.id,
            Name: result.name,
            Description: result.description,
            Image: result.image,
          }
          this.skills.push(data);
          this.rerender();
        }
      }
      
    }
    catch (e) {
      alert('Add failed');
    }
  };

  public editSkill = async (skill) => {
    try 
    {
      if (skill) 
      {
        const result = await this.service.updateSkills(skill.Id, skill) as any;
        console.log(result);
        if (result)
        {
          alert('Edit sucessfully');
          this.skills.forEach(element => 
          {
            if (element.Id == result.id) 
            {
              element.Name = result.name;
              element.Description = result.description;
              element.Image = result.image;
            }
          });
          this.rerender();
        }
      }   
    }
    catch (e) {
      alert('Edit failed');
    }
  };

  public deleteSkill = async (Id: string) => {
    try 
    {     
      var r = confirm("Are you sure you want to delete this skill?");
      if (r)
      {
        await this.service.deleteSkills(Id);
        this.skills = this.skills.filter(s => s.Id != Id);
        this.rerender();
      }      
    }
    catch (e) {
      console.log(e);
    }

  }
  public createSkill = async () => {
    try {
      let skill = new Skill();
      skill.Id = this.ID;
      skill.Name = this.Name;
      skill.Description = this.Description;

      const result = await this.service.postSkills(skill);
      console.log(result);
      alert('Add sucessfully');
      this.reload();
    }
    catch (e) {
      console.log(e);
    }
  }
  //update yêu cầu: id và object cần sửa
  // 1 nút là create 1 nút update, update đúng mà nhỉ
  public updateSkill = async () => {
    try 
    {
      let skill = new Skill();
      skill.Id = this.ID;
      skill.Name = this.Name;
      skill.Description = this.Description;
      skill.Image = this.Image;
      const result = await this.service.updateSkills(skill.Id, skill);
      console.log(result);
      alert('Update sucessfully');
      this.reload();
    }
    catch (e) {
      console.log(e);
    }
  }
  getImageMime(base64: string): string
  {
    if (base64.charAt(0)=='/') return 'jpg';
    else if (base64.charAt(0)=='R') return "gif";
    else if(base64.charAt(0)=='i') return 'png';
    else return 'jpeg';
  }
  getImageSource(base64: string): string
  {
    return `data:image/${this.getImageMime(base64)};base64,${base64}`; 
  }

  rerender() 
  {
    this.dtElement.dtInstance.then((dtInstance : DataTables.Api) => 
    {
      dtInstance.destroy();
      this.dtTrigger.next();
    });
  }
}