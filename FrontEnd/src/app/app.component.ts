import { Component } from '@angular/core';
import { NavBarMenultem } from './Model/NavBarMenultem';
import { Admin, Classic, Defult } from './NavBarData/NavBarItems';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'CookBookFronted';
  public RoleForNav = sessionStorage.getItem("Navbar");

  DefultNavBar:Array<NavBarMenultem>=Defult;
  ClassicNavBar:Array<NavBarMenultem>=Classic
  AdminNavBar:Array<NavBarMenultem>=Admin;
}
