import { LoginService } from './../../../../Services/Login.service';
import { UserService } from 'src/app/Services/User.service';
import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/Model/User';

@Component({
  selector: 'app-all-new-user',
  templateUrl: './all-new-user.component.html',
  styleUrls: ['./all-new-user.component.css']
})
export class AllNewUserComponent implements OnInit {
public NewUserArray:Array<User>=[];
public TypeDate:string='AllTheTime'; //Today=היום  Month=החודש    Year= השנה  AllTheTime=כל הזמנים
public page:number=0;
  constructor(private HttpUser:UserService,private HttpLogin:LoginService) { }

  async ngOnInit(): Promise<void> {
    await this.GetNewUserByDate();
  }

  async GetNewUserByDate()
  {
    (await this.HttpUser.GetCountRegisterDate(this.HttpLogin.Token,this.TypeDate)).subscribe((response)=>{this.NewUserArray=response,this.page=0;});
  }

}
