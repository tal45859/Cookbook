import { ReportingService } from './../../../Services/Reporting.service';
import { Reporting } from './../../../Model/Reporting';
import { Component, Input, OnInit } from '@angular/core';
import { ResponseValidation } from 'src/app/Model/ResponseValidation';
import { ReportingValidationService } from 'src/app/Services/ReportingValidation.service';

@Component({
  selector: 'app-add-reporting',
  templateUrl: './add-reporting.component.html',
  styleUrls: ['./add-reporting.component.css']
})
export class AddReportingComponent implements OnInit {
  @Input() RecipeIdToReporting?:number=0;
  public ObjectToAddReporting:Reporting={};
  public IsCreate:boolean=false;
  public SpinnerOn:boolean=false;
  public ResponseMessage:Array<ResponseValidation>=[{Isok:true,Message:''},{Isok:true,Message:''}];
  constructor(private HttpReporting:ReportingService,private Validation:ReportingValidationService) { }

  ngOnInit(): void {
  }
  async AddReporting()
  {
    this.ObjectToAddReporting.RecipeId=this.RecipeIdToReporting;
    this.ResponseMessage[0]=this.Validation.CheckCause(this.ObjectToAddReporting.Cause);
    if(!this.ResponseMessage[0].Isok)
    {
      this.SpinnerOn=false;
      return;
    }
    (await this.HttpReporting.AddReporting(this.ObjectToAddReporting)).subscribe(
      ()=>{this.SpinnerOn=false,this.SetTimeoutFun()},
      ()=>{this.SpinnerOn=false,this.ResponseMessage[1]={Isok:false,Message:'לא הצלחנו להוסיף את הדיווח אנא נסה שנית מאוחר יותר!'}}
    )
  }
  public SetTimeoutFun()
  {
    this.IsCreate=true;
    setTimeout(()=>{
      this.IsCreate=false;
    },3000);
  }
}

