import { LoginService } from './../../../Services/Login.service';
import { UserService } from 'src/app/Services/User.service';
import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/Model/User';

@Component({
  selector: 'app-user-home',
  templateUrl: './user-home.component.html',
  styleUrls: ['./user-home.component.css']
})
export class UserHomeComponent implements OnInit {
public OpenChildren:number=1;
public UserObject:User={};

  constructor(private HttpUser:UserService,private HttpLogin:LoginService) { }

  async ngOnInit(): Promise<void> {
    await this.GetUserByToken();
  }

  async GetUserByToken()
  {
    (await this.HttpUser.GetUserByToken(await this.HttpLogin.Token)).subscribe(
      (response)=>{this.UserObject=response}
    )
  }

}
