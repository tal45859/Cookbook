import { SubcategoryService } from './../../../Services/Subcategory.service';
import { CategoryService } from './../../../Services/Category.service';
import { isNull } from '@angular/compiler/src/output/output_ast';
import { ResponseValidation } from './../../../Model/ResponseValidation';
import { Favorite } from './../../../Model/Favorite';
import { FavoriteService } from './../../../Services/Favorite.service';
import { Recipe } from './../../../Model/Recipe';
import { ImageService } from './../../../Services/Image.service';
import { RecipeService } from './../../../Services/Recipe.service';
import { LoginService } from './../../../Services/Login.service';
import { Component, OnInit } from '@angular/core';
import { Image } from 'src/app/Model/Image';
import { Category } from 'src/app/Model/Category';
import { Subcategory } from 'src/app/Model/Subcategory';

@Component({
  selector: 'app-all-recipe',
  templateUrl: './all-recipe.component.html',
  styleUrls: ['./all-recipe.component.css']
})
export class AllRecipeComponent implements OnInit {
  public AllRecipeArr:Array<Recipe>=[];
  public AllImageForRecipe:Array<Image>=[];
  public Role:string=this.HttpLogin.Role;
  public ObjectToAddFavorite:Favorite={};
  public RecipeObjToSun:Recipe={};
  public page:number=0;
  public OpenSun:number=0;
  public IsDeleteOrAddFavorite:boolean=false;
  public ResponseMessage:ResponseValidation={Isok:true,Message:''};
  public AllCategory:Array<Category>=[];
  public AllSubCategory:Array<Subcategory>=[];
  public CategoryIdOrSubCategoryId?:number=0;
  public ResponseError:boolean=false;
  public option=[
    {value:1,Name:'ללא מסנן'},
    {value:2,Name:'לפי תת קטגוריה'},
    {value:3,Name:'לפי קטגוריה'},
    {value:4,Name:'הנצפים ביותר'},
    {value:5,Name:'האוהבים ביותר'},
    {value:6,Name:'הלא האוהבים'},
    {value:7,Name:'החדשים ביותר'}
  ];
  public Filter:number=1;

  constructor(private HttpLogin:LoginService,private HttpRecipe:RecipeService,private HttpImage:ImageService,private HttpFavorite:FavoriteService,
    private HttpCategory:CategoryService,private HttpSubcategory:SubcategoryService) { }

  async ngOnInit(): Promise<void> {
    await this.GetAllRecipe();
    await this.GetAllImage();
    await this.GetAllSubCategoryAndCategory();
  }

  async GetAllRecipe()
  {
    (await this.HttpRecipe.GetAllRecipeCanBeExpected()).subscribe((response)=>{this.AllRecipeArr=response});
  }

  async GetAllImage()
  {
    (await this.HttpImage.GetAllImageCanBeExpected()).subscribe((response)=>{this.AllImageForRecipe=response});
  }

  public GetImageByRecipeId(RecipeId?:number)
  {
    for(let i=0;i<this.AllImageForRecipe.length;i++)
    {
      if(this.AllImageForRecipe[i].RecipeId==RecipeId)
      {
        return this.AllImageForRecipe[i].Url;
      }
    }
    return "assets/defult.png";
  }

  async GetAllSubCategoryAndCategory()
  {
    (await this.HttpCategory.GetAllCategory()).subscribe((response)=>{this.AllCategory=response});
    (await this.HttpSubcategory.GetAllSubcategory()).subscribe((response)=>{this.AllSubCategory=response});
  }

  //לעשות הוספת פיבריט למי שיכול
  async ClickAddFavorite(RecipeId?:number)
  {
    this.ObjectToAddFavorite.RecipeId=RecipeId;
    (await this.HttpFavorite.AddFavorite(this.HttpLogin.Token,this.ObjectToAddFavorite)).subscribe(
    ()=>{ this.ResponseMessage={Isok:true,Message:'המתכון הוסף בהצלחה למועדפים'},this.MessageError(RecipeId)},//לעעשות משהוו
    ()=>{this.IsDeleteOrAddFavorite=true , this.ResponseMessage={Isok:false,Message:'לא הצלחנו להוסיף את המתכון למועדפם אנא נסה שנית מאוחר יותר'},this.MessageError(RecipeId)});//לעשות משהו
  }

  //לעשות מחיקה למנהל
  async ClickDeleteForAdmin(RecipeId?:number)
  {
    (await this.HttpRecipe.DeleteRecipe(this.HttpLogin.Token,RecipeId)).subscribe(
      ()=>{this.GetAllRecipe(),this.GetAllImage()},//לעשות משהו
      ()=>{this.IsDeleteOrAddFavorite=false , this.ResponseMessage={Isok:false,Message:'לא הצלחנו למחוק את המתכון אנא נסה שנית מאוחר יותר!'},this.MessageError(RecipeId)}//לעשות משהו
    );
  }

  public MessageError(id?:number)
  {
    const element = document.getElementById(""+id);
    element?.classList?.add("AddDisplay");
    element?.classList?.remove("removeDisplay");

    setTimeout(()=>{
      const element = document.getElementById(""+id);
      element?.classList?.remove("AddDisplay");
      element?.classList?.add("removeDisplay");
    },3000);
  }

  async ClickStartFilter()
  {
    this.ResponseError=false;
    if(this.Filter==1)
    {
      await this.GetAllRecipe();
      return;
    }
    else if(this.Filter==2)
    {
      await this.GetAllRecipeBySubCategoryId(this.CategoryIdOrSubCategoryId);
      return;
    }
    else if(this.Filter==3)
    {
      await this.GetAllRecipeByCategoryId(this.CategoryIdOrSubCategoryId);
      return;
    }
    else if(this.Filter==4)
    {
      await this.GetAllRecipeByBesViews();
      return;
    }
    else if(this.Filter==5)
    {
      await this.GetAllRecipeByBesLike();
      return;
    }
    else if(this.Filter==6)
    {
      await this.GetAllRecipeByBesNoLike();
      return;
    }
    else if(this.Filter==7)
    {
      await this.GetAllRecipeByUploadDate();
      return;
    }
  }

  //קבלת רשימת כל המתכונים שניתן לצפות בהם לפי מזהה תת קטגוריה
  async GetAllRecipeBySubCategoryId(SubCategoryId?:number)
  {
    if(SubCategoryId==0)
    {
      this.ResponseError=true;
      return;
    }
    (await this.HttpRecipe.GetAllRecipeCanBeExpectedBySubcategoryId(SubCategoryId)).subscribe((Response)=>{this.AllRecipeArr=Response});
  }
  //קבלת רשימת כל המתכונים שניתן לצפות בהם לפי מזהה קטגוריה
  async GetAllRecipeByCategoryId(CategoryId?:number)
  {
    if(CategoryId==0)
    {
      this.ResponseError=true;
      return;
    }
    (await this.HttpRecipe.GetAllRecipeCanBeExpectedByCategoryId(CategoryId)).subscribe((Response)=>{this.AllRecipeArr=Response});
  }
  //קבלת רשימת המתכונים בצורה ממוינת מי הנצפים ביותר שניתן לצפות בהם
  async GetAllRecipeByBesViews()
  {
    (await this.HttpRecipe.GetAllRecipeCanBeExpectedByNumberOfViews()).subscribe((Response)=>{this.AllRecipeArr=Response});
  }
  //קבלת רשימת מתכונים בצורה ממוינת מי האהובים ביותר שניתן לצפות בהם
  async GetAllRecipeByBesLike()
  {
    (await this.HttpRecipe.GetAllRecipeCanBeExpectedByNumberOfLikes()).subscribe((Response)=>{this.AllRecipeArr=Response});
  }
  //קבלת רשימת מתכונים בצורה ממוינת מי הכי לא אהובים שניתן לצפות בהם
  async GetAllRecipeByBesNoLike()
  {
    (await this.HttpRecipe.GetAllRecipeCanBeExpectedByNumberOfNoLieks()).subscribe((Response)=>{this.AllRecipeArr=Response});
  }
  //קבלת רשימת מתכונים בצורה ממוינת מי החדשים ביותר שניתן לצפות בהם
  async GetAllRecipeByUploadDate()
  {
    (await this.HttpRecipe.GetAllRecipeCanBeExpectedByUploadDate()).subscribe((Response)=>{this.AllRecipeArr=Response});
  }

  CloseMe()
  {
    this.OpenSun=0;
  }




}
