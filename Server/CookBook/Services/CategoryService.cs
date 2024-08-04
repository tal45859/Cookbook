using CookBook.Data;
using CookBook.Data.DTO;
using CookBook.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace CookBook.Services
{
    public class CategoryService
    {
        //תקציר
        //////////
        // בנאי
        // קבלת קטגוריה לפי מזהה
        // קבלת קטגוריה לפי שם קטגוריה
        // קבלת כל הקטגוריות
        // יצירת קטגוריה
        // מחיקת קטגוריה
        // עדכון קטגוריה
        
        
        //כלל
        // nullלעשות ידנית שבמקרה שלוחצים למחוק קטגוריה אז לעבור על כל המתכונים שתת הקטגוריה מכילה את הקטגוריה ולהחליף את המפתח זר ל
        private readonly CookBookDBContext m_db;
        private readonly SubcategoryService _SubcategoryService;

        // בנאי
        public CategoryService(CookBookDBContext db , SubcategoryService subcategoryService)
        {
            m_db = db;
            _SubcategoryService = subcategoryService;
        }

        // קבלת קטגוריה לפי מזהה
        public Category GetCategoryById(int CategoryId)
        {
            return m_db.Category.Where(c => c.Id == CategoryId).FirstOrDefault();
        }

        // קבלת קטגוריה לפי שם קטגוריה
        public Category GetCategoryByCategoryName(string CategoryName)
        {
            return m_db.Category.Where(c=>c.CategoryName == CategoryName).FirstOrDefault();
        }

        // קבלת כל הקטגוריות
        public List<Category> GetAllCategory()
        {
            return m_db.Category.ToList();
        }

        // יצירת קטגוריה
        public bool AddCategory(CategoryDTO CategoryFromUserToAdd)
        {
            Category CategoryToAdd =new Category();
            CategoryToAdd.CategoryName = CategoryFromUserToAdd.CategoryName;
            m_db.Category.Add(CategoryToAdd);
            int c = m_db.SaveChanges();
            return c > 0;
        }
 
        // מחיקת קטגוריה
        public ResponseDTO DeleteCategory(int CategoryId)
        {
            Category CategoryToDelete = GetCategoryById(CategoryId);
            if (CategoryToDelete == null || CategoryToDelete.Id != CategoryId)
            {
                return new ResponseDTO() { Status = Data.DTO.StatusCode.Error, StatusText = $"לא נמצא בבסיס הנתונים {CategoryId} קטגוריה בעלת מזהה " };
            }
            List<Subcategory> LSubcategory = _SubcategoryService.GetAllSubcategoryByCategoryId(CategoryToDelete.Id);//מביאים את כל התתי קטגוריות
            _SubcategoryService.ChangeRecipeColumnValueForListSubcategory(LSubcategory);//משנים לכל המתכונים שמכילים את התתי קטגוריות את הערך ל0
            m_db.Category.Remove(CategoryToDelete);
            int c = m_db.SaveChanges();
            return c > 0 ?
              new ResponseDTO() { Status = Data.DTO.StatusCode.Success }
              :
              new ResponseDTO() { Status = Data.DTO.StatusCode.Error, StatusText = "לא הצלחנו למחוק את הקטגוריה" };
        }


        // עדכון קטגוריה
        public ResponseDTO UpdateCategory(CategoryDTO CategoryToUpdateFromUser)
        {
            Category CategoryToUpdate = GetCategoryById(CategoryToUpdateFromUser.Id);
            if (CategoryToUpdate == null || CategoryToUpdate.Id != CategoryToUpdateFromUser.Id)
            {
                return new ResponseDTO() { Status = Data.DTO.StatusCode.Error, StatusText = $"לא נמצא בבסיס הנתונים {CategoryToUpdateFromUser.Id} קטגוריה בעלת מזהה " };
            }
            CategoryToUpdate.CategoryName = CategoryToUpdateFromUser.CategoryName;
            int c = m_db.SaveChanges();
            return c > 0 ?
              new ResponseDTO() { Status = Data.DTO.StatusCode.Success }
              :
              new ResponseDTO() { Status = Data.DTO.StatusCode.Error, StatusText = "לא הצלחנו לשמור את השינוים" };
        }

    }
}
