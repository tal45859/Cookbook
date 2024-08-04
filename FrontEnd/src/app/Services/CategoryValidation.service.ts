import { ResponseValidation } from './../Model/ResponseValidation';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CategoryValidationService {

constructor() { }

 CheckCategoryName(CategoryName?:string):ResponseValidation
 {
  let Response:ResponseValidation={};
  //בדיקה האם המחרוזת ריקה
  if(CategoryName==null || CategoryName.length==0)
  {
   Response.Isok=false;
   Response.Message="אנא הזן שם קטגוריה!";
   return Response;
  }
  //בדיקה אם קיים במחרוזת תווים אסורים
   else if(/[^a-zA-Zא-ת ]/.test(CategoryName))
   {
     Response.Isok=false;
     Response.Message="אנא הזן אותיות באנגלית או בעברית בלבד!";
     return Response;
   }
   Response.Isok=true
   return  Response;
 }

}
