import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { WizStepDetailsComponent } from './wiz-step-details.component';

describe('WizStepDetailsComponent', () => {
  let component: WizStepDetailsComponent;
  let fixture: ComponentFixture<WizStepDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ WizStepDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WizStepDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
