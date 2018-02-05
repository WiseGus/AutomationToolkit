import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { WizStepResultsComponent } from './wiz-step-results.component';

describe('WizStepResultsComponent', () => {
  let component: WizStepResultsComponent;
  let fixture: ComponentFixture<WizStepResultsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ WizStepResultsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WizStepResultsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
