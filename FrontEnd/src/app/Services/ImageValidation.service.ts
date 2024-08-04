import { ResponseValidation } from './../Model/ResponseValidation';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ImageValidationService {

constructor() { }

public CheckImage(ImageName?:string):ResponseValidation
{
  let MessageObj:ResponseValidation={};
  if(ImageName?.toString().endsWith(".jpg") || ImageName?.toString().endsWith(".jpeg") || ImageName?.toString().endsWith(".png"))
  {
    MessageObj.Isok=true;
    return MessageObj;
  }
  MessageObj.Message="אנא הכנס קובץ מסוג תמונה בלבד: png ,jpg , jpeg";
  MessageObj.Isok=false;
  return MessageObj;
}

}
