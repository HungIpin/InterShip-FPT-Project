import { TestBed } from '@angular/core/testing';

import { ExamInCertificateService } from './exam-in-certificate.service';

describe('ExamInCertificateService', () => {
  let service: ExamInCertificateService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ExamInCertificateService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
