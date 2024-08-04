import { Injectable } from '@angular/core';
import { ResponseValidation } from '../Model/ResponseValidation';

@Injectable({
  providedIn: 'root'
})
export class CommentValidationService {

constructor() { }

CheckTitle(Title?:string):ResponseValidation
 {
  let Response:ResponseValidation={};
  //בדיקה האם המחרוזת ריקה
  if(Title==null || Title.length==0)
  {
   Response.Isok=false;
   Response.Message="אנא הזן כותרת !";
   return Response;
  }
  //בדיקה אם קיים במחרוזת תווים אסורים
   else if(/[^a-zA-Zא-ת ]/.test(Title))
   {
     Response.Isok=false;
     Response.Message="אנא הזן אותיות באנגלית או בעברית בלבד!";
     return Response;
   }
   Response.Isok=true
   return  Response;
 }


 CheckdBody(Body?:string):ResponseValidation
 {
  let Response:ResponseValidation={};
  //בדיקה האם המחרוזת ריקה
  if(Body==null || Body.length==0)
  {
   Response.Isok=false;
   Response.Message="אנא הזן גוף תגובה!";
   return Response;
  }
  //בדיקה אם קיים במחרוזת תווים אסורים
   else if(/[^a-zA-Zא-ת ]/.test(Body))
   {
     Response.Isok=false;
     Response.Message="אנא הזן אותיות באנגלית או בעברית בלבד!";
     return Response;
   }
   Response.Isok=true
   return  Response;
 }

}
