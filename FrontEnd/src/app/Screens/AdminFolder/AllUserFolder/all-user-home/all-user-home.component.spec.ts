import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AllUserHomeComponent } from './all-user-home.component';

describe('AllUserHomeComponent', () => {
  let component: AllUserHomeComponent;
  let fixture: ComponentFixture<AllUserHomeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AllUserHomeComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AllUserHomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
