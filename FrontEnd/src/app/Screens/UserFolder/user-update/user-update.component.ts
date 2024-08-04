import { LoginService } from './../../../Services/Login.service';
import { UserValidationService } from 'src/app/Services/UserValidation.service';
import { UserService } from 'src/app/Services/User.service';
import { ResponseValidation } from './../../../Model/ResponseValidation';
import { Component, Input, OnInit } from '@angular/core';
import { User } from 'src/app/Model/User';
import { flush } from '@angular/core/testing';

@Component({
  selector: 'app-user-update',
  templateUrl: './user-update.component.html',
  styleUrls: ['./user-update.component.css']
})
export class UserUpdateComponent implements OnInit {
  @Input() UserObjectInput:User={};
  public Password:string='';
  public UserObjForCheckValidation:User={};
  public OpenEye:boolean=false;
  public SpinnerOn:boolean=false;
  public IsAfterClickUpdate:boolean=false;
  public UserValidationResponse:Array<ResponseValidation>=[{Isok:true,Message:''},{Isok:true,Message:''},
  {Isok:true,Message:''},{Isok:true,Message:''},{Isok:true,Message:''},{Isok:true,Message:''}];
  constructor(private HttpUser:UserService,private Validation:UserValidationService,private HttpLogin:LoginService) { }

  async ngOnInit(): Promise<void> {

    await this.AddToUserObjValidationTrueUser();
     this.Password=String(this.UserObjectInput.Password);
  }

  async ClickUpdate()
  {
    this.IsAfterClickUpdate=false;
    this.UserValidationResponse[5].Isok=true;
    this.UserValidationResponse[5].Message='';

    if(!this.CheckHaveChange())
    {
      this.UserValidationResponse[5].Isok=false;
      this.UserValidationResponse[5].Message='לא ניתן לעדכן לא בוצע שינוי!';
      this.IsAfterClickUpdate=true;
      this.SpinnerOn=false;
      return;
    }
    else if(!this.CheckValidation())
    {
      this.SpinnerOn=false;
      return;
    }
    await this.UpdateUser();
  }

  public StartReload()
  {
    this.SpinnerOn=false;
    this.IsAfterClickUpdate=true;
    this.UserValidationResponse[5].Isok=true;
    this.UserValidationResponse[5].Message='המשתמש עודכן בהצלחה';
    setTimeout(()=>{
       window.location.reload();
    },3000);
  }


  public AddToUserObjValidationTrueUser()
  {
    this.UserObjForCheckValidation.FirstName=this.UserObjectInput.FirstName;
    this.UserObjForCheckValidation.LastName=this.UserObjectInput.LastName;
    this.UserObjForCheckValidation.Birthdate=this.UserObjectInput.Birthdate;
    this.UserObjForCheckValidation.Phone=this.UserObjectInput.Phone;
    this.UserObjForCheckValidation.Password=this.UserObjectInput.Password;
  }

  public CheckValidation():boolean
  {
    this.UserValidationResponse[0]=this.Validation.CheckFirstName(this.UserObjectInput.FirstName);
    this.UserValidationResponse[1]=this.Validation.CheckLastName(this.UserObjectInput.LastName);
    this.UserValidationResponse[2]=this.Validation.CheckBirthdate(this.UserObjectInput.Birthdate);
    this.UserValidationResponse[3]=this.Validation.CheckPhone(this.UserObjectInput.Phone);
    if(this.UserObjectInput.Password!=this.UserObjForCheckValidation.Password)
    {
      this.UserValidationResponse[4]=this.Validation.CheckPassword(this.UserObjectInput.Password);
    }
    if(!this.UserValidationResponse[0].Isok || !this.UserValidationResponse[1].Isok
      || !this.UserValidationResponse[2].Isok || !this.UserValidationResponse[3].Isok
      || !this.UserValidationResponse[4].Isok)
      {
        return false;
      }
      return true;
  }


  public CheckHaveChange():boolean
  {
    return this.UserObjForCheckValidation.FirstName!=this.UserObjectInput.FirstName||
    this.UserObjForCheckValidation.LastName!=this.UserObjectInput.LastName||
    this.UserObjForCheckValidation.Birthdate!=this.UserObjectInput.Birthdate||
    this.UserObjForCheckValidation.Phone!=this.UserObjectInput.Phone||
    this.UserObjForCheckValidation.Password!=this.UserObjectInput.Password;
  }

  async UpdateUser()
  {
    (await this.HttpUser.UpdateUserByToken(this.HttpLogin.Token,this.UserObjectInput)).subscribe(
      ()=>{this.StartReload();},
      ()=>{this.SpinnerOn=false,this.IsAfterClickUpdate=true,this.UserValidationResponse[5].Isok=false,
        this.UserValidationResponse[5].Message='חלה שגיאה בעדכון המשתמש אנא נסה שנית מאוחר יותר';}
    );
  }

}
