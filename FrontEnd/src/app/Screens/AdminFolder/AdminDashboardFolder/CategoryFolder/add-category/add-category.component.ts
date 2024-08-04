import { Component, OnInit } from '@angular/core';
import { Category } from 'src/app/Model/Category';
import { ResponseValidation } from 'src/app/Model/ResponseValidation';
import { CategoryService } from 'src/app/Services/Category.service';
import { CategoryValidationService } from 'src/app/Services/CategoryValidation.service';
import { LoginService } from 'src/app/Services/Login.service';

@Component({
  selector: 'app-add-category',
  templateUrl: './add-category.component.html',
  styleUrls: ['./add-category.component.css']
})
export class AddCategoryComponent implements OnInit {
  public CategoryObjToAdd:Category={};
  public IsCreate:boolean=false;
  public SpinnerOn:boolean=false;
  public ResponseErrorMesage:ResponseValidation={Isok:true,Message:''};

  constructor(private HttpLogin:LoginService,private HttpCategory:CategoryService,private Validation:CategoryValidationService) { }

  ngOnInit(): void {
  }

  async ClickAddCategory()
  {
    this.ResponseErrorMesage=this.Validation.CheckCategoryName(this.CategoryObjToAdd.CategoryName);
    if(!this.ResponseErrorMesage.Isok)
    {
      this.SpinnerOn=false;
      return;
    }
    (await this.HttpCategory.AddCategory(this.HttpLogin.Token,this.CategoryObjToAdd)).subscribe(
      ()=>{this.SetTimeoutFun(), this.SpinnerOn=false;},
      ()=>{this.ResponseErrorMesage={Isok:false,Message:"לא הצלחנו להוסיף את הקטגוריה אנא נסה שנית מאוחריותר!"}, this.SpinnerOn=false;}
    );
    this.CategoryObjToAdd={};
  }

  public SetTimeoutFun()
  {
    this.IsCreate=true;
    setTimeout(()=>{
      this.IsCreate=false;
    },3000);
  }


}
