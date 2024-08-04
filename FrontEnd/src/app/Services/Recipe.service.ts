import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Recipe } from '../Model/Recipe';

@Injectable({
  providedIn: 'root'
})
export class RecipeService {
  //תקציר
  /////////
  //בנאי
  //JWT הוספת מתכון על פי
  //הוספת צפיה למתכון לפי מזהה מתכון
  //הוספת אהבתי או לא אהבתי למתכון לפי מזהה מתכון
  //מחיקת מתכון למשתמש ומנהל
  //עדכון מתכון למי שייצר אותו
  //שינוי מצב האם ניתן לצפות במתכון לפי מזהה מתכון //צריך לעדכן גם את המועדפים שימחק משם
  //JWT קבלת מזהה מתכון אחרון על פי
  //קבלת מתכון לפי מזהה מתכון עם אוביקטים למנהל
  //קבלת מתכון שניתן לצפות בו לפי מזהה מתכון עם אוביקטים
  //JWT קבלת מתכון לפי מזהה מתכון עם אוביקטים למשתמש על פי
  //קבלת רשימת כל המתכונים כולל אוביקטים למנהל
  //קבלת רשימת כל המתכונים שמותר לצפות בהם עם אוביקטים
  // JWT קבלת רשימת כל המתכונים של משתמש למשתמש לפי
  //קבלת רשימת כל המתכונים שאפשר לצפות בהם של משתמש מסוים לפי מזהה משתמש
  //קבלת רשימת כל המתכונים שניתן לצפות בהם לפי מזהה תת קטגוריה
  //קבלת רשימת כל המתכונים שניתן לצפות בהם לפי מזהה קטגוריה
  //קבלת רשימת המתכונים בצורה ממוינת מי הנצפים ביותר שניתן לצפות בהם
  //JWT קבלת רשימת המתכונים בצורה ממוינת מי הנצפים ביותר של משתמש על פי
  //קבלת רשימת מתכונים בצורה ממוינת מי האהובים ביותר שניתן לצפות בהם
  //JWT קבלת רשימת מתכונים בצורה ממוינת מי האהובים ביותר של משתמש על פי
  //קבלת רשימת מתכונים בצורה ממוינת מי הכי לא אהובים שניתן לצפות בהם
  //JWT קבלת רשימת מתכונים בצורה ממוינת מי הכי לא אהובים של משתמש על פי
  //קבלת רשימת מתכונים בצורה ממוינת מי החדשים ביותר שניתן לצפות בהם
  //JWT קבלת רשימת מתכונים בצורה ממוינת מי החדשים ביותר של משתמש על פי

  endPointApi="https://localhost:44328/api/Recipe/";

  //בנאי
  constructor(private http:HttpClient) { }

  //JWT הוספת מתכון על פי
  async AddRecipe(Token:string,RecipeToAdd:Recipe): Promise<Observable<boolean>>
  {
   return this.http.post<boolean>(this.endPointApi+"AddRecipe",RecipeToAdd, {
    headers: new HttpHeaders().set('Authorization',""+Token)});
  }

  //הוספת צפיה למתכון לפי מזהה מתכון
  async AddViewsToRecipe(RecipeId?:number): Promise<Observable<any>>
  {
   return this.http.get<any>(this.endPointApi+"AddViewsToRecipe/"+RecipeId);
  }

  //הוספת אהבתי או לא אהבתי למתכון לפי מזהה מתכון
  async AddLikeOrNoLikeToRecipe(RecipeId?:number,likeOrNoFroUser?:boolean): Promise<Observable<any>>
  {
   return this.http.put<any>(this.endPointApi+"AddLikeOrNoLikeToRecipe/"+RecipeId,likeOrNoFroUser);
  }

  //מחיקת מתכון למשתמש ומנהל
  async DeleteRecipe(Token:string,RecipeId?:number): Promise<Observable<any>>
  {
   return this.http.delete<any>(this.endPointApi+"DeleteRecipe/"+RecipeId, {
    headers: new HttpHeaders().set('Authorization',""+Token)});
  }

  //עדכון מתכון למי שייצר אותו
  async UpdateRecipe(Token:string,RecipeToUpdate:Recipe): Promise<Observable<any>>
  {
   return this.http.put<any>(this.endPointApi+"UpdateRecipe",RecipeToUpdate, {
    headers: new HttpHeaders().set('Authorization',""+Token)});
  }

  //שינוי מצב האם ניתן לצפות במתכון לפי מזהה מתכון //צריך לעדכן גם את המועדפים שימחק משם
  async UpdateCanBeExpectedForUser(Token:string,RecipeId:number,CanShow:boolean): Promise<Observable<any>>
  {
   return this.http.put<any>(this.endPointApi+"UpdateCanBeExpectedForUser/"+RecipeId,CanShow, {
    headers: new HttpHeaders().set('Authorization',""+Token)});
  }

  //JWT קבלת מזהה מתכון אחרון על פי
  async GetLastRecipeIdByJWTForUser(Token:string): Promise<Observable<number>>
  {
    return this.http.get<number>(this.endPointApi+"GetLastRecipeIdByJWTForUser", {
      headers: new HttpHeaders().set('Authorization',""+Token)});
  }

  //קבלת מתכון לפי מזהה מתכון עם אוביקטים למנהל
  async GetRecipeByIdForAdmin(Token:string,RecipeId:number): Promise<Observable<Recipe>>
  {
   return this.http.get<Recipe>(this.endPointApi+"GetRecipeByIdForAdmin/"+RecipeId, {
    headers: new HttpHeaders().set('Authorization',""+Token)});
  }

  //קבלת מתכון שניתן לצפות בו לפי מזהה מתכון עם אוביקטים
  async GetRecipeCanBeExpectedById(RecipeId:number): Promise<Observable<Recipe>>
  {
   return this.http.get<Recipe>(this.endPointApi+"GetRecipeCanBeExpectedById/"+RecipeId);
  }

  //JWT קבלת מתכון לפי מזהה מתכון עם אוביקטים למשתמש על פי
  async GetRecipeByIdAndJWTForUser(Token:string,RecipeId:number): Promise<Observable<Recipe>>
  {
   return this.http.get<Recipe>(this.endPointApi+"GetRecipeByIdAndJWTForUser/"+RecipeId, {
    headers: new HttpHeaders().set('Authorization',""+Token)});
  }

  //קבלת רשימת כל המתכונים כולל אוביקטים למנהל
  async GetAllRecipeForAdmin(Token:string): Promise<Observable<Array<Recipe>>>
  {
   return this.http.get<Array<Recipe>>(this.endPointApi+"GetAllRecipeForAdmin", {
    headers: new HttpHeaders().set('Authorization',""+Token)});
  }

  //קבלת רשימת כל המתכונים שמותר לצפות בהם עם אוביקטים
  async GetAllRecipeCanBeExpected(): Promise<Observable<Array<Recipe>>>
  {
   return this.http.get<Array<Recipe>>(this.endPointApi+"GetAllRecipeCanBeExpected");
  }

  // JWT קבלת רשימת כל המתכונים של משתמש למשתמש לפי
  async GetAllRecipeJWTForUser(Token:string): Promise<Observable<Array<Recipe>>>
  {
   return this.http.get<Array<Recipe>>(this.endPointApi+"GetAllRecipeJWTForUser", {
    headers: new HttpHeaders().set('Authorization',""+Token)});
  }

  //קבלת רשימת כל המתכונים שאפשר לצפות בהם של משתמש מסוים לפי מזהה משתמש
  async GetAllRecipeCanBeExpectedByUserId(UserId:number): Promise<Observable<Array<Recipe>>>
  {
   return this.http.get<Array<Recipe>>(this.endPointApi+"GetAllRecipeCanBeExpectedByUserId/"+UserId);
  }

  //קבלת רשימת כל המתכונים שניתן לצפות בהם לפי מזהה תת קטגוריה
  async GetAllRecipeCanBeExpectedBySubcategoryId(SubcategoryId?:number): Promise<Observable<Array<Recipe>>>
  {
   return this.http.get<Array<Recipe>>(this.endPointApi+"GetAllRecipeCanBeExpectedBySubcategoryId/"+SubcategoryId);
  }

  //קבלת רשימת כל המתכונים שניתן לצפות בהם לפי מזהה קטגוריה
  async GetAllRecipeCanBeExpectedByCategoryId(CategoryId?:number): Promise<Observable<Array<Recipe>>>
  {
   return this.http.get<Array<Recipe>>(this.endPointApi+"GetAllRecipeCanBeExpectedByCategoryId/"+CategoryId);
  }

  //קבלת רשימת המתכונים בצורה ממוינת מי הנצפים ביותר שניתן לצפות בהם
  async GetAllRecipeCanBeExpectedByNumberOfViews(): Promise<Observable<Array<Recipe>>>
  {
   return this.http.get<Array<Recipe>>(this.endPointApi+"GetAllRecipeCanBeExpectedByNumberOfViews");
  }

  //JWT קבלת רשימת המתכונים בצורה ממוינת מי הנצפים ביותר של משתמש על פי
  async GetAllRecipeByJWTAndByNumberOfViewsForUser(Token:string): Promise<Observable<Array<Recipe>>>
  {
   return this.http.get<Array<Recipe>>(this.endPointApi+"GetAllRecipeByJWTAndByNumberOfViewsForUser", {
    headers: new HttpHeaders().set('Authorization',""+Token)});
  }

  //קבלת רשימת מתכונים בצורה ממוינת מי האהובים ביותר שניתן לצפות בהם
  async GetAllRecipeCanBeExpectedByNumberOfLikes(): Promise<Observable<Array<Recipe>>>
  {
   return this.http.get<Array<Recipe>>(this.endPointApi+"GetAllRecipeCanBeExpectedByNumberOfLikes");
  }

  //JWT קבלת רשימת מתכונים בצורה ממוינת מי האהובים ביותר של משתמש על פי
  async GetAllRecipeByJWTAndByNumberOfLikesForUser(Token:string): Promise<Observable<Array<Recipe>>>
  {
   return this.http.get<Array<Recipe>>(this.endPointApi+"GetAllRecipeByJWTAndByNumberOfLikesForUser", {
    headers: new HttpHeaders().set('Authorization',""+Token)});
  }

  //קבלת רשימת מתכונים בצורה ממוינת מי הכי לא אהובים שניתן לצפות בהם
  async GetAllRecipeCanBeExpectedByNumberOfNoLieks(): Promise<Observable<Array<Recipe>>>
  {
   return this.http.get<Array<Recipe>>(this.endPointApi+"GetAllRecipeCanBeExpectedByNumberOfNoLieks");
  }

  //JWT קבלת רשימת מתכונים בצורה ממוינת מי הכי לא אהובים של משתמש על פי
  async GetAllRecipeByJWTAndByNumberOfNoLieksForUser(Token:string): Promise<Observable<Array<Recipe>>>
  {
   return this.http.get<Array<Recipe>>(this.endPointApi+"GetAllRecipeByJWTAndByNumberOfNoLieksForUser", {
    headers: new HttpHeaders().set('Authorization',""+Token)});
  }

  //קבלת רשימת מתכונים בצורה ממוינת מי החדשים ביותר שניתן לצפות בהם
  async GetAllRecipeCanBeExpectedByUploadDate(): Promise<Observable<Array<Recipe>>>
  {
   return this.http.get<Array<Recipe>>(this.endPointApi+"GetAllRecipeCanBeExpectedByUploadDate");
  }

  //JWT קבלת רשימת מתכונים בצורה ממוינת מי החדשים ביותר של משתמש על פי
  async GetAllRecipeByJWTAndByUploadDateForUser(Token:string): Promise<Observable<Array<Recipe>>>
  {
   return this.http.get<Array<Recipe>>(this.endPointApi+"GetAllRecipeByJWTAndByUploadDateForUser", {
    headers: new HttpHeaders().set('Authorization',""+Token)});
  }
}
