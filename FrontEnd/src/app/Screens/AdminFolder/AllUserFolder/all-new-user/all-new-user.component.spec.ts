import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AllNewUserComponent } from './all-new-user.component';

describe('AllNewUserComponent', () => {
  let component: AllNewUserComponent;
  let fixture: ComponentFixture<AllNewUserComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AllNewUserComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AllNewUserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
