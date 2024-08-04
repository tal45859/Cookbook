import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-add-recipe-home',
  templateUrl: './add-recipe-home.component.html',
  styleUrls: ['./add-recipe-home.component.css']
})
export class AddRecipeHomeComponent implements OnInit {
  public RecipeId?:number;
  public OpenNextStep=1;
  constructor() { }

  ngOnInit(): void {
  }



  NextStep(RecipeIdLast:number)
  {
    this.RecipeId=RecipeIdLast;
    this.OpenNextStep=2;
  }

}
