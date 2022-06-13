import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { WizStepPreparationComponent } from './wiz-step-preparation.component';

describe('WizStepPreparationComponent', () => {
  let component: WizStepPreparationComponent;
  let fixture: ComponentFixture<WizStepPreparationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ WizStepPreparationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(WizStepPreparationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
