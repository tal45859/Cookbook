import { ResponseValidation } from './../../../Model/ResponseValidation';
import { Router } from '@angular/router';
import { LoginService } from './../../../Services/Login.service';
import { UserService } from 'src/app/Services/User.service';
import { Component, Input, OnInit } from '@angular/core';
import { User } from 'src/app/Model/User';

@Component({
  selector: 'app-user-details',
  templateUrl: './user-details.component.html',
  styleUrls: ['./user-details.component.css']
})
export class UserDetailsComponent implements OnInit {
 @Input() UserObjectInput:User={};
 public RsponseMessage:ResponseValidation={Isok:true,Message:''};
  constructor(private HttpUser:UserService,private HttpLogin:LoginService,private router:Router) { }

  ngOnInit(): void {
  }

  async ClickDelete()
  {
    (await this.HttpUser.DeleteUserByToken(this.HttpLogin.Token)).subscribe(
      ()=>{window.sessionStorage.clear(),
        this.router.navigate(['/Home'])
        .then(() => {
          window.location.reload();
        })},
        ()=>{this.MessageError()}
    );
  }

  public MessageError()
  {
    this.RsponseMessage.Isok=false;
    this.RsponseMessage.Message='לא הצלחנו למחוק את המשתמש אנא נסה שנית מאוחר יותר!';
    setTimeout(()=>{
      this.RsponseMessage.Isok=true;
    },3000);
  }

}
