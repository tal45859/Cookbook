import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CommentHomeComponent } from './comment-home.component';

describe('CommentHomeComponent', () => {
  let component: CommentHomeComponent;
  let fixture: ComponentFixture<CommentHomeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CommentHomeComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CommentHomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
