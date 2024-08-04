import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddReportingComponent } from './add-reporting.component';

describe('AddReportingComponent', () => {
  let component: AddReportingComponent;
  let fixture: ComponentFixture<AddReportingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddReportingComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddReportingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
