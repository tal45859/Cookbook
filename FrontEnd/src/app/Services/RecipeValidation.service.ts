import { Injectable } from '@angular/core';
import { ResponseValidation } from '../Model/ResponseValidation';

@Injectable({
  providedIn: 'root'
})
export class RecipeValidationService {

constructor() { }


public CheckCategoryId(CategoryId?:number):ResponseValidation
{
  let Response:ResponseValidation={};
  //בדיקה האם המחרוזת ריקה
  if(CategoryId==null||CategoryId==undefined || CategoryId==0)
  {
   Response.Isok=false;
   Response.Message="אנא בחר קטגוריה!";
   return Response;
  }
   Response.Isok=true
   return  Response;
}

public CheckSubCategoryId(SubCategoryId?:number):ResponseValidation
{
  let Response:ResponseValidation={};
  //בדיקה האם המחרוזת ריקה
  if(SubCategoryId==null||SubCategoryId==undefined || SubCategoryId==0)
  {
   Response.Isok=false;
   Response.Message="אנא בחר תת קטגוריה!";
   return Response;
  }
   Response.Isok=true
   return  Response;
}

public CheckRecipeName(RecipeName?:string):ResponseValidation
{
  let Response:ResponseValidation={};
  //בדיקה האם המחרוזת ריקה
  if(RecipeName==null || RecipeName.length==0)
  {
   Response.Isok=false;
   Response.Message="אנא הזן שם מתכון!";
   return Response;
  }
  //בדיקה אם קיים במחרוזת תווים אסורים
   else if(/[^a-zA-Zא-ת ]/.test(RecipeName))
   {
     Response.Isok=false;
     Response.Message="אנא הזן אותיות באנגלית או בעברית בלבד!";
     return Response;
   }
   Response.Isok=true
   return  Response;
}

public CheckIngredients(Ingredients?:string):ResponseValidation
{
  let Response:ResponseValidation={};
  //בדיקה האם המחרוזת ריקה
  if(Ingredients==null || Ingredients.length==0)
  {
   Response.Isok=false;
   Response.Message="אנא הזן מצרכים!";
   return Response;
  }
  Response.Isok=true
  return  Response;
}

public CheckPreparationMethod(PreparationMethod?:string):ResponseValidation
{
  let Response:ResponseValidation={};
  //בדיקה האם המחרוזת ריקה
  if(PreparationMethod==null || PreparationMethod.length==0)
  {
   Response.Isok=false;
   Response.Message="אנא הזן אופן הכנה!";
   return Response;
  }
  Response.Isok=true
  return  Response;
}

public CheckPreparationTime(PreparationTime?:number):ResponseValidation
{
  let Response:ResponseValidation={};
  if(PreparationTime==null || PreparationTime.toString().length==0||PreparationTime==undefined || PreparationTime==0)
  {
   Response.Isok=false;
   Response.Message="אנא הזן זמן הכנה!";
   return Response;
  }
  else if(/[^0-9 ]/.test(PreparationTime.toString()))
  {
    Response.Isok=false;
    Response.Message="אנא הזן מספרים בדקות בלבד!";
    return Response;
  }
  Response.Isok=true
  return  Response;
}

public CheckQuantityOfPortions(QuantityOfPortions?:number):ResponseValidation
{
  let Response:ResponseValidation={};
  if(QuantityOfPortions==null || QuantityOfPortions.toString().length==0||QuantityOfPortions==undefined || QuantityOfPortions==0)
  {
   Response.Isok=false;
   Response.Message="אנא הזן כמות מנות!";
   return Response;
  }
  else if(/[^0-9 ]/.test(QuantityOfPortions.toString()))
  {
    Response.Isok=false;
    Response.Message="אנא הזן מספרים ללא נקודה עשרונית בלבד!";
    return Response;
  }
  Response.Isok=true
  return  Response;
}


public CheckCanBeExpected(CanBeExpected?:boolean):ResponseValidation
{
  let Response:ResponseValidation={};
  //בדיקה האם המחרוזת ריקה
  if(CanBeExpected==null || CanBeExpected==undefined)
  {
   Response.Isok=false;
   Response.Message="אנא הזן ניתן לצפיה!";
   return Response;
  }
  //בדיקה אם קיים במחרוזת תווים אסורים
   Response.Isok=true
   return  Response;
}


}
