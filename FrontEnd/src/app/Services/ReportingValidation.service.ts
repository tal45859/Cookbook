import { Injectable } from '@angular/core';
import { ResponseValidation } from '../Model/ResponseValidation';

@Injectable({
  providedIn: 'root'
})
export class ReportingValidationService {

constructor() { }

CheckCause(Cause?:string):ResponseValidation
 {
  let Response:ResponseValidation={};
  //בדיקה האם המחרוזת ריקה
  if(Cause==null || Cause.length==0)
  {
   Response.Isok=false;
   Response.Message="אנא הזן סיבת דיווח !";
   return Response;
  }
  //בדיקה אם קיים במחרוזת תווים אסורים
   else if(/[^a-zA-Zא-ת ]/.test(Cause))
   {
     Response.Isok=false;
     Response.Message="אנא הזן אותיות באנגלית או בעברית בלבד!";
     return Response;
   }
   Response.Isok=true
   return  Response;
 }
}
