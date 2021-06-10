import { Component, OnInit } from '@angular/core';
import { Certification, Exam } from './model';
import { HttpClient } from '@angular/common/http';
import { Router, NavigationEnd, NavigationStart } from '@angular/router';

declare var jquery:any;

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.scss']
    
})

export class HomeComponent implements OnInit {
    model = {
        left: true,
        middle: false,
        right: false
    };
    namesearch;
    certificateURL = "https://localhost:44374/api/certifications/top";
    examURL = "https://localhost:44374/api/exams/top";
    examURLNew = "https://localhost:44374/api/exams/new";
    focus;
    focus1;
    constructor(private httpClient: HttpClient, private router: Router) { }

    certificates: Certification[];
    examsnew: Exam[];
    exams: Exam[];

    async ngOnInit() {
        this.certificates = [];
        await this.getTopCertifications();
        this.exams = [];
        await this.getTopExams();
        this.examsnew = [];
        await this.getNewExams();
    }
    getImageMime(base64: string)
    {
        if (base64.charAt(0)=='/') return 'jpg';
        else if (base64.charAt(0)=='R') return "gif";
        else if(base64.charAt(0)=='i') return 'png';
        else return 'jpeg';
    }
    async getTopCertifications() 
    {
        this.certificates = await this.httpClient.get<Certification[]>(this.certificateURL).toPromise();
        this.certificates.forEach(element => {
            var extension = this.getImageMime(element.image);
            element.base64 = `data:image/${extension};base64,${element.image}`;            
        });
    }
    async getNewExams() 
    {
        this.examsnew = await this.httpClient.get<Exam[]>(this.examURLNew).toPromise();
    }
    async getTopExams() 
    {
        this.exams = await this.httpClient.get<Exam[]>(this.examURL).toPromise();
    }


    public onSearch = async () => {
        //localStorage.setItem('search', this.namesearch);
        this.router.navigate(['/search'], {queryParams: {name: this.namesearch}});
      }
}
