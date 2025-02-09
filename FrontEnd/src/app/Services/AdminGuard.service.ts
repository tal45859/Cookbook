import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { LoginService } from './Login.service';

@Injectable({
  providedIn: 'root'
})
export class AdminGuardService  {
  public Token:string;
  public Role:string;
constructor(private HttpLogin:LoginService)
 {
  this.Token=this.HttpLogin.Token;
  this.Role=this.HttpLogin.Role;
 }
 //תקציר
 ///////
 //שומר שרק מנהל יוכל להיכנס

 //שומר שרק מנהל יוכל להיכנס
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {

    return this.Token!=null&&this.Role=="Admin";
  }
}
