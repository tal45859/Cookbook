import { LoginHistoryService } from './../../../Services/LoginHistory.service';
import { UserValidationService } from '../../../Services/UserValidation.service';
import { ResponseValidation } from '../../../Model/ResponseValidation';
import { UserService } from '../../../Services/User.service';
import { Login } from '../../../Model/Login';
import { LoginService } from '../../../Services/Login.service';
import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/Model/User';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  public Token:string="";
  public LoginObject:Login={};
  public UserObject:User={};
  public SpinnerOn:boolean=false;
  public OpenEye:boolean=false;
  public UserValidationResponse:Array<ResponseValidation>=[{Isok:true,Message:''},{Isok:true ,Message:''},{Isok:true ,Message:''}];
  constructor(private HttpLogin:LoginService,private HttpUser:UserService,private Validation:UserValidationService,private router:Router,
    private HttpLoginHistory:LoginHistoryService) { }

  ngOnInit(): void {
  }

  async ClickLogin()
  {

    this.UserValidationResponse[0]=this.Validation.CheckEmail(this.LoginObject.Email);
    this.UserValidationResponse[1]=this.Validation.CheckPassword(this.LoginObject.Password);
    this.UserValidationResponse[2].Isok=true;
    if(!this.UserValidationResponse[0].Isok||!this.UserValidationResponse[1].Isok)
    {
      this.SpinnerOn=false;
      return;
    }
    (await this.HttpLogin.GetAtuh(this.LoginObject)).subscribe((response)=>{this.Token = response ,this.HttpLogin.Token=response,
      this.AddHttpLoginHistory(),this.GetUserByToken();},
    (error) => {this.UserValidationResponse[2].Isok=false,this.UserValidationResponse[2].Message="מייל או סיסמה אינם נכונים !",this.SpinnerOn=false;});
  }

  async GetUserByToken()
  {
    (await this.HttpUser.GetUserByToken(this.Token)).subscribe(
      response=>{this.UserObject = response,this.HttpLogin.Navbar=String(this.UserObject.Role)
        ,this.HttpLogin.Role=String(this.UserObject.Role)
        ,this.router.navigate(['/Home'])
        .then(() => {
          window.location.reload();
        })
      });
  }

  public ClickOpenEye()
  {
    this.OpenEye=!this.OpenEye;
  }

  async AddHttpLoginHistory()
  {
    (await this.HttpLoginHistory.AddLoginHistory(this.Token)).subscribe();
  }


}
