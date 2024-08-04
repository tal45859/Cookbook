import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { LoginHistory } from '../Model/LoginHistory';

@Injectable({
  providedIn: 'root'
})
export class LoginHistoryService {
  //תקציר
  //////////
  //בנאי
  //הוספת היסטוריה
  //מחיקת היסטוריה לפי מזהה היסטוריה
  //קבלת היסטוריה לפי מזהה היסטוריה עם אוביקט
  //קבלת רשימת היסטוריה של כל המשתמשים עם אוביקטים
  //קבלת רשימת היסטוריה של משתמש בודד לפי מזהה משתמש
  //קבלת רשימות של משתמשים שהתחברו אם אפשרות סינון של היום השבוע החודש והשנה וכל הזמן

  endPointApi="https://localhost:44328/api/LoginHistory/";

  //בנאי
  constructor(private http:HttpClient) { }

  //הוספת היסטוריה
  async AddLoginHistory(Token:string): Promise<Observable<boolean>>
  {
   return this.http.post<boolean>(this.endPointApi+"AddLoginHistory",{}, {
    headers: new HttpHeaders().set('Authorization',""+Token)});
  }

  //מחיקת היסטוריה לפי מזהה היסטוריה
  async DeleteLoginHistoryById(Token:string,LoginHistoryId:number): Promise<Observable<boolean>>
  {
   return this.http.delete<boolean>(this.endPointApi+"DeleteLoginHistoryById/"+LoginHistoryId, {
    headers: new HttpHeaders().set('Authorization',""+Token)});
  }

  //קבלת היסטוריה לפי מזהה היסטוריה עם אוביקט
  async GetLoginHistoryById(Token:string,LoginHistoryId:number): Promise<Observable<LoginHistory>>
  {
   return this.http.get<LoginHistory>(this.endPointApi+"GetLoginHistoryById/"+LoginHistoryId, {
    headers: new HttpHeaders().set('Authorization',""+Token)});
  }

  //קבלת רשימת היסטוריה של כל המשתמשים עם אוביקטים
  async GetAllLoginHistory(Token:string): Promise<Observable<Array<LoginHistory>>>
  {
   return this.http.get<Array<LoginHistory>>(this.endPointApi+"GetAllLoginHistory", {
    headers: new HttpHeaders().set('Authorization',""+Token)});
  }

  //קבלת רשימת היסטוריה של משתמש בודד לפי מזהה משתמש
  async GetAllLoginHistoryById(Token:string,LoginHistoryId:number): Promise<Observable<Array<LoginHistory>>>
  {
   return this.http.get<Array<LoginHistory>>(this.endPointApi+"GetAllLoginHistoryById/"+LoginHistoryId, {
    headers: new HttpHeaders().set('Authorization',""+Token)});
  }

  //קבלת רשימות של משתמשים שהתחברו אם אפשרות סינון של היום השבוע החודש והשנה וכל הזמן
  //Today=היום   Week=השבוע  Month=החודש   AllTheTime=כל הזמנים   Year= השנה
  async GetLoginHistoryFilteringByDate(Token:string,RequestDate:string): Promise<Observable<Array<LoginHistory>>>
  {
   return this.http.get<Array<LoginHistory>>(this.endPointApi+"GetLoginHistoryFilteringByDate/"+RequestDate, {
    headers: new HttpHeaders().set('Authorization',""+Token)});
  }
}
