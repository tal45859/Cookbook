import { Recipe } from './../../../../Model/Recipe';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { LoginService } from 'src/app/Services/Login.service';
import { ResponseValidation } from 'src/app/Model/ResponseValidation';
import { RecipeService } from 'src/app/Services/Recipe.service';
import { RecipeValidationService } from 'src/app/Services/RecipeValidation.service';

@Component({
  selector: 'app-update-recipe',
  templateUrl: './update-recipe.component.html',
  styleUrls: ['./update-recipe.component.css']
})
export class UpdateRecipeComponent implements OnInit {
  @Output() CloseMeOutpot = new EventEmitter<any>();
  @Input() RecipeObjectUpdate:Recipe={};
  public SpinnerOn:boolean=false;
  public ResponseErrorMesage:Array<ResponseValidation>=[{Isok:true,Message:''},{Isok:true,Message:''},{Isok:true,Message:''},{Isok:true,Message:''},{Isok:true,Message:''},
  {Isok:true,Message:''},{Isok:true,Message:''}];
  constructor(private HttpLogin:LoginService,private HttpRecipe:RecipeService,private ValidationRecipe:RecipeValidationService) { }

  ngOnInit(): void {
  }

  async ClickUpdate()
  {
    this.ResponseErrorMesage[0]=this.ValidationRecipe.CheckRecipeName(this.RecipeObjectUpdate.RecipeName);
    this.ResponseErrorMesage[1]=this.ValidationRecipe.CheckIngredients(this.RecipeObjectUpdate.Ingredients);
    this.ResponseErrorMesage[2]=this.ValidationRecipe.CheckPreparationMethod(this.RecipeObjectUpdate.PreparationMethod);
    this.ResponseErrorMesage[3]=this.ValidationRecipe.CheckPreparationTime(this.RecipeObjectUpdate.PreparationTime);
    this.ResponseErrorMesage[4]=this.ValidationRecipe.CheckQuantityOfPortions(this.RecipeObjectUpdate.QuantityOfPortions);
    this.ResponseErrorMesage[5]=this.ValidationRecipe.CheckCanBeExpected(this.RecipeObjectUpdate.CanBeExpected);
    if(!this.ResponseErrorMesage[0].Isok||!this.ResponseErrorMesage[1].Isok||
      !this.ResponseErrorMesage[2].Isok||!this.ResponseErrorMesage[3].Isok||
      !this.ResponseErrorMesage[4].Isok||!this.ResponseErrorMesage[5].Isok)
      {
        this.SpinnerOn=false;
        return;
      }
      this.RecipeObjectUpdate.Subcategory=undefined;
      this.RecipeObjectUpdate.User=undefined;
      (await this.HttpRecipe.UpdateRecipe(this.HttpLogin.Token,this.RecipeObjectUpdate)).subscribe(
        ()=>{this.SpinnerOn=false,this.CloseMeOutpot.emit()},
        ()=>{this.ResponseErrorMesage[6]={Isok:false,Message:'לא הצלחנו לעדכן את המתכון נראה שלא השתנה כלום אנא נסה שנית מאוחר יותר!'},this.SpinnerOn=false;}
      );

  }
}
