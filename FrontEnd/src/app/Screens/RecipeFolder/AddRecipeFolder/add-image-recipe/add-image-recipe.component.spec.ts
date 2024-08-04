import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddImageRecipeComponent } from './add-image-recipe.component';

describe('AddImageRecipeComponent', () => {
  let component: AddImageRecipeComponent;
  let fixture: ComponentFixture<AddImageRecipeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddImageRecipeComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddImageRecipeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
