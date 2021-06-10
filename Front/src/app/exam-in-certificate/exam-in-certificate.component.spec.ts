import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ExamInCertificateComponent } from './exam-in-certificate.component';

describe('ExamInCertificateComponent', () => {
  let component: ExamInCertificateComponent;
  let fixture: ComponentFixture<ExamInCertificateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ExamInCertificateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ExamInCertificateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
