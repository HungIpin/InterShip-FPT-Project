import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule,ReactiveFormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { RouterModule } from '@angular/router';
import { AppRoutingModule } from './app.routing';

import { AppComponent } from './app.component';
import { SignupComponent } from './signup/signup.component';
import { LandingComponent } from './landing/landing.component';
import { ProfileComponent } from './profile/profile.component';
import { HomeComponent } from './home/home.component';
import { NavbarComponent } from './shared/navbar/navbar.component';
import { AuthNavbarComponent } from './shared/authorized-navbar/authorized-navbar.component';
import { FooterComponent } from './shared/footer/footer.component';

import { HomeModule } from './home/home.module';
import { LoginComponent } from './login/login.component';
import { CertificationsComponent } from './certifications/certifications.component';
import { SkillsComponent } from './skills/skill.component';
import { UsersComponent } from './users/user.component';
import { ExamsComponent } from './exams/exams.component';

import { HttpClientModule } from '@angular/common/http';
import { SkillsService } from './skills/shared/skill.service';
import { DialogNewSKillComponent } from './skills/dialog-new-skill/dialog-new-skill.component';
import { DialogNewUserComponent } from './users/dialog-new-user/dialog-new-user.component';
import { DialogAddExamComponent } from './exams/dialog-add-exam/dialog-add-exam.component';
import { MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatSelectModule } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatRadioModule } from "@angular/material/radio";
import { MatButtonModule} from '@angular/material/button';
import { MatTooltipModule} from '@angular/material/tooltip';
import { MatRippleModule} from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker'; 

import { MatCheckboxModule} from '@angular/material/checkbox';
import { DialogCertificationComponent } from './certifications/dialog-certification/dialog-certification.component';


//Datatble, tránh xa tao ra
import {DataTablesModule} from 'angular-datatables';
//Duration picker
import { DurationPickerModule } from 'ngx-duration-picker';

import { ScoresComponent } from './score/score.component';
import { QuestionComponent } from './question/question.component';

import { DialogAddPartComponent } from './question/dialog-add-part/dialog-add-part.component';
import { DialogAddQuestionComponent } from './question/dialog-add-question/dialog-add-question.component';
import { DialogAddQuestionNumbericComponent } from './question/dialog-add-question-numberic/dialog-add-question-numberic.component';
import { DialogAddQuestionTrueFalseComponent } from './question/dialog-add-question-true-false/dialog-add-question-true-false.component';
import { DialogSettingExamComponent } from './exams/dialog-setting-exam/dialog-setting-exam.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { QuestionPoolComponent } from './questionpools/questionpool.component';
import { DialogAddQuestionPoolComponent } from './questionpools/dialog-add-question-pool/dialog-add-question-pool.component';

import { CKEditorModule } from 'ngx-ckeditor';
import { SearchComponent } from './search/search.component';
import {MatNativeDateModule} from '@angular/material/core';
import { NgxMatFileInputModule } from '@angular-material-components/file-input';
import { DatePipe } from '@angular/common';
import { QuizexamComponent } from './quizexam/quizexam.component';
import { ExamInCertificateComponent } from './exam-in-certificate/exam-in-certificate.component';
import { TestexamComponent } from './testexam/testexam.component';
import { DialogSubmitComponent } from './testexam/dialog-submit/dialog-submit.component';
import { ResultTestExamComponent } from './testexam/result-test-exam/result-test-exam.component';
import { ExamuserComponent } from './examuser/examuser.component';

@NgModule({
  declarations: [
    AppComponent,
    SignupComponent,
    LandingComponent,
    ProfileComponent,
    NavbarComponent,
    AuthNavbarComponent,
    FooterComponent,
    LoginComponent,
    CertificationsComponent,
    SkillsComponent,
    UsersComponent,
    QuestionComponent,
    DialogNewSKillComponent,
    DialogCertificationComponent,
    DialogNewUserComponent,
    DialogAddExamComponent,
    ScoresComponent,
    ExamsComponent,
    DialogAddPartComponent,
    DialogAddQuestionComponent,
    DialogAddQuestionNumbericComponent,
    DialogAddQuestionTrueFalseComponent,
    DialogSettingExamComponent,
    DashboardComponent,
    QuestionPoolComponent,
    DialogAddQuestionPoolComponent,
    SearchComponent,
    QuizexamComponent,
    ExamInCertificateComponent,
    TestexamComponent,
    DialogSubmitComponent,
    ResultTestExamComponent,
    ExamuserComponent
  ],
  imports: [
    BrowserModule,
    NgbModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule,
    AppRoutingModule,
    HomeModule,
    HttpClientModule,
    MatDialogModule,
    BrowserAnimationsModule,
    MatSelectModule,
    MatFormFieldModule,
    MatInputModule,
    MatRadioModule,
    MatCheckboxModule,
    MatButtonModule,
    MatTooltipModule, 
    MatRippleModule,
    DataTablesModule,
    DurationPickerModule,
    CKEditorModule,
    MatDatepickerModule,
    MatNativeDateModule,
    NgxMatFileInputModule

  ],
  entryComponents: [
    DialogNewSKillComponent
  ],
  providers: [SkillsService, {
    provide: MatDialogRef,
    useValue: {}
  },
  DatePipe
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
