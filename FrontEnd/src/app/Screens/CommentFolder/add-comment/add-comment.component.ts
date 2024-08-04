import { ResponseValidation } from './../../../Model/ResponseValidation';
import { Comment } from './../../../Model/Comment';
import { CommentService } from './../../../Services/Comment.service';
import { Component, Input, OnInit } from '@angular/core';
import { CommentValidationService } from 'src/app/Services/CommentValidation.service';

@Component({
  selector: 'app-add-comment',
  templateUrl: './add-comment.component.html',
  styleUrls: ['./add-comment.component.css']
})
export class AddCommentComponent implements OnInit {
  @Input() RecipeIdToAdd?:number=0;
  public CoomentObjectToAdd:Comment={};
  public IsCreate:boolean=false;
  public SpinnerOn:boolean=false;
  public ResponseMessage:Array<ResponseValidation>=[{Isok:true,Message:''},{Isok:true,Message:''},{Isok:true,Message:''}];
  constructor(private HttpComment:CommentService,private Validation:CommentValidationService) { }

  ngOnInit(): void {
  }

  async AddComment()
  {
    this.CoomentObjectToAdd.RecipeId=this.RecipeIdToAdd;
    this.ResponseMessage[0]=this.Validation.CheckTitle(this.CoomentObjectToAdd.Title);
    this.ResponseMessage[1]=this.Validation.CheckdBody(this.CoomentObjectToAdd.Body);
    if(!this.ResponseMessage[0].Isok||!this.ResponseMessage[1].Isok)
    {
      this.SpinnerOn=false;
      return;
    }
    (await this.HttpComment.AddComment(this.CoomentObjectToAdd)).subscribe(
      ()=>{this.SpinnerOn=false,this.SetTimeoutFun()},
      ()=>{this.SpinnerOn=false,this.ResponseMessage[2]={Isok:false,Message:'לא הצלחנו להוסיף את התגובה אנא נסה שנית מאוחר יותר!'}}
    );
  }

  public SetTimeoutFun()
  {
    this.IsCreate=true;
    setTimeout(()=>{
      this.IsCreate=false;
    },3000);
  }

}
