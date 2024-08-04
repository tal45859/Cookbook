import { LoginService } from './../../../../Services/Login.service';
import { UserService } from 'src/app/Services/User.service';
import { User } from 'src/app/Model/User';
import { Component, Input, OnInit } from '@angular/core';
import { ChangeUserRole } from 'src/app/Model/ChangeUserRole';

@Component({
  selector: 'app-all-user-details',
  templateUrl: './all-user-details.component.html',
  styleUrls: ['./all-user-details.component.css']
})
export class AllUserDetailsComponent implements OnInit {
@Input() TypeDetails:number=0;// 1 = כל המשתמשים
                              // 2 = כל המשתמים הרגילים
                              // 3 = כל המנהלים
public AllUserByType:Array<User>=[];
public HeaderTable:String='';
public UserIdForDelete?:number=0;
public UserObjectForUpdateRole:ChangeUserRole={};
public IsDeleteOrUpdate:boolean=false;//fasle=delete // true=update
public page:number=0;
  constructor(private HttpUser:UserService,private HttpLogin:LoginService) { }

  async ngOnInit(): Promise<void> {
    await this.GetAllUserByType();
  }

  async GetAllUserByType()
  {
    //כל המשתמשים
    if(this.TypeDetails == 1)
    {
      this.HeaderTable='כל המשתמשים';
      (await this.HttpUser.GetAllUser(this.HttpLogin.Token)).subscribe((response)=>{this.AllUserByType=response});
    }
    //משתמשים רגילים
    else if(this.TypeDetails == 2)
    {
      this.HeaderTable='משתמשים רגילים';
      (await this.HttpUser.GetAllUserNoAdmin(this.HttpLogin.Token)).subscribe((response)=>{this.AllUserByType=response});
    }
    //מנהלים
    else
    {
      this.HeaderTable='מנהלים';
      (await this.HttpUser.GetAllAdmin(this.HttpLogin.Token)).subscribe((response)=>{this.AllUserByType=response});
    }
    this.page=0;
  }


  async ClickDelete()
  {
    (await this.HttpUser.DeleteUserByIdForAdmin(this.HttpLogin.Token,this.UserIdForDelete)).subscribe(
      ()=>{this.GetAllUserByType();},
        ()=>{this.MessageError()}
    );
  }

  public MessageError()
  {
    let id=!this.IsDeleteOrUpdate?this.UserIdForDelete:this.UserObjectForUpdateRole.UserId;
    const element = document.getElementById(""+id);
    element?.classList?.add("AddDisplay");
    element?.classList?.remove("removeDisplay");

    setTimeout(()=>{
      const element = document.getElementById(""+id);
      element?.classList?.remove("AddDisplay");
      element?.classList?.add("removeDisplay");
    },3000);
  }


  async ClickChangRole()
  {
    if(this.TypeDetails == 3)
    {
      this.UserObjectForUpdateRole.Role ="Classic";
    }
    else
    {
      this.UserObjectForUpdateRole.Role ="Admin"
    }
    (await this.HttpUser.ChangeUserRoleForAdmin(this.HttpLogin.Token,this.UserObjectForUpdateRole)).subscribe(
      ()=>{this.GetAllUserByType();},
      ()=>{this.MessageError()});
  }





}





//בשביל להציג ולגבות שגיאה
// document.getElementById(id).style.display = 'block';
// // hide the lorem ipsum text
// document.getElementById(text).style.display = 'none';
