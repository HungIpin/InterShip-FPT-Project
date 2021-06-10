import { TestBed } from '@angular/core/testing';

import { TestexamService } from './testexam.service';

describe('TestexamService', () => {
  let service: TestexamService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TestexamService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
