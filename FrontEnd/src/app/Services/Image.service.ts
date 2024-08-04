import { Image } from './../Model/Image';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ImageService {

//תקציר
//////////////
//בנאי
//הוספת תמונה חדשה לבסיס נתונים
//הוספת תמונה ישירות לתיקיה
//קבלת תמונה לפי מזהה
//קבלת רשימת תמונות לפי מזהה מתכון
//מחיקת תמונה לוודא שזה משתמש ששיכת לו התמונה
//מחיקת תמונה לוודא שזה מנהל
//קבלת כל התמונות של המתכונים שאפשר לראות
//JWTקבלת כל התמונות על פי

  endPointApi="https://localhost:44328/api/Image/";

  //בנאי
  constructor(private http:HttpClient) { }

//הוספת תמונה חדשה לבסיס נתונים
async AddImageToDB(ImageToAdd:Image,Token:string): Promise<Observable<boolean>>
  {
    return this.http.post<boolean>(this.endPointApi+"AddImageToDB",ImageToAdd, {
      headers: new HttpHeaders().set('Authorization', ""+Token)});
  };

//הוספת תמונה ישירות לתיקיה
async AddImageToFolder(file:FormData, Token:string): Promise<Observable<string>>
{
  return this.http.post<string>(this.endPointApi+"AddImageToFolder",file,{
    headers: new HttpHeaders().set('Authorization', ""+Token)});
};

//קבלת תמונה לפי מזהה
async GetImageById(ImageId:number): Promise<Observable<Image>>
{
  return this.http.get<Image>(this.endPointApi+"GetImageById/"+ImageId);
};

//קבלת רשימת תמונות לפי מזהה מתכון
async GetAllImageByRecipeId(RecipeId?:number): Promise<Observable<Array<Image>>>
{
  return this.http.get<Array<Image>>(this.endPointApi+"GetAllImageByRecipeId/"+RecipeId);
};

//מחיקת תמונה לוודא שזה משתמש ששיכת לו התמונה
async DeleteImageForUser(Token:string,ImageId?:number): Promise<Observable<any>>
  {
    return this.http.delete<any>(this.endPointApi+"DeleteImageForUser/"+ImageId, {
      headers: new HttpHeaders().set('Authorization', Token)});
  };

//מחיקת תמונה לוודא שזה מנהל
async DeleteImageForAdmin(ImageId:number,Token:string): Promise<Observable<any>>
  {
    return this.http.delete<any>(this.endPointApi+"DeleteImageForAdmin/"+ImageId, {
      headers: new HttpHeaders().set('Authorization',""+ Token)});
  };

  //קבלת כל התמונות של המתכונים שאפשר לראות
  async GetAllImageCanBeExpected(): Promise<Observable<Array<Image>>>
  {
    return this.http.get<Array<Image>>(this.endPointApi+"GetAllImageCanBeExpected");
  };

  //JWTקבלת כל התמונות על פי
  async GetAllImageByJWT(Token:string): Promise<Observable<Array<Image>>>
  {
    return this.http.get<Array<Image>>(this.endPointApi+"GetAllImageByJWT",{
      headers: new HttpHeaders().set('Authorization', ""+Token)});
  }

}
