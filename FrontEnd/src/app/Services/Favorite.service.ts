import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Favorite } from '../Model/Favorite';

@Injectable({
  providedIn: 'root'
})
export class FavoriteService {
//תקציר
//////////
//בנאי
//הוספת מועדף
//מחיקת מועדף
//JWT קבלת מועדף לפי מזהה מועדף ולפי
//JWT קבלת רשימת מועדפים לפי

  endPointApi="https://localhost:44328/api/Favorite/";

  //בנאי
  constructor(private http:HttpClient) { }

  //הוספת מועדף
  async AddFavorite(Token:string,FavoriteToAdd:Favorite): Promise<Observable<boolean>>
  {
   return this.http.post<boolean>(this.endPointApi+"AddFavorite",FavoriteToAdd, {
    headers: new HttpHeaders().set('Authorization',""+Token)});
  }

//מחיקת מועדף
async DeleteFavoriteById(Token:string,FavoriteId?:number): Promise<Observable<any>>
{
 return this.http.delete<any>(this.endPointApi+"DeleteFavoriteById/"+FavoriteId, {
  headers: new HttpHeaders().set('Authorization',""+Token)});
}

//JWT קבלת מועדף לפי מזהה מועדף ולפי
async GetFavoriteByIdAndJwt(Token:string,FavoriteId:number): Promise<Observable<Favorite>>
{
 return this.http.get<Favorite>(this.endPointApi+"GetFavoriteByIdAndJwt/"+FavoriteId, {
  headers: new HttpHeaders().set('Authorization',""+Token)});
}

//JWT קבלת רשימת מועדפים לפי
async GetAllFavoriteJwt(Token:string): Promise<Observable<Array<Favorite>>>
{
 return this.http.get<Array<Favorite>>(this.endPointApi+"GetAllFavoriteJwt", {
  headers: new HttpHeaders().set('Authorization',""+Token)});
}
}
