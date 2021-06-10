import { Component, OnInit, ViewChild, ChangeDetectorRef, OnDestroy } from '@angular/core';
import { Certification, SkillinCertification } from './shared/certification.model';
import { CertificationService } from'./shared/certification.service';
import { SkillsService } from'../skills/shared/skill.service';
import { MatDialog } from '@angular/material/dialog';
import { DialogCertificationComponent } from './dialog-certification/dialog-certification.component';
import { JsonPipe } from '@angular/common';
import { Skill } from '../skills/shared/skill.model';
import { DataTableDirective } from 'angular-datatables';
import * as $ from 'jquery';
import { Subject } from 'rxjs';
import { element } from 'protractor';
import { Router, NavigationEnd, NavigationStart } from '@angular/router';
@Component({
  selector: 'app-certifications',
  templateUrl: './certifications.component.html',
  styleUrls: ['./certifications.component.css']
})
export class CertificationsComponent implements OnInit, OnDestroy {
  certifications: Array<Certification>
  //nap du lieu cho nay
  settings: Object
  //Two way binding
  public Name: string = ''
  public Description: string = ''
  public Id: string = ''
  public Image: string = ''
  public TakenTimes: number = 0
  public Difficulty: string =''
  public SkillinCertifications: SkillinCertification[]
  public dataset: any[]

  dtOptions: DataTables.Settings = {};
  dtTrigger: Subject<any> = new Subject();
  @ViewChild(DataTableDirective) dtElement: DataTableDirective;
  
  constructor(private service: CertificationService, private skillService: SkillsService, private chRef : ChangeDetectorRef, public dialog: MatDialog,private router: Router) {}

  async ngOnInit(): Promise<void> 
  {
    this.dtOptions = {
      pagingType: 'full_numbers',
      lengthMenu: [4, 8, 12],
      autoWidth: true };


    this.clearData();
    await this.reload();
    this.chRef.detectChanges();
    this.dtTrigger.next();
  }

  private reload = async () => {
    this.certifications = new Array<Certification>();
    this.dataset = await this.getCertification();  
  }

  initCertificate(certificate: any): Certification
  {
    let certification = new Certification();
    certification.Id = certificate.id;
    certification.Name = certificate.name;
    certification.Description = certificate.description;
    certification.TakenTimes = certificate.takenTimes;
    certification.Image = certificate.image;
    certification.Difficulty = certificate.difficulty;
    certification.SkillinCertifications = [];
    certificate.skillinCertifications.forEach(element => {
      let skill: SkillinCertification = {skillId: element.skillId, name: element.skill.name, certificationId: element.certificationId}
      certification.SkillinCertifications.push(skill);
    });
    return certification;
  }
  public getCertification = async () => {
    const list = await this.service.getCertifications() as any;
    const sublist = await this.service.getDbSkill() as any;
  
    if (list) 
    {
      for (let i = 0; i < list.length; i++) 
      {  
        this.certifications.push(this.initCertificate(list[i]));
      }
    }

    return this.certifications;
  }


  openNewDialog(): void {
    const dialogRef = this.dialog.open(DialogCertificationComponent, {
      width: '250px',
      data: { Id:this.Id, Name: this.Name, Description: this.Description, TakenTimes: this.TakenTimes, Image: this.Image, Difficulty: this.Difficulty, SkillinCertifications: this.SkillinCertifications }
    });

    dialogRef.afterClosed().subscribe(result => {
        console.log('The dialog was closed');
        console.log(result);
        this.createCertification(result);
    });
  }

  openEditDialog(): void {
    const dialogRef = this.dialog.open(DialogCertificationComponent, {
      width: '250px',
      data: { Id:this.Id, Name: this.Name, Description: this.Description, TakenTimes: this.TakenTimes, Image: this.Image, Difficulty: this.Difficulty, SkillinCertifications: this.SkillinCertifications }
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
      console.log(result); //{Name: "123213", Description: "321312"}
      this.editCertification(result);
      this.clearData();
    });
  }

  public createCertification = async (certification) => {
    try 
    {
      if (certification)
      {
        let result: any = await this.service.postCertification(certification);
        if (result)
        {
          alert("Add sucessfully!")
          this.certifications.push(this.initCertificate(result));
          this.rerender();
          this.clearData();
        }
      }     
    }
    catch (e) {
      alert('Add failed');
      console.log(e)
    }
  };

  public editCertification = async (certification) => {
    try 
    {
      if (certification)
      {
        const result = await this.service.updateCertification(certification.Id, certification) as any;
        if (result) 
        {
          alert('Update sucessfully');
          this.certifications.forEach(element => 
            {
              if (element.Id == result.id) 
              {
                element.Name = result.name;
                element.TakenTimes = result.takenTimes;
                element.Description = result.description;
                element.Difficulty = result.difficulty;
                element.Image = result.image;
                element.SkillinCertifications = [];
                result.skillinCertifications.forEach(e => {
                  let skill: SkillinCertification = {skillId: e.skillId, name: e.skill.name, certificationId: e.certificationId}
                  element.SkillinCertifications.push(skill);
                });
              }
            });
          this.rerender();
        }
      }     
    }
    catch (e) {
      alert('Add failed');
    }
  };

  public deleteCertification = async (Id: string) => {
    try 
    {
      var r = confirm("Are you sure you want to delete this certificate?");
      if (r)
      {
        await this.service.deleteCertification(Id);
        this.certifications = this.certifications.filter(c => c.Id != Id);
        this.rerender();
      }
      
    }
    catch (e) {
      console.log(e);
    }

  }

  pushData(id): void {
    console.log(id);
    const certification = this.certifications.find((certification) => certification.Id === id);
    this.Id = certification.Id;
    this.Name = certification.Name;
    this.Description = certification.Description;
    this.TakenTimes = certification.TakenTimes;
    this.Image = certification.Image;
    this.Difficulty = certification.Difficulty;
    certification.SkillinCertifications.forEach(element => {
      this.SkillinCertifications.push(element);
    });
  }
  clearData(): void {
    this.Id = '';
    this.Name = '';
    this.Description = '';
    this.TakenTimes = 0;
    this.Difficulty = ''; 
    this.Image = '';
    this.SkillinCertifications = [];
  }
  getSkills(certificate: any): string
  {
    var skills: string = '';
    certificate.SkillinCertifications.forEach((element: SkillinCertification) => {
      skills += element.name + "\n";
    });
    return skills;
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
  ngOnDestroy(): void
  {
    this.dtTrigger.unsubscribe();
  }
  public onSearch = async (Id: string) => {
    //localStorage.setItem('search', this.namesearch);
    this.router.navigate(['/examincertificate'], {queryParams: {id: Id}});
  }
}
