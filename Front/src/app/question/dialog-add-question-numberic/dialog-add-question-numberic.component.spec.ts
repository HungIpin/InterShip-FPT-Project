import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DialogAddQuestionNumbericComponent } from './dialog-add-question-numberic.component';

describe('DialogAddQuestionNumbericComponent', () => {
  let component: DialogAddQuestionNumbericComponent;
  let fixture: ComponentFixture<DialogAddQuestionNumbericComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DialogAddQuestionNumbericComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DialogAddQuestionNumbericComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
