import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TestexamComponent } from './testexam.component';

describe('TestexamComponent', () => {
  let component: TestexamComponent;
  let fixture: ComponentFixture<TestexamComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TestexamComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TestexamComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
