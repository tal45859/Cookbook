import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-comment-home',
  templateUrl: './comment-home.component.html',
  styleUrls: ['./comment-home.component.css']
})
export class CommentHomeComponent implements OnInit {
  public OpenChildren:number=1;
  @Input() RecipeId?:number=0;
  constructor() { }

  ngOnInit(): void {
  }

}
