import { TestBed } from '@angular/core/testing';

import { QuizexamService } from './quizexam.service';

describe('QuizexamService', () => {
  let service: QuizexamService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(QuizexamService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
