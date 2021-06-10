import { Component, OnInit } from '@angular/core';
import { Router, NavigationEnd, NavigationStart, ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Exam } from '../exams/shared/exam.model';
import { ExamInCertificateService } from './exam-in-certificate.service';


@Component({
  selector: 'app-exam-in-certificate',
  templateUrl: './exam-in-certificate.component.html',
  styleUrls: ['./exam-in-certificate.component.css']
})
export class ExamInCertificateComponent implements OnInit {
  
  constructor(private service:ExamInCertificateService,private httpClient: HttpClient, 
    private router: Router, private route: ActivatedRoute) { 
     
    }

items: Exam[];
id = '';
exams;
bool = true;

ngOnInit(): void {
this.route.queryParams
.subscribe(params => {
this.id = params["id"] || null;
console.log(this.id);
});
if (this.id == null)
{
  alert("Loi nha");
}
else {
  this.search(this.id);
}
}

public onSearch = async (Id: string) => {
  //localStorage.setItem('search', this.namesearch);
  
  this.router.navigate(['/quizexam'], {queryParams: {id: Id}});
}

public search(id) {
this.exams = this.service.getexamincertificate(id).subscribe(
  
  
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

}
