import { Injectable } from '@angular/core';
import { ResponseValidation } from '../Model/ResponseValidation';

@Injectable({
  providedIn: 'root'
})
export class SubcategoryValidationService {

constructor() { }

CheckCategoryName(SubcategoryName?:string):ResponseValidation
 {
  let Response:ResponseValidation={};
  //בדיקה האם המחרוזת ריקה
  if(SubcategoryName==null || SubcategoryName.length==0)
  {
   Response.Isok=false;
   Response.Message="אנא הזן שם תת קטגוריה!";
   return Response;
  }
  //בדיקה אם קיים במחרוזת תווים אסורים
   else if(/[^a-zA-Zא-ת ]/.test(SubcategoryName))
   {
     Response.Isok=false;
     Response.Message="אנא הזן אותיות באנגלית או בעברית בלבד!";
     return Response;
   }
   Response.Isok=true
   return  Response;
 }



}
