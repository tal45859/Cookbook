import { Recipe } from './../../../../Model/Recipe';
import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-update-recipe-home',
  templateUrl: './update-recipe-home.component.html',
  styleUrls: ['./update-recipe-home.component.css']
})
export class UpdateRecipeHomeComponent implements OnInit {
  @Input()RecipeObjectToUpdate:Recipe={};
  public OpenNextStep=1;
  constructor() { }

  ngOnInit(): void {

  }


  NextStep(spam:any)
  {
    this.OpenNextStep=2;
  }

}
