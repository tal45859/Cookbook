using CookBook.Data;
using CookBook.Data.DTO;
using CookBook.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace CookBook.Services
{
    public class FavoriteService
    {
        //תקציר
        //////////
        //בנאי
        //קבלת מועדף נקי מאוביקטים לשימוש פנימי
        //הוספת מועדף
        //מחיקת מועדף
        //JWT קבלת מועדף לפי מזהה מועדף ולפי 
        //JWT קבלת רשימת מועדפים לפי 
        //
        //
        //

        //כלל
        //לעשות שאם בן אדם בחר להסתיר את המתכון שלו גם הפיבוריט נמחק ולא יוצג
        //(בטבלת מועדפים נביא את המתכון ואת המשתמש שייצר את המתכון (לא המשתמש שהוסיף לעצמו מועדף
        //יתכן שניצטרך להביא להביא בתוך אוביקט מתכון את אוביקט המשתמש שיצר ללא סיסמה ואת אוביקט תת הקטגוריה שיכיל בתוכו את הקטגוריה

        private readonly CookBookDBContext m_db;
        private readonly UserService _UserService;

        //בנאי
        public FavoriteService(CookBookDBContext db, UserService userService)
        {
            m_db = db;
            _UserService = userService;
        }

        //קבלת מועדף נקי מאוביקטים לשימוש פנימי
        public Favorite GetFavoriteByIdFromDb(int FavoriteId)
        {
            return m_db.Favorite.Where(f => f.Id == FavoriteId).FirstOrDefault();
        }

        //הוספת מועדף
        public bool AddFavorite(FavoriteDTO FavoriteToAddFromUser)
        {
            Favorite FavoriteToAddToDb =new Favorite();
            FavoriteToAddToDb.UserId = _UserService.GetUserIdByJWT();
            FavoriteToAddToDb.RecipeId = FavoriteToAddFromUser.RecipeId;
            FavoriteToAddToDb.DateAdded = System.DateTime.Now;
            m_db.Favorite.Add(FavoriteToAddToDb);
            int c = m_db.SaveChanges();
            return c > 0;
        }

        //מחיקת מועדף
        public ResponseDTO DeleteFavoriteById(int FavoriteId)
        {
            Favorite FavoriteToDelete = GetFavoriteByIdFromDb(FavoriteId);
            if(FavoriteToDelete==null|| FavoriteToDelete.Id !=FavoriteId || FavoriteToDelete.UserId!= _UserService.GetUserIdByJWT())
            {
                return new ResponseDTO() { Status = Data.DTO.StatusCode.Error, StatusText = "המועדף לא נמצא בבסיס נתונים" };
            }
            m_db.Favorite.Remove(FavoriteToDelete);
            int c = m_db.SaveChanges();
            return c > 0 ?
              new ResponseDTO() { Status = Data.DTO.StatusCode.Success }
              :
              new ResponseDTO() { Status = Data.DTO.StatusCode.Error, StatusText = "לא הצלחנו למחוק את המועדף" };
        }

        //JWT קבלת מועדף לפי מזהה מועדף ולפי 
        public Favorite GetFavoriteByIdAndJwt(int FavoriteId)
        {
            var FavoriteObj = m_db.Favorite.Where(f => f.Id == FavoriteId && f.UserId == _UserService.GetUserIdByJWT()).Select(ee => new Favorite()
            { 
                Id = ee.Id,
                UserId = ee.UserId,
                RecipeId = ee.RecipeId,
                DateAdded = ee.DateAdded,
                Recipe = ee.Recipe
            }).FirstOrDefault();
            FavoriteObj.Recipe.User = m_db.User.Where(u => u.Id == FavoriteObj.Recipe.UserId).FirstOrDefault();
            FavoriteObj.Recipe.Subcategory = m_db.Subcategory.Where(s=>s.Id == FavoriteObj.Recipe.SubcategoryId).FirstOrDefault();
            FavoriteObj.Recipe.Subcategory.Category = m_db.Category.Where(c=>c.Id == FavoriteObj.Recipe.Subcategory.CategoryId).FirstOrDefault();
            return FavoriteObj;
        }

        //JWT קבלת רשימת מועדפים לפי 
        public List<Favorite> GetAllFavoriteJwt()
        {
            var FavoriteObj = m_db.Favorite.Where(f => f.UserId == _UserService.GetUserIdByJWT()).Select(ee => new Favorite()
            {
                Id = ee.Id,
                UserId = ee.UserId,
                RecipeId = ee.RecipeId,
                DateAdded = ee.DateAdded,
                Recipe = ee.Recipe
            }).ToList();
            //החלק התחתון עושה ביותר
            //for(int i=0;i< FavoriteObj.Count;i++)
            //{
            //    FavoriteObj[i].Recipe.User = m_db.User.Where(u => u.Id == FavoriteObj[i].Recipe.UserId).FirstOrDefault();
            //    FavoriteObj[i].Recipe.User.Password = null;
            //    FavoriteObj[i].Recipe.Subcategory = m_db.Subcategory.Where(s => s.Id == FavoriteObj[i].Recipe.SubcategoryId).FirstOrDefault();
            //    //FavoriteObj[i].Recipe.Subcategory.Category = m_db.Category.Where(c => c.Id == FavoriteObj[i].Recipe.Subcategory.CategoryId).FirstOrDefault();
            //}
            return FavoriteObj;
        }

    }
}
