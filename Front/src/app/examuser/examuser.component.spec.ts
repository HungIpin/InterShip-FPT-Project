import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ExamuserComponent } from './examuser.component';

describe('ExamuserComponent', () => {
  let component: ExamuserComponent;
  let fixture: ComponentFixture<ExamuserComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ExamuserComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ExamuserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
