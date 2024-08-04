import { Subcategory } from './../../../../../Model/Subcategory';
import { ResponseValidation } from './../../../../../Model/ResponseValidation';
import { SubcategoryService } from './../../../../../Services/Subcategory.service';
import { CategoryService } from './../../../../../Services/Category.service';
import { LoginService } from './../../../../../Services/Login.service';
import { Component, OnInit } from '@angular/core';
import { Category } from 'src/app/Model/Category';
import { SubcategoryValidationService } from 'src/app/Services/SubcategoryValidation.service';

@Component({
  selector: 'app-all-sub-category',
  templateUrl: './all-sub-category.component.html',
  styleUrls: ['./all-sub-category.component.css']
})
export class AllSubCategoryComponent implements OnInit {
  public AllCategoryArray:Array<Category>=[];
  public AllSubCategoryArray:Array<Subcategory>=[];
  public IdForDelete?:number=0;
  public IsDeleteOrUpdate:boolean=false;
  public SubCategoryObjectToUpdate:Subcategory={};
  public ResponseMesage:ResponseValidation={};

  constructor(private HttpLogin:LoginService,private HtppCategory:CategoryService,private HttpSubCategory:SubcategoryService,private Validation:SubcategoryValidationService) { }

  async ngOnInit(): Promise<void> {
    await this.GetAllCategory();
    await this.GetAllSubCategory();
  }

  async GetAllCategory()
  {
    (await this.HtppCategory.GetAllCategory()).subscribe((response)=>{this.AllCategoryArray=response});
  }

  async GetAllSubCategory()
  {
    (await this.HttpSubCategory.GetAllSubcategory()).subscribe((response)=>{this.AllSubCategoryArray = response});
  }

  public ChangePanel(id?:number)
  {
    const ClassPanel= document?.getElementById(""+id)?.classList;
    if(ClassPanel?.toString() == "removeDisplay")//אם הוא שווה לו תפתח
    {
      document?.getElementById(""+id)?.classList.remove('removeDisplay');
      document?.getElementById(""+id)?.classList.add('AddDisplay');
    }
    else //אם הוא שונה תסגור
    {
      document?.getElementById(""+id)?.classList.remove('AddDisplay');
      document?.getElementById(""+id)?.classList.add('removeDisplay');
    }
  }

  async ClickDelete()
  {
    (await this.HttpSubCategory.DeleteSubcategory(this.HttpLogin.Token,this.IdForDelete)).subscribe(
      ()=>{this.GetAllSubCategory()},
      ()=>{this.ResponseMesage={Isok:false ,Message:'לא הצלחנו למחוק את תת הקטגוריה אנא נסה שנית מאוחר יותר!'},this.MessageError()});
  }

  async ClickUpdate()
  {
    this.ResponseMesage=this.Validation.CheckCategoryName(this.SubCategoryObjectToUpdate.SubcategoryName);
    if(!this.ResponseMesage.Isok)
    {
      this.MessageError();
      return;
    }
    this.SubCategoryObjectToUpdate.Category=undefined;
    (await this.HttpSubCategory.UpdateSubcategory(this.HttpLogin.Token,this.SubCategoryObjectToUpdate)).subscribe(
      ()=>{this.GetAllSubCategory()},
      ()=>{this.ResponseMesage={Isok:false ,Message:'לא הצלחנו לעדכן את שם תת הקטגוריה אנא נסה שנית מאוחר יותר!'},this.MessageError()});
  }



  public MessageError()
  {
    let id=!this.IsDeleteOrUpdate?this.IdForDelete:this.SubCategoryObjectToUpdate.Id;
    const element = document.getElementById("Error"+id);
    element?.classList?.add("AddDisplay");
    element?.classList?.remove("removeDisplay");

    setTimeout(()=>{
      const element = document.getElementById("Error"+id);
      element?.classList?.remove("AddDisplay");
      element?.classList?.add("removeDisplay");
      this.ResponseMesage={Isok:true,Message:''};
    },3000);
  }



}
