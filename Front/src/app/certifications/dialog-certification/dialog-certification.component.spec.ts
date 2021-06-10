import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DialogCertificationComponent } from './dialog-certification.component';

describe('DialogCertificationComponent', () => {
  let component: DialogCertificationComponent;
  let fixture: ComponentFixture<DialogCertificationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DialogCertificationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DialogCertificationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
