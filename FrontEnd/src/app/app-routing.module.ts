import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardHomeComponent } from './Screens/AdminFolder/AdminDashboardFolder/dashboard-home/dashboard-home.component';
import { AllUserHomeComponent } from './Screens/AdminFolder/AllUserFolder/all-user-home/all-user-home.component';
import { HomePageComponent } from './Screens/HomePageFolder/home-page/home-page.component';
import { ForgotPasswordComponent } from './Screens/LoginFolder/forgot-password/forgot-password.component';
import { LoginComponent } from './Screens/LoginFolder/login/login.component';
import { SignUpComponent } from './Screens/LoginFolder/sign-up/sign-up.component';
import { RecipeHomeComponent } from './Screens/RecipeFolder/recipe-home/recipe-home.component';
import { UserHomeComponent } from './Screens/UserFolder/user-home/user-home.component';
import { GeneralGuardService } from './Services/GeneralGuard.service';
import { AdminGuardService } from './Services/AdminGuard.service';

const routes: Routes = [
  {path:'Home',component:HomePageComponent},
  {path:'Login',component:LoginComponent},
  {path:'SignUp',component:SignUpComponent},
  {path:'ForgotPassword',component:ForgotPasswordComponent},
  {path:'UserHome',component:UserHomeComponent ,canActivate:[GeneralGuardService]},
  {path:'AllUserHome',component:AllUserHomeComponent ,canActivate:[AdminGuardService]},
  {path:'DashboardHome',component:DashboardHomeComponent ,canActivate:[AdminGuardService]},
  {path:'RecipeHome',component:RecipeHomeComponent},
 { path: '**', component:  HomePageComponent}
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
