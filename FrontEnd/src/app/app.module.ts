import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ContentComponent } from './Layout/content/content.component';
import { FooterComponent } from './Layout/footer/footer.component';
import { HeaderComponent } from './Layout/header/header.component';
import { DashboardHomeComponent } from './Screens/AdminFolder/AdminDashboardFolder/dashboard-home/dashboard-home.component';
import { HomePageComponent } from './Screens/HomePageFolder/home-page/home-page.component';
import { LoginComponent } from './Screens/LoginFolder/login/login.component';
import { SignUpComponent } from './Screens/LoginFolder/sign-up/sign-up.component';
import { ForgotPasswordComponent } from './Screens/LoginFolder/forgot-password/forgot-password.component';
import { UserHomeComponent } from './Screens/UserFolder/user-home/user-home.component';
import { UserUpdateComponent } from './Screens/UserFolder/user-update/user-update.component';
import { UserDetailsComponent } from './Screens/UserFolder/user-details/user-details.component';
import { AllUserHomeComponent } from './Screens/AdminFolder/AllUserFolder/all-user-home/all-user-home.component';
import { AllUserDetailsComponent } from './Screens/AdminFolder/AllUserFolder/all-user-details/all-user-details.component';
import { AllNewUserComponent } from './Screens/AdminFolder/AllUserFolder/all-new-user/all-new-user.component';
import { AllLoginHistoryComponent } from './Screens/AdminFolder/AllUserFolder/all-login-history/all-login-history.component';
import { CategoryDashboardComponent } from './Screens/AdminFolder/AdminDashboardFolder/CategoryFolder/category-dashboard/category-dashboard.component';
import { SubcategoryDashboardComponent } from './Screens/AdminFolder/AdminDashboardFolder/SubcategoryFolder/subcategory-dashboard/subcategory-dashboard.component';
import { ReportingDashboardComponent } from './Screens/AdminFolder/AdminDashboardFolder/reporting-dashboard/reporting-dashboard.component';
import { NgxPaginationModule } from 'ngx-pagination';
import { AddCategoryComponent } from './Screens/AdminFolder/AdminDashboardFolder/CategoryFolder/add-category/add-category.component';
import { AllCategoryComponent } from './Screens/AdminFolder/AdminDashboardFolder/CategoryFolder/all-category/all-category.component';
import { AllSubCategoryComponent } from './Screens/AdminFolder/AdminDashboardFolder/SubcategoryFolder/all-sub-category/all-sub-category.component';
import { AddSubCategoryComponent } from './Screens/AdminFolder/AdminDashboardFolder/SubcategoryFolder/add-sub-category/add-sub-category.component';
import { RecipeHomeComponent } from './Screens/RecipeFolder/recipe-home/recipe-home.component';
import { AllRecipeComponent } from './Screens/RecipeFolder/all-recipe/all-recipe.component';
import { UserRecipeComponent } from './Screens/RecipeFolder/user-recipe/user-recipe.component';
import { FavoriteRecipeComponent } from './Screens/RecipeFolder/favorite-recipe/favorite-recipe.component';
import { RecipeDetailsComponent } from './Screens/RecipeFolder/recipe-details/recipe-details.component';
import { CommentHomeComponent } from './Screens/CommentFolder/comment-home/comment-home.component';
import { AllCommentComponent } from './Screens/CommentFolder/all-comment/all-comment.component';
import { AddCommentComponent } from './Screens/CommentFolder/add-comment/add-comment.component';
import { AddReportingComponent } from './Screens/ReportingFolder/add-reporting/add-reporting.component';
import { AddRecipeHomeComponent } from './Screens/RecipeFolder/AddRecipeFolder/add-recipe-home/add-recipe-home.component';
import { AddRecipeComponent } from './Screens/RecipeFolder/AddRecipeFolder/add-recipe/add-recipe.component';
import { AddImageRecipeComponent } from './Screens/RecipeFolder/AddRecipeFolder/add-image-recipe/add-image-recipe.component';
import { UpdateRecipeHomeComponent } from './Screens/RecipeFolder/UpdateRecipeFolder/update-recipe-home/update-recipe-home.component';
import { UpdateRecipeComponent } from './Screens/RecipeFolder/UpdateRecipeFolder/update-recipe/update-recipe.component';
import { UpdateImageComponent } from './Screens/RecipeFolder/UpdateRecipeFolder/update-image/update-image.component';


@NgModule({
  declarations: [
    AppComponent,
    ContentComponent,
    FooterComponent,
    HeaderComponent,
    DashboardHomeComponent,
    HomePageComponent,
    LoginComponent,
    SignUpComponent,
    ForgotPasswordComponent,
    UserHomeComponent,
    UserUpdateComponent,
    UserDetailsComponent,
    AllUserHomeComponent,
    AllUserDetailsComponent,
    AllNewUserComponent,
    AllLoginHistoryComponent,
    CategoryDashboardComponent,
    SubcategoryDashboardComponent,
    ReportingDashboardComponent,
    AddCategoryComponent,
    AllCategoryComponent,
    AllSubCategoryComponent,
    AddSubCategoryComponent,
    RecipeHomeComponent,
    AllRecipeComponent,
    UserRecipeComponent,
    FavoriteRecipeComponent,
    RecipeDetailsComponent,
    CommentHomeComponent,
    AllCommentComponent,
    AddCommentComponent,
    AddReportingComponent,
    AddRecipeHomeComponent,
    AddRecipeComponent,
    AddImageRecipeComponent,
    UpdateRecipeHomeComponent,
    UpdateRecipeComponent,
    UpdateImageComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    NgxPaginationModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
