import { LoginService } from './../../../Services/Login.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-recipe-home',
  templateUrl: './recipe-home.component.html',
  styleUrls: ['./recipe-home.component.css']
})
export class RecipeHomeComponent implements OnInit {
  public OpenChildren:number=1;
  public HaveToken:boolean=false;
  constructor(private HttpLogin:LoginService) { }

  async ngOnInit(): Promise<void> {
   this.HaveToken=this.HttpLogin.Token=='null'||this.HttpLogin.Token==null?false:true;
  }

}

//דף בית מתכונים
//בתוכו
//כל המתכונים
//המתכונים שלי
//המועדפים שלי

//דף כל המתכונים
//בתוכו
//כל המתכונים
//אפשרות סינון באמצעות קומפוננטת בן שתביא כל פעם אוביקט מסונן או מספר בחירה
//דף פרטי מתכון ברגע שלחצו על מתכון
//אפשרות מחיקה למנהל והוספה למועדפים למי שיש תוקן


//דף המתכונים שלי
//בתוכו
//כל המתוכנים
//הוסף מתכון למי שיש טוקן
//אפשרות מחיקה ועדכון

//דף המועדפים שלי
//בתוכו
//כל המועדפים
//אם אפשרות מחיקת מועדף


