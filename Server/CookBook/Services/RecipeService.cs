using CookBook.Data;
using CookBook.Data.DTO;
using CookBook.Data.Entities;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CookBook.Services
{
    public class RecipeService
    {
        //תקציר
        /////////
        //בנאי
        //קבלת מתכון לפי מזהה מתכון נקי מאוביקטים לשימוש פנימי
        //JWT הוספת מתכון על פי
        //הוספת צפיה למתכון לפי מזהה מתכון
        //הוספת אהבתי או לא אהבתי למתכון לפי מזהה מתכון
        //מחיקת מתכון למשתמש ומנהל
        //עדכון מתכון למי שייצר אותו
        //שינוי מצב האם ניתן לצפות במתכון לפי מזהה מתכון //צריך לעדכן גם את המועדפים שימחק משם
        //JWT קבלת מזהה מתכון אחרון על פי
        //קבלת מתכון לפי מזהה מתכון עם אוביקטים למנהל
        //קבלת מתכון שניתן לצפות בו לפי מזהה מתכון עם אוביקטים
        //JWT קבלת מתכון לפי מזהה מתכון עם אוביקטים למשתמש על פי
        //קבלת רשימת כל המתכונים כולל אוביקטים למנהל
        //קבלת רשימת כל המתכונים שמותר לצפות בהם עם אוביקטים
        // JWT קבלת רשימת כל המתכונים של משתמש למשתמש לפי 
        //קבלת רשימת כל המתכונים שאפשר לצפות בהם של משתמש מסוים לפי מזהה משתמש
        //קבלת רשימת כל המתכונים שניתן לצפות בהם לפי מזהה תת קטגוריה
        //קבלת רשימת כל המתכונים שניתן לצפות בהם לפי מזהה קטגוריה
        //קבלת רשימת המתכונים בצורה ממוינת מי הנצפים ביותר שניתן לצפות בהם
        //JWT קבלת רשימת המתכונים בצורה ממוינת מי הנצפים ביותר של משתמש על פי
        //קבלת רשימת מתכונים בצורה ממוינת מי האהובים ביותר שניתן לצפות בהם
        //JWT קבלת רשימת מתכונים בצורה ממוינת מי האהובים ביותר של משתמש על פי
        //קבלת רשימת מתכונים בצורה ממוינת מי הכי לא אהובים שניתן לצפות בהם
        //JWT קבלת רשימת מתכונים בצורה ממוינת מי הכי לא אהובים של משתמש על פי
        //קבלת רשימת מתכונים בצורה ממוינת מי החדשים ביותר שניתן לצפות בהם
        //JWT קבלת רשימת מתכונים בצורה ממוינת מי החדשים ביותר של משתמש על פי


        //לעשות ולחשוב איך והאם כדאי פה או באנגולר
        //מסנן לפי טקסט חופשי שיחפש לפי כמות האותיות והסדר שלהם ויחזיר רשימה של מהכ רלונטים ועד הפחות


        //חשוב
        //לחשוב אם צריך להסתיר גם את המיל

        private readonly CookBookDBContext m_db;
        private readonly UserService _UserService;

        //בנאי
        public RecipeService(CookBookDBContext db, UserService userService)
        {
            m_db = db;
            _UserService = userService;
        }

        //קבלת מתכון לפי מזהה מתכון נקי מאוביקטים לשימוש פנימי
        public Recipe GetRecipeByIdFromDB(int RecipeId)
        {
            return m_db.Recipe.Where(r => r.Id == RecipeId).FirstOrDefault();
        }

        //JWT הוספת מתכון על פי
        public bool AddRecipe(RecipeDTO RecipeToAddFromUser)
        {
            Recipe RecipeToAdd = new Recipe();
            RecipeToAdd.UserId = _UserService.GetUserIdByJWT();
            RecipeToAdd.SubcategoryId= RecipeToAddFromUser.SubcategoryId;
            RecipeToAdd.RecipeName = RecipeToAddFromUser.RecipeName;
            RecipeToAdd.Ingredients = RecipeToAddFromUser.Ingredients;
            RecipeToAdd.PreparationMethod = RecipeToAddFromUser.PreparationMethod;
            RecipeToAdd.PreparationTime = RecipeToAddFromUser.PreparationTime;
            RecipeToAdd.QuantityOfPortions = RecipeToAddFromUser.QuantityOfPortions;
            RecipeToAdd.UploadDate = System.DateTime.Now;
            RecipeToAdd.CanBeExpected = RecipeToAddFromUser.CanBeExpected;
            RecipeToAdd.NumberOfViews = 0;
            RecipeToAdd.NumberOfLikes = 0;
            RecipeToAdd.NumberOfNoLieks = 0;
            m_db.Recipe.Add(RecipeToAdd);
            int c = m_db.SaveChanges();
            return c > 0;
        }

        //הוספת צפיה למתכון לפי מזהה מתכון
        public ResponseDTO AddViewsToRecipe(int RecipeId)
        {
            Recipe RecipeToAddView = GetRecipeByIdFromDB(RecipeId);
            if (RecipeToAddView == null || RecipeToAddView.Id != RecipeId)
            {
                return new ResponseDTO() { Status = Data.DTO.StatusCode.Error, StatusText = "המתכון לא נמצא בבסיס נתונים" };
            }
            RecipeToAddView.NumberOfViews = RecipeToAddView.NumberOfViews + 1;
            int c = m_db.SaveChanges();
            return c > 0 ?
                  new ResponseDTO() { Status = Data.DTO.StatusCode.Success }
                  :
                  new ResponseDTO() { Status = Data.DTO.StatusCode.Error, StatusText = "לא הצלחנו לשמור את השינוים" };
        }

        //הוספת אהבתי או לא אהבתי למתכון לפי מזהה מתכון
        public ResponseDTO AddLikeOrNoLikeToRecipe(int RecipeId , bool likeOrNoFroUser)
        {
            Recipe RecipeToAddLikeOrNoLike = GetRecipeByIdFromDB(RecipeId);
            if (RecipeToAddLikeOrNoLike == null || RecipeToAddLikeOrNoLike.Id != RecipeId)
            {
                return new ResponseDTO() { Status = Data.DTO.StatusCode.Error, StatusText = "המתכון לא נמצא בבסיס נתונים" };
            }            
            if(likeOrNoFroUser)
            {
                RecipeToAddLikeOrNoLike.NumberOfLikes = RecipeToAddLikeOrNoLike.NumberOfLikes + 1;
            }
            else
            {
                RecipeToAddLikeOrNoLike.NumberOfNoLieks = RecipeToAddLikeOrNoLike.NumberOfNoLieks + 1;
            }
            int c = m_db.SaveChanges();
            return c > 0 ?
                  new ResponseDTO() { Status = Data.DTO.StatusCode.Success }
                  :
                  new ResponseDTO() { Status = Data.DTO.StatusCode.Error, StatusText = "לא הצלחנו לשמור את השינוים" };
        }

        
        //מחיקת מתכון למשתמש ומנהל
        //לחפש את כל התמונות של המתכון ולמחוק אותם מהתיקיה
        public ResponseDTO DeleteRecipe(int RecipeId)
        {
            User User = _UserService.GetUserByJWT();
            Recipe RecipeToDelete= GetRecipeByIdFromDB(RecipeId);
            List<Image> LImages = m_db.Image.Where(i => i.RecipeId == RecipeId).ToList() ;
            if (User.Role == "Classic" && RecipeToDelete.UserId!=User.Id)
            {
                return new ResponseDTO() { Status = Data.DTO.StatusCode.Error, StatusText = "לא מורשה" };
            }
            if(RecipeToDelete == null|| RecipeToDelete.Id!= RecipeId)
            {
                return new ResponseDTO() { Status = Data.DTO.StatusCode.Error, StatusText = "המתכון לא נמצא בבסיס נתונים" };
            }
            if(LImages.Count>0)
            {
                for(int i=0;i<LImages.Count;i++)
                {
                    string url = "https://localhost:44328/";
                    string UrlToDelete = LImages[i].Url.Substring(url.Length, LImages[i].Url.Length - url.Length);
                    var PathToDelete = Path.Combine(Directory.GetCurrentDirectory(), UrlToDelete);
                    FileInfo file = new FileInfo(PathToDelete);
                    try
                    {
                        file.Delete();
                        m_db.SaveChanges();
                    }
                    catch
                    {
                        return new ResponseDTO() { Status = Data.DTO.StatusCode.Error, StatusText = "לא הצלחנו למחוק את התמונה" };
                    }
                }
            }

            m_db.Recipe.Remove(RecipeToDelete);
            int c = m_db.SaveChanges();
            return c > 0 ?
                 new ResponseDTO() { Status = Data.DTO.StatusCode.Success }
                 :
                 new ResponseDTO() { Status = Data.DTO.StatusCode.Error, StatusText = "לא הצלחנו למחוק את המתכון" };
        }

        //עדכון מתכון למי שייצר אותו
        public ResponseDTO UpdateRecipe(RecipeDTO RecipeToUpdateFromUser)
        {
            Recipe RecipeToUpdate = GetRecipeByIdFromDB(RecipeToUpdateFromUser.Id);
            if(RecipeToUpdate==null| RecipeToUpdate.Id!= RecipeToUpdateFromUser.Id)
            {
                return new ResponseDTO() { Status = Data.DTO.StatusCode.Error, StatusText = "המתכון לא נמצא בבסיס נתונים" };
            }
            //RecipeToUpdate.SubcategoryId = RecipeToUpdateFromUser.SubcategoryId;
            RecipeToUpdate.RecipeName = RecipeToUpdateFromUser.RecipeName;
            RecipeToUpdate.Ingredients = RecipeToUpdateFromUser.Ingredients;
            RecipeToUpdate.PreparationMethod = RecipeToUpdateFromUser.PreparationMethod;
            RecipeToUpdate.PreparationTime = RecipeToUpdateFromUser.PreparationTime;
            RecipeToUpdate.QuantityOfPortions = RecipeToUpdateFromUser.QuantityOfPortions;
            RecipeToUpdate.CanBeExpected = RecipeToUpdateFromUser.CanBeExpected;
            int c = m_db.SaveChanges();
            return c > 0 ?
              new ResponseDTO() { Status = Data.DTO.StatusCode.Success }
              :
              new ResponseDTO() { Status = Data.DTO.StatusCode.Error, StatusText = "לא הצלחנו לשמור את השינוים" };
        }

        /// <summary>
        /// לבדוק את המחיקה טוב שהמעדפים עפים
        /// </summary>
        /// <param name="RecipeId"></param>
        /// <param name="CanShow"></param>
        /// <returns></returns>
        //שינוי מצב האם ניתן לצפות במתכון לפי מזהה מתכון //צריך לעדכן גם את המועדפים שימחק משם
        public ResponseDTO UpdateCanBeExpectedForUser(int RecipeId ,bool CanShow)
        {
            Recipe RecipeToUpdateCanBeExpected = GetRecipeByIdFromDB(RecipeId);
            if (RecipeToUpdateCanBeExpected == null | RecipeToUpdateCanBeExpected.Id != RecipeId)
            {
                return new ResponseDTO() { Status = Data.DTO.StatusCode.Error, StatusText = "המתכון לא נמצא בבסיס נתונים" };
            }
            if(CanShow == false)
            {
                //מחיקת המועדפים
                List<Favorite> LFavorite = m_db.Favorite.Where(f => f.RecipeId == RecipeToUpdateCanBeExpected.Id).ToList();
                m_db.Favorite.RemoveRange(LFavorite);
                m_db.SaveChanges();
            }
            RecipeToUpdateCanBeExpected.CanBeExpected = CanShow;
            int c = m_db.SaveChanges();
            return c > 0 ?
             new ResponseDTO() { Status = Data.DTO.StatusCode.Success }
             :
             new ResponseDTO() { Status = Data.DTO.StatusCode.Error, StatusText = "לא הצלחנו לשמור את השינוים" };
        }

        //JWT קבלת מזהה מתכון אחרון על פי
        public int  GetLastRecipeIdByJWTForUser()
        {
            Recipe LastRecipeId = m_db.Recipe.Where(r=>r.UserId ==_UserService.GetUserIdByJWT()).ToList().Last();
            return LastRecipeId.Id;
        }

        //קבלת מתכון לפי מזהה מתכון עם אוביקטים למנהל
        public Recipe GetRecipeByIdForAdmin(int RecipeId)
        {
            var RecipeObj = m_db.Recipe.Where(r => r.Id == RecipeId).Select(ee => new Recipe()
            {
                Id = ee.Id,
                UserId = ee.UserId,
                SubcategoryId = ee.SubcategoryId,
                RecipeName = ee.RecipeName,
                Ingredients = ee.Ingredients,
                PreparationMethod = ee.PreparationMethod,
                PreparationTime = ee.PreparationTime,
                QuantityOfPortions = ee.QuantityOfPortions,
                UploadDate = ee.UploadDate,
                CanBeExpected = ee.CanBeExpected,
                NumberOfViews = ee.NumberOfViews,
                NumberOfLikes = ee.NumberOfLikes,
                NumberOfNoLieks = ee.NumberOfNoLieks,
                User = ee.User,
                Subcategory = ee.Subcategory,
               
            }).FirstOrDefault();
            //הכנסת הקטגוריה לתת הקטגוריה
            RecipeObj.Subcategory.Category = m_db.Category.Where(r => r.Id == RecipeObj.Subcategory.CategoryId).FirstOrDefault();
            //הסתרת הסיסמה של המשתמש
            RecipeObj.User.Password = null;

            return RecipeObj;
        }

        //קבלת מתכון שניתן לצפות בו לפי מזהה מתכון עם אוביקטים
        public Recipe GetRecipeCanBeExpectedById(int RecipeId)
        {
            var RecipeObj = m_db.Recipe.Where(r => r.Id == RecipeId && r.CanBeExpected == true).Select(ee => new Recipe()
            {
                Id = ee.Id,
                UserId = ee.UserId,
                SubcategoryId = ee.SubcategoryId,
                RecipeName = ee.RecipeName,
                Ingredients = ee.Ingredients,
                PreparationMethod = ee.PreparationMethod,
                PreparationTime = ee.PreparationTime,
                QuantityOfPortions = ee.QuantityOfPortions,
                UploadDate = ee.UploadDate,
                CanBeExpected = ee.CanBeExpected,
                NumberOfViews = ee.NumberOfViews,
                NumberOfLikes = ee.NumberOfLikes,
                NumberOfNoLieks = ee.NumberOfNoLieks,
                User = ee.User,
                Subcategory = ee.Subcategory,

            }).FirstOrDefault();
            if(RecipeObj != null)
            {
                //הכנסת הקטגוריה לתת הקטגוריה
                RecipeObj.Subcategory.Category = m_db.Category.Where(r => r.Id == RecipeObj.Subcategory.CategoryId).FirstOrDefault();
                //הסתרת הסיסמה של המשתמש
                RecipeObj.User.Password = null;
            }
            return RecipeObj;
        }

        //JWT קבלת מתכון לפי מזהה מתכון עם אוביקטים למשתמש על פי
        public Recipe GetRecipeByIdAndJWTForUser(int RecipeId)
        {
            var RecipeObj = m_db.Recipe.Where(r => r.Id == RecipeId && r.UserId == _UserService.GetUserIdByJWT()).Select(ee => new Recipe()
            {
                Id = ee.Id,
                UserId = ee.UserId,
                SubcategoryId = ee.SubcategoryId,
                RecipeName = ee.RecipeName,
                Ingredients = ee.Ingredients,
                PreparationMethod = ee.PreparationMethod,
                PreparationTime = ee.PreparationTime,
                QuantityOfPortions = ee.QuantityOfPortions,
                UploadDate = ee.UploadDate,
                CanBeExpected = ee.CanBeExpected,
                NumberOfViews = ee.NumberOfViews,
                NumberOfLikes = ee.NumberOfLikes,
                NumberOfNoLieks = ee.NumberOfNoLieks,
                User = ee.User,
                Subcategory = ee.Subcategory,

            }).FirstOrDefault();
            if(RecipeObj!=null)
            {
                //הכנסת הקטגוריה לתת הקטגוריה
                RecipeObj.Subcategory.Category = m_db.Category.Where(r => r.Id == RecipeObj.Subcategory.CategoryId).FirstOrDefault();
                //הסתרת הסיסמה של המשתמש
                RecipeObj.User.Password = null;
            }
            return RecipeObj;
        }

        //קבלת רשימת כל המתכונים כולל אוביקטים למנהל
        public List<Recipe> GetAllRecipeForAdmin()
        {
            var RecipeObj = m_db.Recipe.Select(ee => new Recipe()
            {
                Id = ee.Id,
                UserId = ee.UserId,
                SubcategoryId = ee.SubcategoryId,
                RecipeName = ee.RecipeName,
                Ingredients = ee.Ingredients,
                PreparationMethod = ee.PreparationMethod,
                PreparationTime = ee.PreparationTime,
                QuantityOfPortions = ee.QuantityOfPortions,
                UploadDate = ee.UploadDate,
                CanBeExpected = ee.CanBeExpected,
                NumberOfViews = ee.NumberOfViews,
                NumberOfLikes = ee.NumberOfLikes,
                NumberOfNoLieks = ee.NumberOfNoLieks,
                User = ee.User,
                Subcategory = ee.Subcategory,

            }).ToList();
            for(int i=0;i<RecipeObj.Count;i++)
            {
                RecipeObj[i].Subcategory.Category = m_db.Category.Where(r => r.Id == RecipeObj[i].Subcategory.CategoryId).FirstOrDefault();
                RecipeObj[i].User.Password = null;
            }
            return RecipeObj;
        }

        //קבלת רשימת כל המתכונים שמותר לצפות בהם עם אוביקטים
        public List<Recipe> GetAllRecipeCanBeExpected()
        {
            var RecipeObj = m_db.Recipe.Where(r => r.CanBeExpected == true).Select(ee => new Recipe()
            {
                Id = ee.Id,
                UserId = ee.UserId,
                SubcategoryId = ee.SubcategoryId,
                RecipeName = ee.RecipeName,
                Ingredients = ee.Ingredients,
                PreparationMethod = ee.PreparationMethod,
                PreparationTime = ee.PreparationTime,
                QuantityOfPortions = ee.QuantityOfPortions,
                UploadDate = ee.UploadDate,
                CanBeExpected = ee.CanBeExpected,
                NumberOfViews = ee.NumberOfViews,
                NumberOfLikes = ee.NumberOfLikes,
                NumberOfNoLieks = ee.NumberOfNoLieks,
                User = ee.User,
                Subcategory = ee.Subcategory,

            }).ToList();
            for (int i = 0; i < RecipeObj.Count; i++)
            {
                //עשה בעיות לבדוק למה ואם לא מצליח לחשוב על פתרון אחר במקום
               // RecipeObj[i].Subcategory.Category = m_db.Category.Where(r => r.Id == RecipeObj[i].Subcategory.CategoryId).FirstOrDefault();
                RecipeObj[i].User.Password = null;
            }
            return RecipeObj;
        }

        // JWT קבלת רשימת כל המתכונים של משתמש למשתמש לפי 
        public List<Recipe> GetAllRecipeJWTForUser()
        {
            var RecipeObj = m_db.Recipe.Where(r => r.UserId == _UserService.GetUserIdByJWT()).Select(ee => new Recipe()
            {
                Id = ee.Id,
                UserId = ee.UserId,
                SubcategoryId = ee.SubcategoryId,
                RecipeName = ee.RecipeName,
                Ingredients = ee.Ingredients,
                PreparationMethod = ee.PreparationMethod,
                PreparationTime = ee.PreparationTime,
                QuantityOfPortions = ee.QuantityOfPortions,
                UploadDate = ee.UploadDate,
                CanBeExpected = ee.CanBeExpected,
                NumberOfViews = ee.NumberOfViews,
                NumberOfLikes = ee.NumberOfLikes,
                NumberOfNoLieks = ee.NumberOfNoLieks,
                User = ee.User,
                Subcategory = ee.Subcategory,

            }).ToList();
            for (int i = 0; i < RecipeObj.Count; i++)
            {
                //RecipeObj[i].Subcategory.Category = m_db.Category.Where(r => r.Id == RecipeObj[i].Subcategory.CategoryId).FirstOrDefault();
                RecipeObj[i].User.Password = null;
            }
            return RecipeObj;
        }

        //קבלת רשימת כל המתכונים שאפשר לצפות בהם של משתמש מסוים לפי מזהה משתמש
        public List<Recipe> GetAllRecipeCanBeExpectedByUserId(int UserId)
        {
            var RecipeObj = m_db.Recipe.Where(r => r.UserId == UserId && r.CanBeExpected == true).Select(ee => new Recipe()
            {
                Id = ee.Id,
                UserId = ee.UserId,
                SubcategoryId = ee.SubcategoryId,
                RecipeName = ee.RecipeName,
                Ingredients = ee.Ingredients,
                PreparationMethod = ee.PreparationMethod,
                PreparationTime = ee.PreparationTime,
                QuantityOfPortions = ee.QuantityOfPortions,
                UploadDate = ee.UploadDate,
                CanBeExpected = ee.CanBeExpected,
                NumberOfViews = ee.NumberOfViews,
                NumberOfLikes = ee.NumberOfLikes,
                NumberOfNoLieks = ee.NumberOfNoLieks,
                User = ee.User,
                Subcategory = ee.Subcategory,

            }).ToList();
            for (int i = 0; i < RecipeObj.Count; i++)
            {
                //RecipeObj[i].Subcategory.Category = m_db.Category.Where(r => r.Id == RecipeObj[i].Subcategory.CategoryId).FirstOrDefault();
                RecipeObj[i].User.Password = null;
            }
            return RecipeObj;
        }

        //קבלת רשימת כל המתכונים שניתן לצפות בהם לפי מזהה תת קטגוריה
        public List<Recipe> GetAllRecipeCanBeExpectedBySubcategoryId(int SubcategoryId)
        {
            var RecipeObj = m_db.Recipe.Where(r => r.SubcategoryId == SubcategoryId && r.CanBeExpected == true).Select(ee => new Recipe()
            {
                Id = ee.Id,
                UserId = ee.UserId,
                SubcategoryId = ee.SubcategoryId,
                RecipeName = ee.RecipeName,
                Ingredients = ee.Ingredients,
                PreparationMethod = ee.PreparationMethod,
                PreparationTime = ee.PreparationTime,
                QuantityOfPortions = ee.QuantityOfPortions,
                UploadDate = ee.UploadDate,
                CanBeExpected = ee.CanBeExpected,
                NumberOfViews = ee.NumberOfViews,
                NumberOfLikes = ee.NumberOfLikes,
                NumberOfNoLieks = ee.NumberOfNoLieks,
                User = ee.User,
                Subcategory = ee.Subcategory,

            }).ToList();
            for (int i = 0; i < RecipeObj.Count; i++)
            {
                //RecipeObj[i].Subcategory.Category = m_db.Category.Where(r => r.Id == RecipeObj[i].Subcategory.CategoryId).FirstOrDefault();
                RecipeObj[i].User.Password = null;
            }
            return RecipeObj;
        }

        //קבלת רשימת כל המתכונים שניתן לצפות בהם לפי מזהה קטגוריה
        public List<Recipe> GetAllRecipeCanBeExpectedByCategoryId(int CategoryId)
        {
            var RecipeObj = m_db.Recipe.Where(r => r.CanBeExpected == true).Select(ee => new Recipe()
            {
                Id = ee.Id,
                UserId = ee.UserId,
                SubcategoryId = ee.SubcategoryId,
                RecipeName = ee.RecipeName,
                Ingredients = ee.Ingredients,
                PreparationMethod = ee.PreparationMethod,
                PreparationTime = ee.PreparationTime,
                QuantityOfPortions = ee.QuantityOfPortions,
                UploadDate = ee.UploadDate,
                CanBeExpected = ee.CanBeExpected,
                NumberOfViews = ee.NumberOfViews,
                NumberOfLikes = ee.NumberOfLikes,
                NumberOfNoLieks = ee.NumberOfNoLieks,
                User = ee.User,
                Subcategory = ee.Subcategory,

            }).ToList();
            List<Recipe> LRecipe=new List<Recipe>();
            for (int i=0;i<RecipeObj.Count;i++)
            {
                if (RecipeObj[i].Subcategory.CategoryId == CategoryId) 
                {
                    LRecipe.Add(RecipeObj[i]);
                }
            }
            for (int i = 0; i < LRecipe.Count; i++)
            {
                //LRecipe[i].Subcategory.Category = m_db.Category.Where(r => r.Id == LRecipe[i].Subcategory.CategoryId).FirstOrDefault();
                LRecipe[i].User.Password = null;
            }   
            return LRecipe;
        }

        //קבלת רשימת המתכונים בצורה ממוינת מי הנצפים ביותר שניתן לצפות בהם
        public List<Recipe> GetAllRecipeCanBeExpectedByNumberOfViews()
        {
            List<Recipe> LRecipe = GetAllRecipeCanBeExpected();
            for (int i = 0; i < LRecipe.Count; i++) 
            {
                for (int c = 0; c < LRecipe.Count; c++) 
                {
                    if(LRecipe[i].NumberOfViews > LRecipe[c].NumberOfViews)
                    {
                        Recipe temp = LRecipe[i];
                        LRecipe[i] =LRecipe[c];
                        LRecipe[c] = temp;
                    }
                }
            }       
            return LRecipe;
        }

        //JWT קבלת רשימת המתכונים בצורה ממוינת מי הנצפים ביותר של משתמש על פי
        public List<Recipe> GetAllRecipeByJWTAndByNumberOfViewsForUser()
        {
            List<Recipe> LRecipe = GetAllRecipeJWTForUser();
            for (int i = 0; i < LRecipe.Count; i++)
            {
                for (int c = 0; c < LRecipe.Count; c++)
                {
                    if (LRecipe[i].NumberOfViews > LRecipe[c].NumberOfViews)
                    {
                        Recipe temp = LRecipe[i];
                        LRecipe[i] = LRecipe[c];
                        LRecipe[c] = temp;
                    }
                }
            }
            return LRecipe;
        }

        //קבלת רשימת מתכונים בצורה ממוינת מי האהובים ביותר שניתן לצפות בהם
        public List<Recipe> GetAllRecipeCanBeExpectedByNumberOfLikes()
        {
            List<Recipe> LRecipe = GetAllRecipeCanBeExpected();
            for (int i = 0; i < LRecipe.Count; i++)
            {
                for (int c = 0; c < LRecipe.Count; c++)
                {
                    if (LRecipe[i].NumberOfLikes > LRecipe[c].NumberOfLikes)
                    {
                        Recipe temp = LRecipe[i];
                        LRecipe[i] = LRecipe[c];
                        LRecipe[c] = temp;
                    }
                }
            }
            return LRecipe;
        }

        //JWT קבלת רשימת מתכונים בצורה ממוינת מי האהובים ביותר של משתמש על פי
        public List<Recipe> GetAllRecipeByJWTAndByNumberOfLikesForUser()
        {
            List<Recipe> LRecipe = GetAllRecipeJWTForUser();
            for (int i = 0; i < LRecipe.Count; i++)
            {
                for (int c = 0; c < LRecipe.Count; c++)
                {
                    if (LRecipe[i].NumberOfLikes > LRecipe[c].NumberOfLikes)
                    {
                        Recipe temp = LRecipe[i];
                        LRecipe[i] = LRecipe[c];
                        LRecipe[c] = temp;
                    }
                }
            }
            return LRecipe;
        }

        //קבלת רשימת מתכונים בצורה ממוינת מי הכי לא אהובים שניתן לצפות בהם
        public List<Recipe> GetAllRecipeCanBeExpectedByNumberOfNoLieks()
        {
            List<Recipe> LRecipe = GetAllRecipeCanBeExpected();
            for (int i = 0; i < LRecipe.Count; i++)
            {
                for (int c = 0; c < LRecipe.Count; c++)
                {
                    if (LRecipe[i].NumberOfNoLieks > LRecipe[c].NumberOfNoLieks)
                    {
                        Recipe temp = LRecipe[i];
                        LRecipe[i] = LRecipe[c];
                        LRecipe[c] = temp;
                    }
                }
            }
            return LRecipe;
        }

        //JWT קבלת רשימת מתכונים בצורה ממוינת מי הכי לא אהובים של משתמש על פי
        public List<Recipe> GetAllRecipeByJWTAndByNumberOfNoLieksForUser()
        {
            List<Recipe> LRecipe = GetAllRecipeJWTForUser();
            for (int i = 0; i < LRecipe.Count; i++)
            {
                for (int c = 0; c < LRecipe.Count; c++)
                {
                    if (LRecipe[i].NumberOfNoLieks > LRecipe[c].NumberOfNoLieks)
                    {
                        Recipe temp = LRecipe[i];
                        LRecipe[i] = LRecipe[c];
                        LRecipe[c] = temp;
                    }
                }
            }
            return LRecipe;
        }

        //קבלת רשימת מתכונים בצורה ממוינת מי החדשים ביותר שניתן לצפות בהם
        public List<Recipe> GetAllRecipeCanBeExpectedByUploadDate()
        {
            List<Recipe> LRecipe = GetAllRecipeCanBeExpected();
            for (int i = 0; i < LRecipe.Count; i++)
            {
                for (int c = 0; c < LRecipe.Count; c++)
                {
                    if (LRecipe[i].UploadDate > LRecipe[c].UploadDate)
                    {
                        Recipe temp = LRecipe[i];
                        LRecipe[i] = LRecipe[c];
                        LRecipe[c] = temp;
                    }
                }
            }
            return LRecipe;
        }

        //JWT קבלת רשימת מתכונים בצורה ממוינת מי החדשים ביותר של משתמש על פי
        public List<Recipe> GetAllRecipeByJWTAndByUploadDateForUser()
        {
            List<Recipe> LRecipe = GetAllRecipeJWTForUser();
            for (int i = 0; i < LRecipe.Count; i++)
            {
                for (int c = 0; c < LRecipe.Count; c++)
                {
                    if (LRecipe[i].UploadDate > LRecipe[c].UploadDate)
                    {
                        Recipe temp = LRecipe[i];
                        LRecipe[i] = LRecipe[c];
                        LRecipe[c] = temp;
                    }
                }
            }
            return LRecipe;
        }       
    }
}
