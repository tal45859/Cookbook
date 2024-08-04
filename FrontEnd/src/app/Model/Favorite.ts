import { Recipe } from "./Recipe";

export class Favorite {
  public Id?:number;
  public UserId?:number;
  public RecipeId?:number;
  public DateAdded?:Date;
  public Recipe?:Recipe;
}
