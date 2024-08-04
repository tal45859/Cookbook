import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Reporting } from '../Model/Reporting';

@Injectable({
  providedIn: 'root'
})
export class ReportingService {

//תקציר
///////////
//בנאי
//קבלת דיווח לפי מזהה
//הוספת דיווח
//מחיקת דיווח
//שינוי מצב דיווח לפתור והוספת הערת סיבת סיום
//קבלת רשימת דיווחים לא פתורים
//קבלת רשימת דיווחים פתורים
//קבלת רשימת כל הדיווחים
//קבלת רשימת דיווחים לפי מזהה מתכון

endPointApi="https://localhost:44328/api/Reporting/";

//בנאי
constructor(private http:HttpClient) { }

//קבלת דיווח לפי מזהה
async GetReportingById(Token:string,ReportingId:number): Promise<Observable<Reporting>>
{
 return this.http.get<Reporting>(this.endPointApi+"GetReportingById/"+ReportingId, {
  headers: new HttpHeaders().set('Authorization',""+Token)});
}

//הוספת דיווח
async AddReporting(ReportingToAdd:Reporting): Promise<Observable<boolean>>
{
 return this.http.post<boolean>(this.endPointApi+"AddReporting",ReportingToAdd);
}

//מחיקת דיווח
async DeleteReportingById(Token:string,ReportingId?:number): Promise<Observable<any>>
{
 return this.http.delete<any>(this.endPointApi+"DeleteReportingById/"+ReportingId, {
  headers: new HttpHeaders().set('Authorization',""+Token)});
}

//שינוי מצב דיווח לפתור והוספת הערת סיבת סיום
async UpdateReporting(Token:string,ReportingToUpdate:Reporting): Promise<Observable<any>>
{
 return this.http.put<any>(this.endPointApi+"UpdateReporting",ReportingToUpdate, {
  headers: new HttpHeaders().set('Authorization',""+Token)});
}


//קבלת רשימת דיווחים לא פתורים
async GetAllReportingActive(Token:string): Promise<Observable<Array<Reporting>>>
{
 return this.http.get<Array<Reporting>>(this.endPointApi+"GetAllReportingActive", {
  headers: new HttpHeaders().set('Authorization',""+Token)});
}

//קבלת רשימת דיווחים פתורים
async GetAllReportingNoActive(Token:string): Promise<Observable<Array<Reporting>>>
{
 return this.http.get<Array<Reporting>>(this.endPointApi+"GetAllReportingNoActive", {
  headers: new HttpHeaders().set('Authorization',""+Token)});
}

//קבלת רשימת כל הדיווחים
async GetAllReporting(Token:string): Promise<Observable<Array<Reporting>>>
{
 return this.http.get<Array<Reporting>>(this.endPointApi+"GetAllReporting", {
  headers: new HttpHeaders().set('Authorization',""+Token)});
}

//קבלת רשימת דיווחים לפי מזהה מתכון
async GetAllReportingByRecipeId(Token:string,RecipeId:number): Promise<Observable<Array<Reporting>>>
{
 return this.http.get<Array<Reporting>>(this.endPointApi+"GetAllReportingByRecipeId/"+RecipeId, {
  headers: new HttpHeaders().set('Authorization',""+Token)});
}
}
