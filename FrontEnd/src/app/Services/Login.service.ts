import { User } from './../Model/User';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { tap } from 'rxjs';
import { Observable } from 'rxjs/internal/Observable';

import { Login } from '../Model/Login';


@Injectable({
  providedIn: 'root'
})
export class LoginService {
  //תקציר
  ////////
  //בנאי
  //הזדאות וקבלת תוקן
  //תוקן
  //רוול

endPointApi="https://localhost:44328/api/User/auth";

//בנאי
constructor(private http:HttpClient) { }

//הזדאות וקבלת תוקן
async GetAtuh(auth:Login): Promise<Observable<string>>
{
  return this.http.post(this.endPointApi,auth,{responseType: 'text' });
}

//הזדאות וקבלת תוקן
async getUser(Token:string): Promise<Observable<User>>
{
 return this.http.get<User>("https://localhost:44328/api/User/GetUserByToken", {
  headers: new HttpHeaders().set('Authorization',""+Token)});
}

 //תוקן
  get Token():string
  {
    return String(window.sessionStorage.getItem("Token"));
  }
  set Token(val:string)
  {
    window.sessionStorage.setItem("Token",val);
  }

  //רוול
  get Role():string
  {
    return String(window.sessionStorage.getItem("Role"));
  }
  set Role(val:string)
  {
    window.sessionStorage.setItem("Role",val);
  }

  //נאב
  get Navbar():string
  {
    return String(window.sessionStorage.getItem("Navbar"));
  }
  set Navbar(val:string)
  {
    window.sessionStorage.setItem("Navbar",val);
  }
}
