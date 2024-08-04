import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddRecipeHomeComponent } from './add-recipe-home.component';

describe('AddRecipeHomeComponent', () => {
  let component: AddRecipeHomeComponent;
  let fixture: ComponentFixture<AddRecipeHomeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddRecipeHomeComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddRecipeHomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
