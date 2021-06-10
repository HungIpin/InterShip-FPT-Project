import { Component, OnInit } from '@angular/core';
import { Router, NavigationEnd, NavigationStart } from '@angular/router';

@Component({
  selector: 'app-result-test-exam',
  templateUrl: './result-test-exam.component.html',
  styleUrls: ['./result-test-exam.component.css']
})
export class ResultTestExamComponent implements OnInit{

  scoredo:string;
  passscore:string;
  Pass:string="";
  NotPass:string="";
  constructor(private router: Router) 
  { 
    this.scoredo = this.router.getCurrentNavigation().extras.state.score;
    this.passscore = this.router.getCurrentNavigation().extras.state.passScore;
  }

 
  ngOnInit(): void {
    this.Pass = "";
    this.NotPass = "";
    if(parseInt(this.scoredo) > parseInt(this.passscore))
    {
      this.Pass = "true";
    }
    else{
      this.NotPass = "false";
    }
  }

  ondone(){
    this.Pass = "";
    this.NotPass = "";
    localStorage.removeItem('totalscore');
    localStorage.removeItem('passscore');
    localStorage.removeItem('data');
    this.router.navigate(['/home']);
  }


    

}
