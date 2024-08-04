import { ResponseValidation } from './../../../Model/ResponseValidation';
import { ImageService } from './../../../Services/Image.service';
import { Image } from './../../../Model/Image';
import { Recipe } from './../../../Model/Recipe';
import { RecipeService } from './../../../Services/Recipe.service';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-recipe-details',
  templateUrl: './recipe-details.component.html',
  styleUrls: ['./recipe-details.component.css']
})
export class RecipeDetailsComponent implements OnInit {
  @Input() RecipeObj?:Recipe={};
  @Output() CloseMeOutpot = new EventEmitter<any>();
  public ImageArray:Array<Image>=[];
  public AddLike:boolean=false;
  public LikeOrUnLike?:boolean;
  public ResponseMessage:ResponseValidation={Isok:true,Message:''};

  public Index:number=0;
  constructor(private HttpRicep:RecipeService,private HttpImage:ImageService) { }

  async ngOnInit(): Promise<void> {
    await this.AddShow();
    await this.GetAllImageByRecipeId();

  }

  async GetAllImageByRecipeId()
  {
    (await this.HttpImage.GetAllImageByRecipeId(this.RecipeObj?.Id)).subscribe((response)=>{this.ImageArray=response});
  }



  async AddShow()
  {
    (await this.HttpRicep.AddViewsToRecipe(this.RecipeObj?.Id)).subscribe();
  }

  async AddLikeOrNoLike(LikeOrNOLike:boolean)
  {
    if(!this.AddLike)
    {
      (await this.HttpRicep.AddLikeOrNoLikeToRecipe(this.RecipeObj?.Id,LikeOrNOLike)).subscribe(()=>{this.AddLike=true});//לעשות משהו

    }
    else
    {
      this.ResponseMessage={Isok:false,Message:'לא ניתן להוסיף יותר מפעם אחת'};
      setTimeout(()=>{
        this.ResponseMessage={Isok:true,Message:''};
      },3000);
    }
  }

  public ChangeIndex(isNextOrPrev:boolean)
  {
    if(isNextOrPrev)
    {
      if(this.Index==this.ImageArray.length-1)
      {
        this.Index=0;
      }
      else
      {
        this.Index++;
      }
    }
    else
    {
      if(this.Index==0)
      {
        this.Index=this.ImageArray.length-1;
      }
      else
      {
        this.Index--;
      }
    }

  }

  public ClickClose()
  {
    this.CloseMeOutpot.emit();
  }

  //לעשות תגובות
  //לעשות דיווח


}
