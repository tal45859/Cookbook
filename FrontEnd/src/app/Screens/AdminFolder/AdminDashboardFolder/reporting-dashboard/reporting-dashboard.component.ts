import { ReportingService } from './../../../../Services/Reporting.service';
import { LoginService } from './../../../../Services/Login.service';
import { Component, OnInit } from '@angular/core';
import { Reporting } from 'src/app/Model/Reporting';

@Component({
  selector: 'app-reporting-dashboard',
  templateUrl: './reporting-dashboard.component.html',
  styleUrls: ['./reporting-dashboard.component.css']
})
export class ReportingDashboardComponent implements OnInit {
  public ReportingArray:Array<Reporting>=[];
  public FilterReportingByType:number=1;
  public ResponseMesage:string='';
  public IdForDelete?:number=0;
  public ReportingObjectToUpdate:Reporting={};
  public IsDeleteOrUpdate:boolean=false;
  public page:number=0;
  constructor(private HttpLogin:LoginService,private HttpReporting:ReportingService) { }

  async ngOnInit(): Promise<void> {
    await this.GetAllReporting();
  }

  async GetAllReporting()
  {
    if(this.FilterReportingByType==1)
    {

      (await this.HttpReporting.GetAllReporting(this.HttpLogin.Token)).subscribe((response)=>{this.ReportingArray=response});
    }
    else if(this.FilterReportingByType==2)
    {
      (await this.HttpReporting.GetAllReportingNoActive(this.HttpLogin.Token)).subscribe((response)=>{this.ReportingArray=response});
    }
    else
    {
      (await this.HttpReporting.GetAllReportingActive(this.HttpLogin.Token)).subscribe((response)=>{this.ReportingArray=response});
    }
    this.page=0;

  }

  public ChangePanel(id?:number)
  {
    const ClassPanel= document?.getElementById(""+id)?.classList;
    if(ClassPanel?.toString() == "panelClose")//אם הוא שווה לו תפתח
    {
      document?.getElementById(""+id)?.classList.remove('panelClose');
      document?.getElementById(""+id)?.classList.add('panelOpen');
    }
    else //אם הוא שונה תסגור
    {
      document?.getElementById(""+id)?.classList.remove('panelOpen');
      document?.getElementById(""+id)?.classList.add('panelClose');
    }
  }

  async ClickDeleteReporting()
  {
     (await this.HttpReporting.DeleteReportingById(this.HttpLogin.Token,this.IdForDelete)).subscribe(
       ()=>{this.GetAllReporting()},
       ()=>{this.MessageError()}
     );
  }

  async UpdateReporting()
  {
    if(this.ReportingObjectToUpdate.ClosingExplanation==null ||this.ReportingObjectToUpdate.ClosingExplanation=='')
    {
      // הודעת שגיאה
      this.MessageError();
      return;
    }
    this.ReportingObjectToUpdate.IsActive=true;
    (await this.HttpReporting.UpdateReporting(this.HttpLogin.Token,this.ReportingObjectToUpdate)).subscribe(
      ()=>{this.GetAllReporting()},
      ()=>{this.MessageError()}
    );
  }

  public MessageError()
  {
    let id=!this.IsDeleteOrUpdate?this.IdForDelete:this.ReportingObjectToUpdate.Id;
    const element = document.getElementById("M"+id);
    element?.classList?.add("AddDisplay");
    element?.classList?.remove("removeDisplay");

    setTimeout(()=>{
      const element = document.getElementById("M"+id);
      element?.classList?.remove("AddDisplay");
      element?.classList?.add("removeDisplay");
    },3000);
  }



}
