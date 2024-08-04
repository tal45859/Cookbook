import { LoginHistoryService } from './../../../../Services/LoginHistory.service';
import { LoginService } from './../../../../Services/Login.service';
import { Component, OnInit } from '@angular/core';
import { LoginHistory } from 'src/app/Model/LoginHistory';

@Component({
  selector: 'app-all-login-history',
  templateUrl: './all-login-history.component.html',
  styleUrls: ['./all-login-history.component.css']
})
export class AllLoginHistoryComponent implements OnInit {
  public LoginHistoryArray:Array<LoginHistory>=[];
  public TypeDate:string='AllTheTime'; //Today=היום  Month=החודש    Year= השנה  AllTheTime=כל הזמנים
  public page:number=0;
  constructor(private HttpLogin:LoginService,private HttpLoginHistory:LoginHistoryService) { }

  async ngOnInit(): Promise<void> {
    await this.GetLoginHistoryByDate();
  }

  async GetLoginHistoryByDate()
  {
    (await this.HttpLoginHistory.GetLoginHistoryFilteringByDate(this.HttpLogin.Token,this.TypeDate)).subscribe(
      (response)=>{this.LoginHistoryArray=response,this.page=0;}
    );
  }

}
