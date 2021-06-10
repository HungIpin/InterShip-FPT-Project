import { Component, OnInit, ViewChild, ChangeDetectorRef, OnDestroy } from '@angular/core';
import { ExamsService } from './shared/exam.service';
import { Exam } from './shared/exam.model';
import { MatDialog } from '@angular/material/dialog';
import { DialogAddExamComponent } from './dialog-add-exam/dialog-add-exam.component';
import { Subject } from 'rxjs';
import { DataTableDirective } from 'angular-datatables';
import { DialogSettingExamComponent } from './dialog-setting-exam/dialog-setting-exam.component';
import { SettingExam } from './shared/settingexam.model';
import { AuthenticationService } from '../authentication/authentication.service';
import { Router, NavigationEnd, NavigationStart } from '@angular/router';
import { Account } from  '../authentication/Account';

@Component({
  selector: 'app-exams',
  templateUrl: './exams.component.html',
  styleUrls: ['./exams.component.css']
})
export class ExamsComponent implements OnInit, OnDestroy {
  //nap du lieu cho nay
  settings: Object
  //Two way binding
  public Name: string = ''
  public Descriptions: string = ''
  public ID: string = ''
  isEdit: boolean;
  dialogExam: Exam;
  dialogSetting: SettingExam;
  Idexam: string;
  public dataset: Exam[]

  dtOptions: DataTables.Settings = {};
  dtTrigger: Subject<any> = new Subject();
  @ViewChild(DataTableDirective) dtElement: DataTableDirective;
  currentAccount: Account;
  constructor(private service: ExamsService, public dialog: MatDialog, private router: Router,private chRef : ChangeDetectorRef, private authenticationService: AuthenticationService) {
    this.authenticationService.currentAccount.subscribe(x => this.currentAccount = x);
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

  openDialog(): void {
    const dialogRef = this.dialog.open(DialogAddExamComponent, {
      width: '400px',
      data: this.dialogExam
    });

    dialogRef.afterClosed().subscribe((result: Exam) => {
      if (result)
      {
        console.log('The dialog was closed');
        console.log(result);
        if (!this.isEdit) this.createNewExam(result);
        else this.updateExam(result);
      }
        
    });
  }

  settingDialog(): void {
    const dialogRef = this.dialog.open(DialogSettingExamComponent, {
      width: '700px',
      data: this.dialogSetting
      
    });

    dialogRef.afterClosed().subscribe((result: SettingExam) => {
      if (result)
      {
        console.log('The dialog was closed');
        console.log(result);
        const kt =  this.service.gettruefalseidexam(this.Idexam);
        console.log(kt);
        if (kt) this.createNewSetting(result);
        else this.updateSetting(result);
      }
        
    });
  }

  getsettingexam(id):void{
    this.isEdit = true;
    this.Idexam = id;
    this.dialogSetting = {
      id: null,
      examId: id,
      availableDate: null,
      displayPoint: null,
      dueDate: null,
      numOfSubmissions: null,
      password: "",
      fbDisplayAfter: null,
      fbDisplayBefore: null   
    }
    
    this.settingDialog();
  }

  editExam(exam: Exam): void {

    this.isEdit = true;
    this.dialogExam = {
      id: exam.id,
      name: exam.name,
      description: exam.description,
      numOfQuestions: exam.numOfQuestions,
      passScore: exam.passScore,
      duration: exam.duration,
      rating: exam.rating,
      createdDate: exam.createdDate,
      certificationId: exam.certificationId,
      feedbackTypeId: exam.feedbackTypeId,
      feedbackLevelId: exam.feedbackLevelId,
      accountId: exam.accountId,
      scoreRecordingId: exam.scoreRecordingId,
      certification: exam.certification
    }
    this.openDialog();

  }
 
  private reload = async () => {
    this.clearData();
    this.dataset = await this.getexams();
  }

  public getexams = async () => {

    const list = await this.service.getexams() as Exam[];
    return list;

  }
  public deleteExam = async (id) => {
    var r = confirm("Are you want to delete this exam?");
    if (r)
    {
      try 
      {
        await this.service.deleteexams(id);
        alert('delete successfully');
        await this.reload();
        this.rerender();
      }
      catch (e) {
        console.log(e);
      }
    }
  }
  clearData()
  {
    this.dialogExam = {
      id: '',
      name: '',
      description: '',
      numOfQuestions: 0,
      passScore: 0,
      duration: 0,
      rating: 0,
      createdDate: null,
      certificationId: '',
      feedbackTypeId: 1,
      feedbackLevelId: 1,
      accountId: Number(this.authenticationService.currentAccountValue.id),
      scoreRecordingId: 1,
      certification: null
    }
  }

  public createNewExam = async (exam) => {
    try 
    {
      const result = await this.service.postexam(exam);
      console.log(result);
      if (result)
      {
        alert('Add sucessfully');
        //await this.reload();
        this.dataset = result as Exam[];
        this.rerender();
      }    
    }
    catch (e) {
      alert('Add failed');
    }
  };

  public createNewSetting = async (setting) => {
    try 
    {
      let settingExamnew = new SettingExam();
      settingExamnew = setting;
      settingExamnew.displayPoint = true;
      const result = await this.service.postsetting(settingExamnew);
      console.log(result);
      if (result)
      {
        alert('Add sucessfully');
        //await this.reload();
        //this.dataset = result as Exam[];
        //this.rerender();
      }    
    }
    catch (e) {
      alert('Add failed');
    }
  };

  public updateExam = async (exam: Exam) => {
    try 
    {
      const result = await this.service.updatexams(exam.id, exam);
      console.log(result);
      alert('Update sucessfully');
      await this.reload();
      //this.dataset = result as Exam[];
      this.rerender();
      // if (result)
      // {
        
      // }
     
    }
    catch (e) {
      console.log(e);
    }
  }

  public updateSetting = async (setting: SettingExam) => {
    try 
    {
      const result = await this.service.updatesetting(setting.id, setting);
      console.log(result);
      alert('Update sucessfully');
      await this.reload();
      //this.dataset = result as Exam[];
      this.rerender();
      // if (result)
      // {
        
      // }
     
    }
    catch (e) {
      console.log(e);
    }
  }

  convertToHour(duration: number): string
  {
    let hour = Math.floor(duration / 60);
    let minute = duration % 60;
    return hour.toString() + ":" + (minute < 10? "0" + minute.toString(): minute.toString()); 
  }
  rerender() 
  {
    this.dtElement.dtInstance.then((dtInstance : DataTables.Api) => 
    {
      dtInstance.destroy();
      this.dtTrigger.next();
    });
  }
  public onSearch = async (id) => {
    //localStorage.setItem('search', this.namesearch);
    if(this.currentAccount.role === 'admin')
    {
      this.router.navigate(['/quizexam'], {queryParams: {id: id}});
    }
    else{
      this.router.navigate(['/test'], {queryParams: {id: id}});
    }
  }
}
