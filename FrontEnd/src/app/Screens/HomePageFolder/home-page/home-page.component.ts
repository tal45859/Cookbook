import { Component, OnInit } from '@angular/core';
import { Image } from 'src/app/Model/Image';
import { Recipe } from 'src/app/Model/Recipe';
import { ImageService } from 'src/app/Services/Image.service';
import { RecipeService } from 'src/app/Services/Recipe.service';



@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css']
})

export class HomePageComponent implements OnInit {
  public AllRecipeByBesViews:Array<Recipe>=[];
  public AllRecipeByBesLike:Array<Recipe>=[];
  public AllRecipeByUploadDate:Array<Recipe>=[];
  public AllImageForRecipe:Array<Image>=[];
  public OpenSun:number=0;
  public RecipeObjToSun:Recipe={};
  constructor(private HttpRecipe:RecipeService, private HttpImage:ImageService) { }

  async ngOnInit(): Promise<void> {
    await this.GetAllRecipeByBesViews();
    await this.GetAllRecipeByBesLike();
    await this.GetAllRecipeByUploadDate();
    await this.GetAllImage();

  }

  //קבלת רשימת המתכונים בצורה ממוינת מי הנצפים ביותר שניתן לצפות בהם
  async GetAllRecipeByBesViews()
  {
    (await this.HttpRecipe.GetAllRecipeCanBeExpectedByNumberOfViews()).subscribe((Response)=>{this.AllRecipeByBesViews=Response});
  }
  //קבלת רשימת מתכונים בצורה ממוינת מי האהובים ביותר שניתן לצפות בהם
  async GetAllRecipeByBesLike()
  {
    (await this.HttpRecipe.GetAllRecipeCanBeExpectedByNumberOfLikes()).subscribe((Response)=>{this.AllRecipeByBesLike=Response});
  }
  //קבלת רשימת מתכונים בצורה ממוינת מי החדשים ביותר שניתן לצפות בהם
  async GetAllRecipeByUploadDate()
  {
    (await this.HttpRecipe.GetAllRecipeCanBeExpectedByUploadDate()).subscribe((Response)=>{this.AllRecipeByUploadDate=Response});
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

  CloseMe()
  {
    this.OpenSun=0;
  }


}



