import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { QuizexamComponent } from './quizexam.component';

describe('QuizexamComponent', () => {
  let component: QuizexamComponent;
  let fixture: ComponentFixture<QuizexamComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ QuizexamComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(QuizexamComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
