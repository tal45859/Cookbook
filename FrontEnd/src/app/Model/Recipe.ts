import { Subcategory } from "./Subcategory";
import { User } from "./User";

export class Recipe {
  public Id?:number;
  public UserId?:number;
  public SubcategoryId?:number;
  public RecipeName?:string;
  public Ingredients?:string;
  public PreparationMethod?:string;
  public PreparationTime?:number;
  public QuantityOfPortions?:number;
  public UploadDate?:Date;
  public CanBeExpected?:boolean;
  public NumberOfViews?:number;
  public NumberOfLikes?:number;
  public NumberOfNoLieks?:number;
  public User?:User;
  public Subcategory?:Subcategory;
}
