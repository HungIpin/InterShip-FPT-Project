import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DialogNewSKillComponent } from './dialog-new-skill.component';

describe('DialogNewSKillComponent', () => {
  let component: DialogNewSKillComponent;
  let fixture: ComponentFixture<DialogNewSKillComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DialogNewSKillComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DialogNewSKillComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
