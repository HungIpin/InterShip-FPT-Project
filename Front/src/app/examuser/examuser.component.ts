import { Component, OnInit } from '@angular/core';
import { Router, NavigationEnd, NavigationStart, ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { ExamsService } from '../exams/shared/exam.service';
import { Exam } from '../exams/shared/exam.model';
import { AuthenticationService } from '../authentication/authentication.service';
import { Account } from  '../authentication/Account';
import { ExamInCertificateService } from '../exam-in-certificate/exam-in-certificate.service';

@Component({
  selector: 'app-examuser',
  templateUrl: './examuser.component.html',
  styleUrls: ['./examuser.component.css']
})
export class ExamuserComponent implements OnInit {
  currentAccount: Account;
  constructor(private service:ExamsService,private servicebyid:ExamInCertificateService,private httpClient: HttpClient, 
    private router: Router, private route: ActivatedRoute,private authenticationService: AuthenticationService) { 
      this.authenticationService.currentAccount.subscribe(x => this.currentAccount = x);
    }

  items: Exam[];
  id = '';
  exams;
  bool = true;

  ngOnInit(): void {
    this.reload();
  }
  private reload = async () => {
   
    this.route.queryParams.subscribe(params => {
      this.id = params["id"] || null;
      console.log(this.id);
    });
    if (this.id == null)
    {
      this.items = await this.getexams();
    }
    else {
      if(this.currentAccount.role === 'user' || this.currentAccount.role === 'admin')
      {
        this.search(this.id);
      }
      else{
        this.router.navigate(['/login']);
      }
    }
  }

  public search(id) {
    this.exams = this.servicebyid.getexamincertificate(id).subscribe(  
    (data) => {
    if (data != null) {
      this.items = []
      this.items = data;
      this.bool = false;
      console.log(this.exams);
    
    }
    else {
      this.bool = true;
    }
    }
    )
    console.log(this.items);
    
  }

  public onSearch = async (id) => {
    //localStorage.setItem('search', this.namesearch);
    if(this.currentAccount.role === 'user' || this.currentAccount.role === 'admin')
    {
      this.router.navigate(['/test'], {queryParams: {id: id}});
    }
    else{
      this.router.navigate(['/login']);
    }
  }
  public getexams = async () => {
    const list = await this.service.getexams() as Exam[];
    return list;
  }

}
