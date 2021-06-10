import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DialogSettingExamComponent } from './dialog-setting-exam.component';

describe('DialogSettingExamComponent', () => {
  let component: DialogSettingExamComponent;
  let fixture: ComponentFixture<DialogSettingExamComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DialogSettingExamComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DialogSettingExamComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
