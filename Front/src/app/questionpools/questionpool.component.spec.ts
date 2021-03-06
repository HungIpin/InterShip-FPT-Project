import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { QuestionPoolComponent } from './questionpool.component';

describe('QuestionComponent', () => {
  let component: QuestionPoolComponent;
  let fixture: ComponentFixture<QuestionPoolComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ QuestionPoolComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(QuestionPoolComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
