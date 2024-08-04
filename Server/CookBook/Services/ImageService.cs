using CookBook.Data;
using CookBook.Data.DTO;
using CookBook.Data.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CookBook.Services
{
    public class ImageService
    {
        //תקציר
        //////////////
        //בנאי
        //קבלת תמונה לפי מזהה
        //קבלת רשימת תמונות לפי מזהה מתכון
        //הוספת תמונה חדשה
        //מחיקת תמונה לוודא שזה משתמש ששיכת לו התמונה
        //מחיקת תמונה לוודא שזה מנהל
        //קבלת כל התמונות של המתכונים שאפשר לראות

        //JWTקבלת כל התמונות על פי 

        private readonly CookBookDBContext m_db;
        private readonly UserService _UserService;

        //בנאי
        public ImageService(CookBookDBContext db, UserService userService)
        {
            m_db = db;
            _UserService = userService;
        }

        //קבלת תמונה לפי מזהה
        public Image GetImageById(int ImageId)
        {
            return m_db.Image.Where(i => i.Id == ImageId).FirstOrDefault();
        }

        //קבלת רשימת תמונות לפי מזהה מתכון
        public List<Image>GetAllImageByRecipeId(int RecipeId)
        {
            return m_db.Image.Where(i=>i.RecipeId == RecipeId).ToList();
        }

        //הוספת תמונה חדשה
        public bool AddImage(ImageDTO ImageToAddFromUser)
        {
            Image ImageToAdd = new Image();
            ImageToAdd.Url= "https://localhost:44328/StaticFiles/Images/" + ImageToAddFromUser.Url;
            ImageToAdd.RecipeId = ImageToAddFromUser.RecipeId;
            ImageToAdd.UploadDate = DateTime.Now;
            m_db.Image.Add(ImageToAdd);
            int c = m_db.SaveChanges();
            return c > 0;
        }

        //מחיקת תמונה לוודא שזה משתמש ששיכת לו התמונה
        public ResponseDTO DeleteImageForUser(int ImageId)
        {
            Image ImageToDelete = GetImageById(ImageId);
            Recipe recipe = m_db.Recipe.Where(r => r.Id == ImageToDelete.RecipeId).FirstOrDefault();
            User user = _UserService.GetUserByJWT();
            if (ImageToDelete == null || ImageToDelete.Id != ImageId || recipe.UserId != user.Id)
            {
                return new ResponseDTO() { Status = Data.DTO.StatusCode.Error, StatusText = "התמונה לא נמצאה בבסיס הנתונים" };
            }
            string url = "https://localhost:44328/";
            string UrlToDelete = ImageToDelete.Url.Substring(url.Length, ImageToDelete.Url.Length - url.Length);
            var PathToDelete = Path.Combine(Directory.GetCurrentDirectory(), UrlToDelete);
            FileInfo file = new FileInfo(PathToDelete);
            try
            {
                file.Delete();
                m_db.Image.Remove(ImageToDelete);
                m_db.SaveChanges();
            }
            catch
            {
                return new ResponseDTO() { Status = Data.DTO.StatusCode.Error, StatusText = "לא הצלחנו למחוק את התמונה" };
            }
            return new ResponseDTO() { Status = Data.DTO.StatusCode.Success };
        }


        //מחיקת תמונה לוודא שזה מנהל
        public ResponseDTO DeleteImageForAdmin(int ImageId)
        {
            Image ImageToDelete = GetImageById(ImageId);
            if(ImageToDelete == null || ImageToDelete.Id != ImageId)
            {
                return new ResponseDTO() { Status = Data.DTO.StatusCode.Error, StatusText = "התמונה לא נמצאה בבסיס הנתונים" };
            }
            string url = "https://localhost:44328/";
            string UrlToDelete = ImageToDelete.Url.Substring(url.Length, ImageToDelete.Url.Length - url.Length);
            var PathToDelete = Path.Combine(Directory.GetCurrentDirectory(), UrlToDelete);
            FileInfo file = new FileInfo(PathToDelete);
            try
            {
                file.Delete();
                m_db.Image.Remove(ImageToDelete);
                m_db.SaveChanges();
            }
            catch
            {
                return new ResponseDTO() { Status = Data.DTO.StatusCode.Error, StatusText = "לא הצלחנו למחוק את התמונה" };
            }
            return new ResponseDTO() { Status = Data.DTO.StatusCode.Success };
        }


        //קבלת כל התמונות של המתכונים שאפשר לראות
        public List<Image> GetAllImageCanBeExpected()
        {
            List<Recipe> recipes = m_db.Recipe.Where(r => r.CanBeExpected == true).ToList();
            List<Image> images = new List<Image>();
            for (int i = 0; i < recipes.Count; i++) 
            {
                List<Image>temp=m_db.Image.Where(image=> image.RecipeId == recipes[i].Id).ToList();
                for(int c=0;c<temp.Count; c++)
                {
                    images.Add(temp[c]);
                }              
            }         
            return images;
        }

        //JWTקבלת כל התמונות על פי 
        public List<Image>GetAllImageByJWT()
        {
            List<Recipe> recipes = m_db.Recipe.Where(r => r.UserId == _UserService.GetUserIdByJWT()).ToList();
            List<Image> images = new List<Image>();
            for (int i = 0; i < recipes.Count; i++)
            {
                List<Image> temp = m_db.Image.Where(image => image.RecipeId == recipes[i].Id).ToList();
                for (int c = 0; c < temp.Count; c++)
                {
                    images.Add(temp[c]);
                }
            }
            return images;
        }


    }
}
