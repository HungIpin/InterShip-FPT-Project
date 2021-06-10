import { NgModule } from '@angular/core';
import { CommonModule, } from '@angular/common';
import { BrowserModule  } from '@angular/platform-browser';
import { Routes, RouterModule } from '@angular/router';

import { HomeComponent } from './home/home.component';
import { ProfileComponent } from './profile/profile.component';
import { SignupComponent } from './signup/signup.component';
import { LandingComponent } from './landing/landing.component';
import { LoginComponent } from './login/login.component';
import { CertificationsComponent } from './certifications/certifications.component';
import { SkillsComponent } from './skills/skill.component';
import { UsersComponent } from './users/user.component';
import { QuestionComponent} from './question/question.component';
import { ScoresComponent} from './score/score.component';
import { ExamsComponent } from './exams/exams.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { QuestionPoolComponent } from './questionpools/questionpool.component';
import { SearchComponent } from './search/search.component';
import { QuizexamComponent } from './quizexam/quizexam.component';
import { ExamInCertificateComponent } from './exam-in-certificate/exam-in-certificate.component';
import { TestexamComponent } from './testexam/testexam.component';
import { ResultTestExamComponent } from './testexam/result-test-exam/result-test-exam.component';
import { ExamuserComponent } from './examuser/examuser.component';
const routes: Routes =[
    { path: 'home',             component: HomeComponent },
    { path: 'user-profile',     component: ProfileComponent },
    { path: 'register',           component: SignupComponent },
    { path: 'landing',          component: LandingComponent },
    { path: 'login',          component: LoginComponent },
    { path: 'certifications',          component: CertificationsComponent },
    { path: 'skills',          component: SkillsComponent },
    { path: 'users',          component: UsersComponent },
    { path: 'scores',          component: ScoresComponent },
    { path: 'questions',          component: QuestionComponent },
    { path: 'exams',          component: ExamsComponent },
    { path: 'dashboard',          component: DashboardComponent },
    { path: 'questionpool', component: QuestionPoolComponent},
    { path: 'search',          component: SearchComponent },
    { path: 'quizexam',          component: QuizexamComponent },
    { path: 'examincertificate',          component: ExamInCertificateComponent },
    { path: 'test',          component: TestexamComponent },
    { path: 'result',          component: ResultTestExamComponent },
    { path: 'examuser',          component: ExamuserComponent },
    { path: '', redirectTo: 'home', pathMatch: 'full' }
];

@NgModule({
  imports: [
    CommonModule,
    BrowserModule,
    RouterModule.forRoot(routes,{
      useHash: true
    })
  ],
  exports: [
  ],
})
export class AppRoutingModule { }
