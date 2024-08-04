import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Category } from '../Model/Category';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
//תקציר
//////////
// בנאי
// קבלת קטגוריה לפי מזהה
// קבלת קטגוריה לפי שם קטגוריה
// קבלת כל הקטגוריות
// יצירת קטגוריה
// מחיקת קטגוריה
// עדכון קטגוריה

  endPointApi="https://localhost:44328/api/Category/";

  //בנאי
  constructor(private http:HttpClient) { }


// קבלת קטגוריה לפי מזהה
async GetCategoryById(CategoryId:number): Promise<Observable<Category>>
{
 return this.http.get<Category>(this.endPointApi+"GetCategoryById/"+CategoryId);
}

// קבלת קטגוריה לפי שם קטגוריה
async GetCategoryByCategoryName(CategoryName:string): Promise<Observable<Category>>
{
 return this.http.get<Category>(this.endPointApi+"GetCategoryByCategoryName/"+CategoryName);
}

// קבלת כל הקטגוריות
async GetAllCategory(): Promise<Observable<Array<Category>>>
{
 return this.http.get<Array<Category>>(this.endPointApi+"GetAllCategory");
}

// יצירת קטגוריה
async AddCategory(Token:string,CategoryToAdd:Category): Promise<Observable<boolean>>
{
 return this.http.post<boolean>(this.endPointApi+"AddCategory",CategoryToAdd, {
  headers: new HttpHeaders().set('Authorization',""+Token)});
}

// מחיקת קטגוריה
async DeleteCategory(Token:string,CategoryId?:number): Promise<Observable<any>>
{
 return this.http.delete<any>(this.endPointApi+"DeleteCategory/"+CategoryId, {
  headers: new HttpHeaders().set('Authorization',""+Token)});
}

// עדכון קטגוריה
async UpdateCategory(Token:string,CategoryToUpdate:Category): Promise<Observable<any>>
{
 return this.http.put<any>(this.endPointApi+"UpdateCategory",CategoryToUpdate, {
  headers: new HttpHeaders().set('Authorization',""+Token)});
}
}
