import { RecipeValidationService } from './../../../../Services/RecipeValidation.service';
import { SubcategoryValidationService } from 'src/app/Services/SubcategoryValidation.service';
import { CategoryValidationService } from 'src/app/Services/CategoryValidation.service';
import { CategoryService } from './../../../../Services/Category.service';
import { Category } from './../../../../Model/Category';
import { SubcategoryService } from './../../../../Services/Subcategory.service';
import { LoginService } from './../../../../Services/Login.service';
import { Subcategory } from './../../../../Model/Subcategory';
import { Recipe } from './../../../../Model/Recipe';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { ResponseValidation } from 'src/app/Model/ResponseValidation';
import { RecipeService } from 'src/app/Services/Recipe.service';

@Component({
  selector: 'app-add-recipe',
  templateUrl: './add-recipe.component.html',
  styleUrls: ['./add-recipe.component.css']
})
export class AddRecipeComponent implements OnInit {
  @Output() CloseMeOutpot = new EventEmitter<number>();
  public IsCreate:boolean=false;
  public SpinnerOn:boolean=false;
  public RecipeObjectToAdd:Recipe={};
  public AllSubCategory:Array<Subcategory>=[];
  public CategoryId?:number=0;
  public AllCategory:Array<Category>=[];
  public ResponseErrorMesage:Array<ResponseValidation>=[{Isok:true,Message:''},{Isok:true,Message:''},{Isok:true,Message:''},{Isok:true,Message:''},{Isok:true,Message:''},
  {Isok:true,Message:''},{Isok:true,Message:''},{Isok:true,Message:''},{Isok:true,Message:''}];

  constructor(private HttpSubcategoy:SubcategoryService,private HttpCategory:CategoryService,private ValidationRecipe:RecipeValidationService,
    private HttpLogin:LoginService,private HttpRecipe:RecipeService) { }

  async ngOnInit(): Promise<void> {
    // await this.GetAllSubcategory();
    await this.GetAllCategory();
  }

  async GetAllSubcategoryByCategoryId()
  {
    (await this.HttpSubcategoy.GetAllSubcategoryByCategoryId(this.CategoryId)).subscribe((response)=>{this.AllSubCategory=response});
  }

  async GetAllCategory()
  {
    (await this.HttpCategory.GetAllCategory()).subscribe((response)=>{this.AllCategory=response});
  }

  async ClickAddRecipe()
  {
    //בדיקות
    this.ResponseErrorMesage[0]=this.ValidationRecipe.CheckCategoryId(this.CategoryId);
    this.ResponseErrorMesage[1]=this.ValidationRecipe.CheckSubCategoryId(this.RecipeObjectToAdd.SubcategoryId);
    this.ResponseErrorMesage[2]=this.ValidationRecipe.CheckRecipeName(this.RecipeObjectToAdd.RecipeName);
    this.ResponseErrorMesage[3]=this.ValidationRecipe.CheckIngredients(this.RecipeObjectToAdd.Ingredients);
    this.ResponseErrorMesage[4]=this.ValidationRecipe.CheckPreparationMethod(this.RecipeObjectToAdd.PreparationMethod);
    this.ResponseErrorMesage[5]=this.ValidationRecipe.CheckPreparationTime(this.RecipeObjectToAdd.PreparationTime);
    this.ResponseErrorMesage[6]=this.ValidationRecipe.CheckQuantityOfPortions(this.RecipeObjectToAdd.QuantityOfPortions);
    this.ResponseErrorMesage[7]=this.ValidationRecipe.CheckCanBeExpected(this.RecipeObjectToAdd.CanBeExpected);
    if(!this.ResponseErrorMesage[0].Isok||!this.ResponseErrorMesage[1].Isok||
      !this.ResponseErrorMesage[2].Isok||!this.ResponseErrorMesage[3].Isok||
      !this.ResponseErrorMesage[4].Isok||!this.ResponseErrorMesage[5].Isok||
      !this.ResponseErrorMesage[6].Isok||!this.ResponseErrorMesage[7].Isok)
      {
        this.SpinnerOn=false;
        return;
      }
    this.SpinnerOn=false;
    (await this.HttpRecipe.AddRecipe(this.HttpLogin.Token,this.RecipeObjectToAdd)).subscribe(
      ()=>{this.ResponseErrorMesage[8]={Isok:true,Message:'המתכון נוסף בהצלחה'},this.SetTimeoutFun(),this.GetLastRecip()},
      ()=>{this.ResponseErrorMesage[8]={Isok:false,Message:'לא הצלחנו להוסיף את המתכון אנא נסה שנית מאוחר יותר!'},this.SetTimeoutFun()}//להציג שגיאה
    );

  }

  async GetLastRecip()
  {
    (await this.HttpRecipe.GetLastRecipeIdByJWTForUser(this.HttpLogin.Token)).subscribe(
      (response)=>{this.CloseMeOutpot.emit(response)}
    );
  }

  public SetTimeoutFun()
  {
    this.IsCreate=true;
    setTimeout(()=>{
      this.IsCreate=false;
      this.ResponseErrorMesage[8]={Isok:true,Message:''};
    },3000);
  }

}
