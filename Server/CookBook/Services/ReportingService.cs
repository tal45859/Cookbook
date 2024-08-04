using CookBook.Data;
using CookBook.Data.DTO;
using CookBook.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace CookBook.Services
{
    public class ReportingService
    {
        //תקציר
        ///////////
        //בנאי
        //קבלת דיווח לפי מזהה
        //הוספת דיווח
        //מחיקת דיווח
        //שינוי מצב דיווח לפתור והוספת הערת סיבת סיום
        //קבלת רשימת דיווחים לא פתורים
        //קבלת רשימת דיווחים פתורים
        //קבלת רשימת כל הדיווחים
        //קבלת רשימת דיווחים לפי מזהה מתכון
        //
        private readonly CookBookDBContext m_db;

        //בנאי
        public ReportingService(CookBookDBContext db)
        {
            m_db = db;
        }

        //קבלת דיווח לפי מזהה
        public Reporting GetReportingById(int ReportingId)
        {
            return m_db.Reporting.Where(r => r.Id == ReportingId).FirstOrDefault();
        }

        //הוספת דיווח
        public bool AddReporting(ReportingDTO ReportingToAddFromUser)
        {
            Reporting ReportingToAdd = new Reporting();
            ReportingToAdd.Cause = ReportingToAddFromUser.Cause;
            ReportingToAdd.RecipeId = ReportingToAddFromUser.RecipeId;
            ReportingToAdd.IsActive = false;//לא פתור
            m_db.Reporting.Add(ReportingToAdd);
            int c = m_db.SaveChanges();
            return c > 0;
        }

        //מחיקת דיווח
        public ResponseDTO DeleteReportingById(int ReportingId)
        {
            Reporting ReportingToDelete = GetReportingById(ReportingId);
            if (ReportingToDelete == null || ReportingToDelete.Id != ReportingId) 
            {
                return new ResponseDTO() { Status = Data.DTO.StatusCode.Error, StatusText = "הדיווח לא נמצא בבסיס נתונים" };
            }
            m_db.Reporting.Remove(ReportingToDelete);
            int c = m_db.SaveChanges();
            return c > 0 ?
                 new ResponseDTO() { Status = Data.DTO.StatusCode.Success }
                 :
                 new ResponseDTO() { Status = Data.DTO.StatusCode.Error, StatusText = "לא הצלחנו למחוק את הדיווח" };
        }


        //שינוי מצב דיווח לפתור והוספת הערת סיבת סיום
        public ResponseDTO UpdateReporting(ReportingDTO ReportingFromUserToUpdate)
        {
            Reporting ReportingToUpdate = GetReportingById(ReportingFromUserToUpdate.Id);
            if(ReportingToUpdate==null|| ReportingToUpdate.Id!= ReportingFromUserToUpdate.Id)
            {
                return new ResponseDTO() { Status = Data.DTO.StatusCode.Error, StatusText = "הדיווח לא נמצא בבסיס נתונים" };
            }
            ReportingToUpdate.IsActive = ReportingFromUserToUpdate.IsActive;
            ReportingToUpdate.ClosingExplanation = ReportingFromUserToUpdate.ClosingExplanation;
            int c = m_db.SaveChanges();
            return c > 0 ?
                new ResponseDTO() { Status = Data.DTO.StatusCode.Success }
                :
                new ResponseDTO() { Status = Data.DTO.StatusCode.Error, StatusText = "לא הצלחנו לשמור את השינוים" };
        }

        //קבלת רשימת דיווחים לא פתורים
        public List<Reporting> GetAllReportingActive()
        {
            return m_db.Reporting.Where(r => r.IsActive == false).ToList();//false לא פתור
        }

        //קבלת רשימת דיווחים פתורים
        public List<Reporting> GetAllReportingNoActive()
        {
            return m_db.Reporting.Where(r => r.IsActive == true).ToList();//true  פתור
        }

        //קבלת רשימת כל הדיווחים
        public List<Reporting> GetAllReporting()
        {
            return m_db.Reporting.ToList();
        }

        //קבלת רשימת דיווחים לפי מזהה מתכון
        public List<Reporting> GetAllReportingByRecipeId(int RecipeId)
        {
            return m_db.Reporting.Where(r=>r.RecipeId== RecipeId).ToList();
        }


    }
}
