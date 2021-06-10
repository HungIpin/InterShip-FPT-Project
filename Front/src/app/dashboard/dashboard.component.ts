import { Component, OnInit } from '@angular/core';
import * as Chartist from 'chartist';
import { Certification, Exam } from '../home/model';
import { HttpClient } from '@angular/common/http';
import { User } from '../users/shared/user.model';
import { UsersService } from '../users/shared/user.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['../../assets/scss/material-dashboard.scss']
})
export class DashboardComponent implements OnInit {

  constructor(private httpClient: HttpClient, private service: UsersService) { }

  topCertificates: Certification[];

  exams: number;
  certificates: number;
  questions: number;
  users: number;

  certificateURL = "https://localhost:44374/api/certifications";
  examURL = "https://localhost:44374/api/exams";
  questionURL = "https://localhost:44374/api/questions";
  userURL = "https://localhost:44374/api/users";

  admins: User[];
  ratings: any[];

  startAnimationForBarChart(chart){
      let seq2: any, delays2: any, durations2: any;

      seq2 = 0;
      delays2 = 80;
      durations2 = 500;
      chart.on('draw', function(data) {
        if(data.type === 'bar'){
            seq2++;
            data.element.animate({
              opacity: {
                begin: seq2 * delays2,
                dur: durations2,
                from: 0,
                to: 1,
                easing: 'ease'
              }
            });
        }
      });

      seq2 = 0;
  };

  async getTopCertifications() 
  {
    this.topCertificates = await this.httpClient.get<Certification[]>(this.certificateURL + "/top").toPromise();
  }
  // async getTopExams() 
  // {
  //     this.topExams = await this.httpClient.get<Exam[]>(this.examURL + "/top").toPromise();
  // }
  
  async getExamRatings() 
  {
      this.ratings = await this.httpClient.get<any[]>(this.examURL + "/ratings").toPromise();
  }
  async getBarChartData()
  {
    return await this.httpClient.get<any>(this.certificateURL + "/countexams").toPromise();
  }
  async initCertificatePieChart()
  {
    await this.getTopCertifications();
    let labels: string[] = [];
    let series: number[] = [];
    
    this.topCertificates.forEach(element => {
      labels.push(element.name);
      series.push(element.takenTimes);
    });
    const dataTopCertificate: any = {
      labels: labels,
      series: series };

    const optionsCertificatesChart: any = {
      donut: false,
      donutWidth: 15,
      showLabel: true,
      labelOffset: 35,
      labelDirection: 'explode'
    }

    var dailySalesChart = new Chartist.Pie('#topCertificateChart', dataTopCertificate, optionsCertificatesChart);
    
  }
  async initExamPieChart()
  {
    // await this.getTopExams();
    await this.getExamRatings();
    let labels: string[] = ['★', '★★', '★★★', '★★★★', '★★★★★'];
    let series: number[] = [];
    this.ratings.forEach(element => {
      series.push(element.total);
    });
    const dataTopExam: any = {
      labels: labels,
      series: series };

    const options: any = {
      donut: false,
      donutWidth: 5,
      showLabel: true,
      labelOffset: 10,
      labelDirection: 'explode'
    }
  
    var dailySalesChart = new Chartist.Pie('#topExamPieChart', dataTopExam, options);
    
  }
  async initBarChart()
  {
    let data = await this.getBarChartData();
    let labels: string[] = [];
    let series: number[] = [];
    let sum = 0;
    data.forEach(element => {
      labels.push(element.id);
      series.push(element.numOfExams);
      sum += element.numOfExams;
    });
    const dataBarChart: any = {
      labels: labels,
      series: [series]};

      var optionswebsiteViewsChart = {
        axisX: {
            showGrid: false
        },
        low: 0,
        high: sum,
        chartPadding: { top: 0, right: 5, bottom: 0, left: 0}
    };
    var responsiveOptions: any[] = [
      ['screen and (max-width: 640px)', {
        seriesBarDistance: 5,
        axisX: {
          labelInterpolationFnc: function (value) {
            return value[0];
          }
        }
      }]
    ];
    var websiteViewsChart = new Chartist.Bar('#websiteViewsChart', dataBarChart, optionswebsiteViewsChart, responsiveOptions);
    this.startAnimationForBarChart(websiteViewsChart);
    
  }
  async initNumbers()
  {
    this.certificates = Number(await this.httpClient.get<string>(this.certificateURL + "/counts").toPromise());
    this.exams = Number(await this.httpClient.get<string>(this.examURL + "/counts").toPromise());
    this.questions = Number(await this.httpClient.get<string>(this.questionURL + "/counts").toPromise());
    this.users = Number(await this.httpClient.get<string>(this.userURL + "/counts").toPromise());
  }
  async initTables()
  {
    // this.recentExams = await this.httpClient.get<any>(this.examURL + "/recent").toPromise();
    // this.recentPools = await this.httpClient.get<any>("https://localhost:44374/api/questionpools/recent").toPromise();
    
    var listUsers = await this.service.getusers() as User[];
    this.admins = listUsers.filter(u => u.account.role == 'admin');

  }
  async ngOnInit() 
  {
    this.admins = [];
    this.ratings = [];
    await this.initNumbers();
    await this.initCertificatePieChart();
    await this.initExamPieChart();
    await this.initBarChart();
    await this.initTables();
  }
  calculateDiff(date: string): string
  {
    let d2: Date = new Date();
    let d1 = new Date(date);
    var diffMs = d2.getTime() - d1.getTime();
    var diffDays = Math.floor(diffMs / 86400000); // days
    var diffHrs = Math.floor((diffMs % 86400000) / 3600000); // hours
    var diffMins = Math.round(((diffMs % 86400000) % 3600000) / 60000); // minutes
    return diffDays + " days, " + diffHrs + " hours, " + diffMins + " minutes ago";
  }
  
}
