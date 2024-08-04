import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ChangeUserRole } from '../Model/ChangeUserRole';
import { User } from '../Model/User';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  //תקציר
  ////////////
  //בנאי
  // JWT קבלת אוביקט משתמש על פי
  // JWT קבלת מזהה משתמש על פי
  // הוספת משתמש
  // קבלת משתמש לפי מזהה
  // קבלת כל המשתמשים מוגבל למהל
  // קבלת כל המנהלים מוגבל למנהל
  // קבלת רשימת משתמשים שהם לא מנהלים מוגבל למנהל
  // קבלת משתמש על פי מייל מוגבל למנהל
  // האם קיים מייל כזה
  // JWT עדכון משתמש על פי
  // JWT מחיקת משתמש על פי
  // מחיקת משתמש למנהל
  // עדכון תפקיד למשתמש מוגבל למנהל
  //שליחת סיסמה חדשה למיל של המשתשמש ושמירה במאגר הנותנים שלו
  //עדכון משתמש מחובר או לא
  //קבלת מספר כמה משתמשים נרשמו אם אפשרות סינון של היום השבוע החודש והשנה וכל הזמן

  endPointApi="https://localhost:44328/api/User/";
  //בנאי
constructor(private http:HttpClient) { }

// JWT קבלת אוביקט משתמש על פי
async GetUserByToken(Token:string): Promise<Observable<User>>
{
 return this.http.get<User>(this.endPointApi+"GetUserByToken", {
  headers: new HttpHeaders().set('Authorization',""+Token)});
}

  // JWT קבלת מזהה משתמש על פי
  async GetUserIdByToken(Token:string): Promise<Observable<number>>
  {
   return this.http.get<number>(this.endPointApi+"GetUserIdByToken", {
    headers: new HttpHeaders().set('Authorization',""+Token)});
  }

  // הוספת משתמש
  async AddUser(UserToAdd:User): Promise<Observable<string>>
  {
   return this.http.post<string>(this.endPointApi+"AddUser",UserToAdd);
  }

  // קבלת משתמש לפי מזהה
  async GetUserById(UserId:number): Promise<Observable<User>>
  {
   return this.http.get<User>(this.endPointApi+"GetUserById/"+UserId);
  }

  // קבלת כל המשתמשים מוגבל למהל
  async GetAllUser(Token:string): Promise<Observable<Array<User>>>
  {
   return await this.http.get<Array<User>>(this.endPointApi+"GetAllUser", {
    headers: new HttpHeaders().set('Authorization',""+Token)});
  }

  // קבלת כל המנהלים מוגבל למנהל
  async GetAllAdmin(Token:string): Promise<Observable<Array<User>>>
  {
   return await this.http.get<Array<User>>(this.endPointApi+"GetAllAdmin", {
    headers: new HttpHeaders().set('Authorization',""+Token)});
  }

  // קבלת רשימת משתמשים שהם לא מנהלים מוגבל למנהל
  async GetAllUserNoAdmin(Token:string): Promise<Observable<Array<User>>>
  {
   return this.http.get<Array<User>>(this.endPointApi+"GetAllUserNoAdmin", {
    headers: new HttpHeaders().set('Authorization',""+Token)});
  }

  // קבלת משתמש על פי מייל מוגבל למנהל
  async GetUserByMail(Token:string,Mail:string): Promise<Observable<User>>
  {
   return this.http.get<User>(this.endPointApi+"GetUserByMail/"+Mail, {
    headers: new HttpHeaders().set('Authorization',""+Token)});
  }

  // האם קיים מייל כזה
  async GetHaveUser(Mail?:string): Promise<Observable<boolean>>
  {
   return this.http.get<boolean>(this.endPointApi+"GetHaveUser/"+Mail);
  }

  // JWT עדכון משתמש על פי
  async UpdateUserByToken(Token:string,UserToUpdate:User): Promise<Observable<any>>
  {
   return this.http.put<any>(this.endPointApi+"UpdateUserByToken",UserToUpdate, {
    headers: new HttpHeaders().set('Authorization',""+Token)});
  }

  // JWT מחיקת משתמש על פי
  async DeleteUserByToken(Token:string): Promise<Observable<any>>
  {
   return this.http.delete<any>(this.endPointApi+"DeleteUserByToken", {
    headers: new HttpHeaders().set('Authorization',""+Token)});
  }

  // מחיקת משתמש למנהל
  async DeleteUserByIdForAdmin(Token:string,UserId?:number): Promise<Observable<any>>
  {
   return this.http.delete<any>(this.endPointApi+"DeleteUserByIdForAdmin/"+UserId, {
    headers: new HttpHeaders().set('Authorization',""+Token)});
  }

  // עדכון תפקיד למשתמש מוגבל למנהל
  async ChangeUserRoleForAdmin(Token:string,UserToUpdate:ChangeUserRole): Promise<Observable<any>>
  {
   return this.http.put<any>(this.endPointApi+"ChangeUserRoleForAdmin",UserToUpdate, {
    headers: new HttpHeaders().set('Authorization',""+Token)});
  }

  //שליחת סיסמה חדשה למיל של המשתשמש ושמירה במאגר הנותנים שלו
  async ForgotPassword(Mail:string): Promise<Observable<boolean>>
  {
   return this.http.get<boolean>(this.endPointApi+"ForgotPassword/"+Mail);
  }

  //עדכון משתמש מחובר או לא
  async UpdateIsActiveForUser(Token:string,Active:boolean): Promise<Observable<any>>
  {
   return this.http.put<any>(this.endPointApi+"UpdateIsActiveForUser/"+Active, {
    headers: new HttpHeaders().set('Authorization',""+Token)});
  }

  //קבלת מספר כמה משתמשים נרשמו אם אפשרות סינון של היום השבוע החודש והשנה וכל הזמן
  async GetCountRegisterDate(Token:string,RequestDate:string): Promise<Observable<Array<User>>>
  {
   return this.http.get<Array<User>>(this.endPointApi+"GetCountRegisterDate/"+RequestDate, {
    headers: new HttpHeaders().set('Authorization',""+Token)});
  }
}
