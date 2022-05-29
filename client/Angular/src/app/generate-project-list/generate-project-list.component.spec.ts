import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GenerateProjectListComponent } from './generate-project-list.component';

describe('GenerateProjectComponent', () => {
  let component: GenerateProjectListComponent;
  let fixture: ComponentFixture<GenerateProjectListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GenerateProjectListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GenerateProjectListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
