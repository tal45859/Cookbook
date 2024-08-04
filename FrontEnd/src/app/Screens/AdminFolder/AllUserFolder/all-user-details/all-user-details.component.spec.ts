import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AllUserDetailsComponent } from './all-user-details.component';

describe('AllUserDetailsComponent', () => {
  let component: AllUserDetailsComponent;
  let fixture: ComponentFixture<AllUserDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AllUserDetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AllUserDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
