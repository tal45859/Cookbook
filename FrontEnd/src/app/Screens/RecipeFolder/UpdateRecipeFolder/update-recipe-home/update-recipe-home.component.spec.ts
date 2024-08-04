import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateRecipeHomeComponent } from './update-recipe-home.component';

describe('UpdateRecipeHomeComponent', () => {
  let component: UpdateRecipeHomeComponent;
  let fixture: ComponentFixture<UpdateRecipeHomeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UpdateRecipeHomeComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UpdateRecipeHomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
