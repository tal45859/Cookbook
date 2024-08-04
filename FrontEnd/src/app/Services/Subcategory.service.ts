import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Subcategory } from '../Model/Subcategory';

@Injectable({
  providedIn: 'root'
})
export class SubcategoryService {
  //תקציר
  ////////////
  //בנאי
  //קבלת תת קטגוריה לפי מזהה תת קטגוריה
  //קבלת רשימת תתי קטגוריה עם אוביקט קטגוריה
  //קבלת אוביקט תת קטגוריה לפי מזהה קטגוריה
  //קבלת רשימת תתי קטגוריה של קטגוריה לפי מזהה קטגוריה
  // הוספת תת קטגוריה
  // עדכון תת קטגוריה
  // מחיקת תת קטגוריה

  endPointApi="https://localhost:44328/api/Subcategory/";

  //בנאי
  constructor(private http:HttpClient) { }

  //קבלת תת קטגוריה לפי מזהה תת קטגוריה
  async GetSubcategoryById(SubcategoryId:number): Promise<Observable<Subcategory>>
  {
    return this.http.get<Subcategory>(this.endPointApi+"GetSubcategoryById/"+SubcategoryId);
  }

  //קבלת רשימת תתי קטגוריה עם אוביקט קטגוריה
  async GetAllSubcategory(): Promise<Observable<Array<Subcategory>>>
  {
    return this.http.get<Array<Subcategory>>(this.endPointApi+"GetAllSubcategory");
  }

  //קבלת אוביקט תת קטגוריה לפי מזהה קטגוריה
  async GetSubcategoryByCategoryId(CategoryId?:number): Promise<Observable<Subcategory>>
  {
    return this.http.get<Subcategory>(this.endPointApi+"GetSubcategoryByCategoryId/"+CategoryId);
  }

  //קבלת רשימת תתי קטגוריה של קטגוריה לפי מזהה קטגוריה
  async GetAllSubcategoryByCategoryId(CategoryId?:number): Promise<Observable<Array<Subcategory>>>
  {
    return this.http.get<Array<Subcategory>>(this.endPointApi+"GetAllSubcategoryByCategoryId/"+CategoryId);
  }

  // הוספת תת קטגוריה
  async AddSubcategory(Token:string,SubcategoryToAdd:Subcategory): Promise<Observable<boolean>>
  {
   return this.http.post<boolean>(this.endPointApi+"AddSubcategory",SubcategoryToAdd, {
    headers: new HttpHeaders().set('Authorization',""+Token)});
  }

  // עדכון תת קטגוריה
  async UpdateSubcategory(Token:string,SubcategoryToUpdate?:Subcategory): Promise<Observable<any>>
  {
   return this.http.put<any>(this.endPointApi+"UpdateSubcategory",SubcategoryToUpdate, {
    headers: new HttpHeaders().set('Authorization',""+Token)});
  }

  // מחיקת תת קטגוריה
  async DeleteSubcategory(Token:string,SubcategoryId?:number): Promise<Observable<any>>
  {
   return this.http.delete<any>(this.endPointApi+"DeleteSubcategory/"+SubcategoryId, {
    headers: new HttpHeaders().set('Authorization',""+Token)});
  }
}
