import { Comment } from './../Model/Comment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CommentService {
//תקציר
/////////
//בנאי
//קבלת תגובה לפי מזהה תגובה
//הוספת תגובה
//מחיקת תגובה רק למנהל
//קבלת רשימת תגובות לפי מזהה מתכון
//קבלת כל התגובות למנהל

  endPointApi="https://localhost:44328/api/Comment/";

  //בנאי
  constructor(private http:HttpClient) { }

//קבלת תגובה לפי מזהה תגובה
async GetCommentById(Token:string,CommentId:number): Promise<Observable<Comment>>
{
 return this.http.get<Comment>(this.endPointApi+"GetCommentById/"+CommentId);
}

//הוספת תגובה
async AddComment(CommentToAdd:Comment): Promise<Observable<boolean>>
{
 return this.http.post<boolean>(this.endPointApi+"AddComment",CommentToAdd);
}

//מחיקת תגובה רק למנהל
async DeleteCommentByID(Token:string,CommentId?:number): Promise<Observable<any>>
{
 return this.http.delete<any>(this.endPointApi+"DeleteCommentByID/"+CommentId, {
  headers: new HttpHeaders().set('Authorization',""+Token)});
}

//קבלת רשימת תגובות לפי מזהה מתכון
async GetAllCommentByRecipeId(RecipeId?:number): Promise<Observable<Array<Comment>>>
{
 return this.http.get<Array<Comment>>(this.endPointApi+"GetAllCommentByRecipeId/"+RecipeId);
}

//קבלת כל התגובות למנהל
async GetAllCommentForAdmin(Token:string): Promise<Observable<Array<Comment>>>
{
 return this.http.get<Array<Comment>>(this.endPointApi+"GetAllCommentForAdmin", {
  headers: new HttpHeaders().set('Authorization',""+Token)});
}
}
