import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ResultTestExamComponent } from './result-test-exam.component';

describe('ResultTestExamComponent', () => {
  let component: ResultTestExamComponent;
  let fixture: ComponentFixture<ResultTestExamComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ResultTestExamComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ResultTestExamComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
