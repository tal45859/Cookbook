import { ResponseValidation } from './../../../Model/ResponseValidation';
import { Image } from './../../../Model/Image';
import { ImageService } from './../../../Services/Image.service';
import { RecipeService } from './../../../Services/Recipe.service';
import { LoginService } from './../../../Services/Login.service';
import { Component, OnInit } from '@angular/core';
import { Recipe } from 'src/app/Model/Recipe';

@Component({
  selector: 'app-user-recipe',
  templateUrl: './user-recipe.component.html',
  styleUrls: ['./user-recipe.component.css']
})
export class UserRecipeComponent implements OnInit {
  public RecipeArray:Array<Recipe>=[];
  public AllImageForRecipe:Array<Image>=[];
  public OpenSun:number=0;
  public page:number=0;
  public RecipeObjToSun:Recipe={};
  public ResponseMessage:ResponseValidation={Isok:true,Message:''};
  public ObjectToUpdateRecipe:Recipe={};

  constructor(private HttpLogin:LoginService,private HttpRecipe:RecipeService,private HttpImage:ImageService) { }

  async ngOnInit(): Promise<void> {
    await this.GetAllRecipeByJWT();
    await this.GetAllImageByJWT();
  }

  async GetAllRecipeByJWT()
  {
    (await this.HttpRecipe.GetAllRecipeJWTForUser(this.HttpLogin.Token)).subscribe(
      (response)=>{this.RecipeArray=response}
    );
  }

  async GetAllImageByJWT()
  {
    (await this.HttpImage.GetAllImageByJWT(this.HttpLogin.Token)).subscribe(
      (response)=>{this.AllImageForRecipe=response}
    );
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


  async ClickDelete(RecipeId?:number)
  {
    (await this.HttpRecipe.DeleteRecipe(this.HttpLogin.Token,RecipeId)).subscribe(
      ()=>{this.GetAllRecipeByJWT(),this.GetAllImageByJWT()},
      ()=>{ this.ResponseMessage={Isok:false,Message:'לא הצלחנו למחוק את המתכון אנא נסה שנית מאוחר יותר!'},this.MessageError(RecipeId)}//לעשות משהו
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
