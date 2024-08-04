import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AllLoginHistoryComponent } from './all-login-history.component';

describe('AllLoginHistoryComponent', () => {
  let component: AllLoginHistoryComponent;
  let fixture: ComponentFixture<AllLoginHistoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AllLoginHistoryComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AllLoginHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
