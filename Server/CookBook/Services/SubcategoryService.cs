using CookBook.Data;
using CookBook.Data.DTO;
using CookBook.Data.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CookBook.Services
{
    public class SubcategoryService
    {
        //תקציר
        ////////////
        //בנאי
        //קבלת תת קטגוריה לפי מזהה נקי מאוביקטים לשימוש פנימי
        //קבלת תת קטגוריה לפי מזהה תת קטגוריה
        //קבלת רשימת תתי קטגוריה עם אוביקט קטגוריה
        //קבלת אוביקט תת קטגוריה לפי מזהה קטגוריה
        //קבלת רשימת תתי קטגוריה של קטגוריה לפי מזהה קטגוריה
        // הוספת תת קטגוריה
        // עדכון תת קטגוריה
        // מחיקת תת קטגוריה
        //שינוי שדה לערך 0 לכל המתכונים שמכילים את תת הקטגוריה שהולכת להימחק
        //לרשימת תת קטגוריות - שינוי שדה לערך 0 לכל המתכונים שמכילים את תת הקטגוריה שהולכת להימחק 

        //כלל
        // nullלעשות ידנית שבמקרה שלוחצים למחוק קטגוריה אז לעבור על כל המתכונים שתת הקטגוריה מכילה את הקטגוריה ולהחליף את המפתח זר ל

        private readonly CookBookDBContext m_db;
        //בנאי
        public SubcategoryService(CookBookDBContext db)
        {
            m_db = db;
        }

        //קבלת תת קטגוריה לפי מזהה נקי מאוביקטים לשימוש פנימי
        public Subcategory GetSubcategoryByIdFromDB(int id)
        {
            return m_db.Subcategory.Where(s => s.Id == id).FirstOrDefault();
        }

        //קבלת תת קטגוריה לפי מזהה תת קטגוריה
        public Subcategory GetSubcategoryById(int id)
        {
            var SubcategoryObj = m_db.Subcategory.Where(subcategory => subcategory.Id == id).Select(ee => new Subcategory()
            {
                Id = ee.Id,
                CategoryId=ee.CategoryId,
                SubcategoryName=ee.SubcategoryName,
                Category=ee.Category,
            }).FirstOrDefault();
            return SubcategoryObj;
        }

        //קבלת רשימת תתי קטגוריה עם אוביקט קטגוריה
        public List<Subcategory> GetAllSubcategory()
        {
            var SubcategoryObjList = m_db.Subcategory.Include(i => i.Category).Select(ee => new Subcategory()
            {
                Id = ee.Id,
                CategoryId = ee.CategoryId,
                SubcategoryName = ee.SubcategoryName,
                Category = ee.Category,
            }).ToList();
            return SubcategoryObjList;
        }

        //קבלת אוביקט תת קטגוריה לפי מזהה קטגוריה
        public Subcategory GetSubcategoryByCategoryId(int CategoryId)
        {
            var SubcategoryObj = m_db.Subcategory.Where(subcategory => subcategory.CategoryId == CategoryId).Select(ee => new Subcategory()
            {
                Id = ee.Id,
                CategoryId = ee.CategoryId,
                SubcategoryName = ee.SubcategoryName,
                Category = ee.Category,
            }).FirstOrDefault();
            return SubcategoryObj;
        }

        //קבלת רשימת תתי קטגוריה של קטגוריה לפי מזהה קטגוריה
        public List<Subcategory> GetAllSubcategoryByCategoryId(int CategoryId)
        {
            var SubcategoryObjList = m_db.Subcategory.Where(s => s.CategoryId == CategoryId).Include(i => i.Category).Select(ee => new Subcategory()
            {
                Id = ee.Id,
                CategoryId = ee.CategoryId,
                SubcategoryName = ee.SubcategoryName,
                Category = ee.Category,
            }).ToList();
            return SubcategoryObjList;
        }

        // הוספת תת קטגוריה
        public bool AddSubcategory(SubcategoryDTO SubcategoryToAddFromUser)
        {
            Subcategory SubcategoryToAdd = new Subcategory();
            SubcategoryToAdd.SubcategoryName=SubcategoryToAddFromUser.SubcategoryName;
            SubcategoryToAdd.CategoryId=SubcategoryToAddFromUser.CategoryId;
            m_db.Subcategory.Add(SubcategoryToAdd);
            int c = m_db.SaveChanges();
            return c > 0;
        }

        // עדכון תת קטגוריה
        public ResponseDTO UpdateSubcategory(SubcategoryDTO SubcategoryToUpdateFromUser)
        {
            Subcategory SubcategoryToUpdate = GetSubcategoryByIdFromDB(SubcategoryToUpdateFromUser.Id);
            if(SubcategoryToUpdate==null|| SubcategoryToUpdate.Id!=SubcategoryToUpdateFromUser.Id)
            {
                return new ResponseDTO() { Status = Data.DTO.StatusCode.Error, StatusText = "תת הקטגוריה לא נמצא בבסיס נתונים" };
            }
            SubcategoryToUpdate.SubcategoryName = SubcategoryToUpdateFromUser.SubcategoryName;
            SubcategoryToUpdate.CategoryId = SubcategoryToUpdateFromUser.CategoryId;
            int c = m_db.SaveChanges();
            return c > 0 ?
                new ResponseDTO() { Status = Data.DTO.StatusCode.Success }
                :
                new ResponseDTO() { Status = Data.DTO.StatusCode.Error, StatusText = "לא הצלחנו לעדכן את התת קטגוריה" };
        }

        // מחיקת תת קטגוריה
        // NULL להוסיף במחיקה שישנה לכל המתכונים את השדה של תת קטגוריה ל
        public ResponseDTO DeleteSubcategory(int SubcategoryId)
        {
            Subcategory SubcategoryToDelete = GetSubcategoryByIdFromDB(SubcategoryId);
            if (SubcategoryToDelete == null || SubcategoryToDelete.Id != SubcategoryId)
            {
                return new ResponseDTO() { Status = Data.DTO.StatusCode.Error, StatusText = "תת הקטגוריה לא נמצא בבסיס נתונים" };
            }
            ChangeRecipeColumnValue(SubcategoryToDelete.Id);
            m_db.Subcategory.Remove(SubcategoryToDelete);
            int c = m_db.SaveChanges();
            return c > 0 ?
                 new ResponseDTO() { Status = Data.DTO.StatusCode.Success }
                 :
                 new ResponseDTO() { Status = Data.DTO.StatusCode.Error, StatusText = "לא הצלחנו למחוק את התת קטגוריה" };

        }

        //שינוי שדה לערך 0 לכל המתכונים שמכילים את תת הקטגוריה שהולכת להימחק
        public void ChangeRecipeColumnValue(int SubcategoryId )
        {
            //לכל המתכונים בשדה תת קטגוריה שמקושרים לתת קטגוריה הנוכחית nullהפיכה ל
            List<Recipe> LRecipe = m_db.Recipe.Where(r => r.SubcategoryId == SubcategoryId).ToList();
            LRecipe.ForEach(r => r.SubcategoryId = 0);//אז במקום 0 nullלא נותן 
            m_db.SaveChanges();
        }


        //לרשימת תת קטגוריות - שינוי שדה לערך 0 לכל המתכונים שמכילים את תת הקטגוריה שהולכת להימחק 
        public void ChangeRecipeColumnValueForListSubcategory(List<Subcategory> LSubcategory)
        {
            for(int i=0;i< LSubcategory.Count;i++)
            {
                List<Recipe> LRecipe = m_db.Recipe.Where(r => r.SubcategoryId == LSubcategory[i].Id).ToList();
                LRecipe.ForEach(r => r.SubcategoryId = 0);//אז במקום 0 nullלא נותן 
                m_db.SaveChanges();
            } 
        }

    }
}

