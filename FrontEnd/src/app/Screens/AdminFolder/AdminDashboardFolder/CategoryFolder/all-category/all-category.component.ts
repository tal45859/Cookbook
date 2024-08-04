import { Component, OnInit } from '@angular/core';
import { Category } from 'src/app/Model/Category';
import { ResponseValidation } from 'src/app/Model/ResponseValidation';
import { CategoryService } from 'src/app/Services/Category.service';
import { CategoryValidationService } from 'src/app/Services/CategoryValidation.service';
import { LoginService } from 'src/app/Services/Login.service';

@Component({
  selector: 'app-all-category',
  templateUrl: './all-category.component.html',
  styleUrls: ['./all-category.component.css']
})
export class AllCategoryComponent implements OnInit {
  public CategoryArray:Array<Category>=[];
  public CategoryObjToUpdate:Category={};
  public IdForDelete?:number=0;
  public IsDeleteOUpdate:boolean=false;//false= delete // true=update
  public page:number=0;
  public ResponseMesage:ResponseValidation={Isok:true,Message:''};
  constructor(private HttpLogin:LoginService,private HttpCategory:CategoryService,private Validation:CategoryValidationService) { }


  async ngOnInit(): Promise<void> {
    await this.GetAllCategory();
  }


  async GetAllCategory()
  {
    (await this.HttpCategory.GetAllCategory()).subscribe(
      (response)=>{this.CategoryArray=response}
    );
  }

  async ClickDeleteCategory()
  {
    (await this.HttpCategory.DeleteCategory(this.HttpLogin.Token,this.IdForDelete)).subscribe(
      ()=>{this.GetAllCategory(),this.page=0},
      ()=>{this.MessageError()}
    );
  }

  async UpdateCategory()
  {
    this.ResponseMesage=this.Validation.CheckCategoryName(this.CategoryObjToUpdate.CategoryName);
    if(!this.ResponseMesage.Isok)
    {
      this.MessageError();
      return;
    }
    (await this.HttpCategory.UpdateCategory(this.HttpLogin.Token,this.CategoryObjToUpdate)).subscribe(
      ()=>{this.GetAllCategory()},
      ()=>{this.ResponseMesage={Isok:false ,Message:'לא הצלחנו לעדכן את שם הקטגוריה אנא נסה שנית מאוחר יותר!'},this.MessageError()}
    );
  }

  public MessageError()
  {
    let id=!this.IsDeleteOUpdate?this.IdForDelete:this.CategoryObjToUpdate.Id;
    const element = document.getElementById(""+id);
    element?.classList?.add("AddDisplay");
    element?.classList?.remove("removeDisplay");

    setTimeout(()=>{
      const element = document.getElementById(""+id);
      element?.classList?.remove("AddDisplay");
      element?.classList?.add("removeDisplay");
      this.ResponseMesage={Isok:true,Message:''};
    },3000);
  }
}
