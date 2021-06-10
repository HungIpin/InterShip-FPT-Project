import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DialogAddExamComponent } from './dialog-add-exam.component';

describe('DialogAddExamComponent', () => {
  let component: DialogAddExamComponent;
  let fixture: ComponentFixture<DialogAddExamComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DialogAddExamComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DialogAddExamComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
