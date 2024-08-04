import { ResponseValidation } from './../../../Model/ResponseValidation';
import { LoginService } from './../../../Services/Login.service';
import { Comment } from './../../../Model/Comment';
import { CommentService } from './../../../Services/Comment.service';
import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-all-comment',
  templateUrl: './all-comment.component.html',
  styleUrls: ['./all-comment.component.css']
})
export class AllCommentComponent implements OnInit {
  @Input() RecipeIdToGetAll?:number=0;
  public AllCommentArray:Array<Comment>=[]
  public page:number=0;
  public Role=this.HttpLogin.Role;
  public ResponseMessage:ResponseValidation={Isok:true,Message:''};
  constructor(private HttpComment:CommentService,private HttpLogin:LoginService) { }

  async ngOnInit(): Promise<void> {
    await this.GetAllComment();
  }

  async GetAllComment()
  {
    (await this.HttpComment.GetAllCommentByRecipeId(this.RecipeIdToGetAll)).subscribe(
      (response)=>{this.AllCommentArray=response}
    );
  }

  public ChangePanel(id?:number)
  {
    const ClassPanel= document?.getElementById(""+id)?.classList;
    if(ClassPanel?.toString() == "removeDisplay")//אם הוא שווה לו תפתח
    {
      document?.getElementById(""+id)?.classList.remove('removeDisplay');
      document?.getElementById(""+id)?.classList.add('AddDisplay');
    }
    else //אם הוא שונה תסגור
    {
      document?.getElementById(""+id)?.classList.remove('AddDisplay');
      document?.getElementById(""+id)?.classList.add('removeDisplay');
    }
  }

  async DeleteComment(id?:number)
  {
    (await this.HttpComment.DeleteCommentByID(this.HttpLogin.Token,id)).subscribe(
      ()=>{this.GetAllComment()},
      ()=>{this.ResponseMessage={Isok:false,Message:"לא הצלחנו למחוק את התוגבה אנא נסה שנית מאוחר יותר!"},this.MessageError(id)}
    );
  }


  public MessageError(id?:number)
  {

    const element = document.getElementById("Error"+id);
    element?.classList?.add("AddDisplay");
    element?.classList?.remove("removeDisplay");

    setTimeout(()=>{
      const element = document.getElementById("Error"+id);
      element?.classList?.remove("AddDisplay");
      element?.classList?.add("removeDisplay");
      this.ResponseMessage={Isok:true,Message:''}
    },3000);

  }

}
