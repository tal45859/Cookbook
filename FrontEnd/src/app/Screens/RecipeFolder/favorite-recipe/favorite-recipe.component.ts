import { Image } from './../../../Model/Image';
import { ImageService } from './../../../Services/Image.service';
import { FavoriteService } from './../../../Services/Favorite.service';
import { LoginService } from './../../../Services/Login.service';
import { Component, OnInit } from '@angular/core';
import { Favorite } from 'src/app/Model/Favorite';
import { Recipe } from 'src/app/Model/Recipe';

@Component({
  selector: 'app-favorite-recipe',
  templateUrl: './favorite-recipe.component.html',
  styleUrls: ['./favorite-recipe.component.css']
})
export class FavoriteRecipeComponent implements OnInit {
  public AllImageForRecipe:Array<Image>=[];
  public FavoriteArray:Array<Favorite>=[];
  public page:number=0;
  public RecipeObjToSun?:Recipe={};
  public OpenSun:number=0;

  constructor(private HttpLogin:LoginService,private HttpFavorite:FavoriteService,private HttpImage:ImageService) { }

  async ngOnInit(): Promise<void> {
    await this.GetAllFavorite();
    await this.GetAllImage();
  }

  async GetAllFavorite()
  {
    (await this.HttpFavorite.GetAllFavoriteJwt(this.HttpLogin.Token)).subscribe(
      (response)=>{this.FavoriteArray=response}
    );
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


  async ClickDeleteFavorite(FavoriteId?:number)
  {
    (await this.HttpFavorite.DeleteFavoriteById(this.HttpLogin.Token,FavoriteId)).subscribe(
      ()=>{this.GetAllFavorite()},//לעשות משהו
      ()=>{this.MessageError(FavoriteId)}
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

  CloseMe()
  {
    this.OpenSun=0;
  }


}
