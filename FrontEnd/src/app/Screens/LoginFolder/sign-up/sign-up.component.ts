import { User } from 'src/app/Model/User';
import { Component, OnInit } from '@angular/core';
import { ResponseValidation } from 'src/app/Model/ResponseValidation';
import { UserService } from 'src/app/Services/User.service';
import { UserValidationService } from 'src/app/Services/UserValidation.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent implements OnInit {
  public UserObject:User={};
  public UserValidationResponse:Array<ResponseValidation>=[{Isok:true,Message:''},{Isok:true ,Message:''},{Isok:true ,Message:''},
                                                          {Isok:true ,Message:''},{Isok:true ,Message:''},{Isok:true ,Message:''},{Isok:true ,Message:''}];
  public OpenEye:boolean=false;
  public SpinnerOn:boolean=false;
  public IsCreate:boolean=false;
  constructor(private HttpUser:UserService,private Validation:UserValidationService,private router:Router) { }

  ngOnInit(): void {
  }

  async ClickSingUp()
  {
    //6 הוא לבדיקת מייל
    this.UserValidationResponse[6].Isok=true;
    this.UserValidationResponse[6].Message='';

    this.UserValidationResponse[0]=this.Validation.CheckEmail(this.UserObject.Mail);
    this.UserValidationResponse[1]=this.Validation.CheckFirstName(this.UserObject.FirstName);
    this.UserValidationResponse[2]=this.Validation.CheckLastName(this.UserObject.LastName);
    this.UserValidationResponse[3]=this.Validation.CheckBirthdate(this.UserObject.Birthdate);
    this.UserValidationResponse[4]=this.Validation.CheckPhone(this.UserObject.Phone);
    this.UserValidationResponse[5]=this.Validation.CheckPassword(this.UserObject.Password);
      if(!this.UserValidationResponse[0].Isok||!this.UserValidationResponse[1].Isok||
        !this.UserValidationResponse[2].Isok||!this.UserValidationResponse[3].Isok||
        !this.UserValidationResponse[4].Isok||!this.UserValidationResponse[5].Isok)
      {
        this.SpinnerOn=false;
        return;
      }
      (await this.HttpUser.GetHaveUser(this.UserObject.Mail)).subscribe(
        (response)=>{this.UserValidationResponse[6].Isok=!response, this.AddNewUser();},
        ()=>{this.UserValidationResponse[6].Isok=false,this.UserValidationResponse[6].Message="חלה שגיאה אנא נסה שנית מאוחר יותר!",this.SpinnerOn=false;});
  }

  async AddNewUser()
  {
    if(!this.UserValidationResponse[6].Isok)
    {
      this.UserValidationResponse[6].Message="מייל קיים במערכת אנא נסה מייל אחר!";
      this.SpinnerOn=false;
      return;
    }

      (await this.HttpUser.AddUser(this.UserObject)).subscribe(
        ()=>{this.SpinnerOn=false, this.GoToLogin()},
        (error)=>{ console.log(error) , this.UserValidationResponse[6].Isok=false,this.UserValidationResponse[6].Message="בשל תקלה לא הצלחנו לשמור את המשתמש אנא נסה שנית מאוחר יותר",this.SpinnerOn=false;});
  }

  public GoToLogin()
  {
    this.IsCreate=true;
    setTimeout(()=>{ this.router.navigate(['/Login'])
    .then(() => {
      window.location.reload();
    });},3000);
  }

}
