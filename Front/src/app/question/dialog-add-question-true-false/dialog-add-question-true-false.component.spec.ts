import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DialogAddQuestionTrueFalseComponent } from './dialog-add-question-true-false.component';

describe('DialogAddQuestionTrueFalseComponent', () => {
  let component: DialogAddQuestionTrueFalseComponent;
  let fixture: ComponentFixture<DialogAddQuestionTrueFalseComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DialogAddQuestionTrueFalseComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DialogAddQuestionTrueFalseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
