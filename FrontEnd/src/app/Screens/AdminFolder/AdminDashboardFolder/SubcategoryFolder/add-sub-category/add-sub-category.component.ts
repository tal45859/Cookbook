import { SubcategoryValidationService } from 'src/app/Services/SubcategoryValidation.service';
import { SubcategoryService } from './../../../../../Services/Subcategory.service';
import { CategoryService } from './../../../../../Services/Category.service';
import { LoginService } from './../../../../../Services/Login.service';
import { Subcategory } from './../../../../../Model/Subcategory';
import { Component, OnInit } from '@angular/core';
import { ResponseValidation } from 'src/app/Model/ResponseValidation';
import { Category } from 'src/app/Model/Category';

@Component({
  selector: 'app-add-sub-category',
  templateUrl: './add-sub-category.component.html',
  styleUrls: ['./add-sub-category.component.css']
})
export class AddSubCategoryComponent implements OnInit {
  public SubcategoryObjToAdd:Subcategory={};
  public AllCaegoryArray:Array<Category>=[];
  public IsCreate:boolean=false;
  public SpinnerOn:boolean=false;
  public ResponseErrorMesage:Array<ResponseValidation>=[{Isok:true,Message:''},{Isok:true,Message:'אנא בחר קטגוריה ראשית'},{Isok:true,Message:'לא הצלחנו להוסיף את התת קטגוריה אנא נסה שנית מאוחר יותר!'}];
  constructor(private HttpLogin:LoginService,private HttpCategory:CategoryService,private HttpSubcategory:SubcategoryService,private Validation:SubcategoryValidationService) { }

  async ngOnInit(): Promise<void> {
    await this.GetAllCategory();
  }

  async GetAllCategory()
  {
    (await this.HttpCategory.GetAllCategory()).subscribe((response)=>{this.AllCaegoryArray=response});
  }

  async ClickAddSubategory()
  {
    this.ResponseErrorMesage[0]=this.Validation.CheckCategoryName(this.SubcategoryObjToAdd.SubcategoryName);
    if(!this.ResponseErrorMesage[0].Isok)
    {
      this.MessageError();
      return;
    }
    if(this.SubcategoryObjToAdd.CategoryId==null)
    {
      this.ResponseErrorMesage[1].Isok=false;
      this.MessageError();
      return;
    }
    (await this.HttpSubcategory.AddSubcategory(this.HttpLogin.Token,this.SubcategoryObjToAdd)).subscribe(
      ()=>{this.SpinnerOn=false;this.IsCreate=true},
      ()=>{this.ResponseErrorMesage[2].Isok=false;this.MessageError()}
    );
  }

  public MessageError()
  {
    this.SpinnerOn=false;
    setTimeout(()=>{
      this.ResponseErrorMesage[0]={Isok:true,Message:''};
      this.ResponseErrorMesage[1].Isok=true;
      this.ResponseErrorMesage[2].Isok=true;
    },3000);
  }

}
