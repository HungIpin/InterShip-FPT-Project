import { Component, OnInit, ViewChild, ChangeDetectorRef, OnDestroy } from '@angular/core';
import { UsersService } from './shared/user.service';
import { AuthenticationService } from '../authentication/authentication.service';
import { NgbRatingConfig } from '@ng-bootstrap/ng-bootstrap';
import { User } from './shared/user.model';
import { DialogData } from '../users/shared/DialogData';
import { Account } from '../authentication/Account';
import { async } from 'rxjs/internal/scheduler/async';
import { MatDialog } from '@angular/material/dialog';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DialogNewUserComponent } from './dialog-new-user/dialog-new-user.component';
import { Exam } from '../exams/shared/exam.model';
import { Subject } from 'rxjs';
import { DataTableDirective } from 'angular-datatables';
import { timeStamp } from 'console';
//import {MatDialog, MatDialogConfig} from '@angular/material/dialog';
@Component({
  selector: 'app-users',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UsersComponent implements OnInit, OnDestroy {
  dialogData: Array<DialogData>
  users: Array<User>
  //nap du lieu cho nay
  settings: Object
  //Two way binding
  public ID: string = ''
  public FirstName: string = ''
  public LastName: string = ''
  public Sex: string = ''
  public DoB: Date
  public Email: string = ''
  public Phone: string = ''
  public Country: string = ''
  isEdit: boolean;
  dialogUser: User;

  public dataset: User[]

  //Two way binding
  public AccountID: string;
  public Username: string;
  public Password: string;
  public Role: string = ''
  public Active: boolean;

  constructor(private service: UsersService, private accService: AuthenticationService, public dialog: MatDialog, private chRef: ChangeDetectorRef) {
  }
  ngOnDestroy(): void {
    this.dtTrigger.unsubscribe();
  }

  dtOptions: DataTables.Settings = {};
  dtTrigger: Subject<any> = new Subject();
  @ViewChild(DataTableDirective) dtElement: DataTableDirective;

  async ngOnInit(): Promise<void> {

    this.dtOptions = {
      pagingType: 'full_numbers',
      lengthMenu: [4, 8, 12],
      autoWidth: true
    };

    await this.reload();
    this.chRef.detectChanges();
    this.dtTrigger.next();
  }

  openDialog(): void {
    const dialogRef = this.dialog.open(DialogNewUserComponent, {
      width: '400px',
      data: { id: this.ID, firstName: this.FirstName, lastName: this.LastName, accountId: this.AccountID, sex: this.Sex, doB: this.DoB, email: this.Email, phone: this.Phone, country: this.Country, userName: this.Username, password: this.Password, role: this.Role }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result.id != null) {
        console.log('The dialog was closed');

        result.doB = this.convertDateTimeCSharp(result.doB);

        if (!this.isEdit) this.createNewUser(result);
        else this.updateUser(result);
      }
      this.reload();
    });
  }

  convertDateTimeCSharp(raw) {
    let date = raw;

    const day = date.getDate();       // yields date
    const month = date.getMonth() + 1;    // yields month (add one as '.getMonth()' is zero indexed)
    const year = date.getFullYear();  // yields year
    const hour = date.getHours();     // yields hours 
    const minute = date.getMinutes(); // yields minutes
    const second = date.getSeconds(); // yields seconds

    // After this construct a string with the above results as below
    const time = day + "/" + month + "/" + year + " " + hour + ':' + minute + ':' + second;
    console.log('time: ', time);

    return time;
  }

openDialog2(id): void {
  const user = this.users.find((user) => user.id === id);
  console.log(this.accService.currentAccountValue.id);
  console.log(user);
  const dialogRef = this.dialog.open(DialogNewUserComponent, {
    width: '400px',
    data: {
      id: this.ID, firstName: this.FirstName, lastName: this.LastName, sex: this.Sex, doB: this.DoB, email: this.Email, phone: this.Phone, country: this.Country, userName: this.Username, password: this.Password, role: this.Role,
      accountId: this.AccountID
    }
  });

  dialogRef.afterClosed().subscribe(result => {
    if (result.id != null) {
      console.log('The dialog was closed');
      result.doB = this.convertDateTimeCSharp(result.doB);
      console.log(result);
      this.updateUser(result);
    }
    this.reload();
  });
}

editUser(user: User): void {
  this.isEdit = true;
  this.dialogUser = {
    id: user.id,
    firstName: user.firstName,
    lastName: user.lastName,
    sex: user.sex,
    doB: user.doB,
    email: user.email,
    phone: user.phone,
    country: user.country,
    accountId: user.accountId,
    account: user.account,
    image: null
  }
    this.openDialog();
}

clearData() {
  this.ID = ''
  this.FirstName = ''
  this.LastName = ''
  this.Sex = ''
  this.DoB = null
  this.Email = ''
  this.Phone = ''
  this.Country = ''
  this.AccountID = null
  this.Username = ''
  this.Password = ''
  this.Role = ''
}


  private reload = async () => {
  this.users = new Array<User>();
  this.dataset = await this.getusers();
}


  public getusers = async () => {
  const list = await this.service.getusers() as User[];
  if (list) {
    for (let i = 0; i < list.length; i++) {
      let user = new User();
      user.id = list[i].id;
      user.firstName = list[i].firstName;
      user.lastName = list[i].lastName;
      user.doB = list[i].doB;
      user.sex = list[i].sex;
      user.phone = list[i].phone;
      user.email = list[i].email;
      this.accService.currentAccountValue.id // nhuw nay
      //tương tự ghi tiếp 
      this.users.push(user);
    }
    return list;
  }
}

  public deleteUser = async (id) => {
  var r = confirm("Are you sure you want to delete this user?");
  if (r) {
    try {
      const user = await this.service.deleteusers(id);
      alert('delete successfully');
      await this.reload();
      this.rerender();
    }
    catch (e) {
      console.log(e);
    }
  }

}
rerender() {
  this.dtElement.dtInstance.then((dtInstance: DataTables.Api) => {
    dtInstance.destroy();
    this.dtTrigger.next();
  });
}


pushData(id): void {
  console.log(id);
  const user = this.users.find((user) => user.id === id);
  this.ID = id;
  this.FirstName = user.firstName,
  this.LastName = user.lastName,
  this.Sex = user.sex,
  this.DoB = user.doB,
  this.Email = user.email,
  this.Phone = user.phone,
  this.Country = user.country,
  this.AccountID = this.accService.currentAccountValue.id.toString(),//is this
  this.Username = '',
  this.Password = '',
  this.Role = ''
}


  public createNewUser = async (user) => {
  try {

    let account = new Account();
    account.username = user.userName;
    account.password = user.password;
    account.role = user.role;
    account.isactive = true;

    const result: any = await this.service.addAccount(account);// rồi đó :v

    let usernew = new User();
    usernew.firstName = user.firstName;
    usernew.lastName = user.lastName;
    usernew.doB = user.doB;
    usernew.phone = user.phone;
    usernew.email = user.email;
    usernew.sex = user.sex;
    usernew.accountId = result.id;
    const result2 = await this.service.postuser(usernew);
    console.log(result);
    console.log(result2);
    alert('Add sucessfully');
    this.reload();
  }
  catch (e) {
    alert('Add failed');
  }
};

  //Mới nhớ lại method ko cho phép chắc ý họ kêu mình chưa đủ tham số
  //update yêu cầu: id và object cần sửa
  // 1 nút là create 1 nút update
  public updateUser = async (user) => {
  try {

    const result = await this.service.updateusers(user.id, user);
    console.log(result);
    alert('Update sucessfully');
    this.reload();
  }
  catch (e) {
    console.log(e);
  }
}
async blockUser(user: User) {
  var r = confirm("Are you sure you want to block this user?");
  if (r) {
    user.account.isActive = false;
    await this.service.blockuser(user.accountId, user.account);
    await this.reload();
    this.rerender();
  }
}
async unBlockUser(user: User) {
  var r = confirm("Are you sure you want to unblock this user?");
  if (r) {
    user.account.isActive = true;
    await this.service.blockuser(user.accountId, user.account);
    await this.reload();
    this.rerender();
  }
}
}