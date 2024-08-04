import { UserValidationService } from '../../../Services/UserValidation.service';
import { UserService } from '../../../Services/User.service';
import { ResponseValidation } from '../../../Model/ResponseValidation';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css']
})
export class ForgotPasswordComponent implements OnInit {

  public SpinnerOn:boolean=false;
  public ShowMesage:boolean=false;
  public EmailForForgotPassword:string='';
  public EmailValidationResponse:ResponseValidation={Isok:true,Message:''}
  constructor(private HttpUser:UserService,private Validation:UserValidationService) { }

  ngOnInit(): void {
  }

  async ClickOk()
  {
    this.EmailValidationResponse=this.Validation.CheckEmail(this.EmailForForgotPassword);
    if(!this.EmailValidationResponse.Isok)
    {
      this.SpinnerOn=false
      return;
    }
    (await this.HttpUser.ForgotPassword(this.EmailForForgotPassword)).subscribe(
      ()=>{this.SpinnerOn=false,this.ShowMesage=true},
      ()=>{this.SpinnerOn=false,this.ShowMesage=true});
  }

}
