// export class NavBarItems {
// }

import { NavBarMenultem } from "../Model/NavBarMenultem";

export const Defult:Array<NavBarMenultem>=[
  {
    name:'דף הבית',
    url:'Home'
  },
  {
    name:"דף מתכונים",
    url:"RecipeHome"
  },
  {
    name:'התחברות',
    url:'Login'
  },
  {
    name:'הרשמה',
    url:'SignUp'
  }
];
export const Classic:Array<NavBarMenultem>=[
  {
    name:'דף הבית',
    url:'Home'
  },
  {
    name:"דף מתכונים",
    url:"RecipeHome"
  },
  {
    name:'פרטים אישים',
    url:'UserHome'
  }
];
export const Admin:Array<NavBarMenultem>=[
  {
    name:'דף הבית',
    url:'Home'
  },
  {
    name:"דף מתכונים",
    url:"RecipeHome"
  },
  {
    name:'פרטים אישים',
    url:'UserHome'
  },
  {
    name:'ניהול משתמשים',
    url:'AllUserHome'
  },
  {
    name:'ניהול האתר',
    url:'DashboardHome'
  }
];
