using CookBook.Data;
using CookBook.Data.DTO;
using CookBook.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CookBook.Services
{
    public class LoginHistoryService
    {
        //תקציר
        //////////
        //בנאי
        //קבלת אוביקט לפי מזהה נקי מאוביקטים לשימוש פנימי
        //הוספת היסטוריה
        //מחיקת היסטוריה לפי מזהה היסטוריה
        //קבלת היסטוריה לפי מזהה היסטוריה עם אוביקט
        //קבלת רשימת היסטוריה של כל המשתמשים עם אוביקטים
        //קבלת רשימת היסטוריה של משתמש בודד לפי מזהה משתמש
        //קבלת רשימות של משתמשים שהתחברו אם אפשרות סינון של היום השבוע החודש והשנה וכל הזמן
        //
        //

        
        //דגשים בכל הזמן זה פשוט להביא את הרשימה של כולם

        private readonly CookBookDBContext m_db;
        private readonly UserService _UserService;

        //בנאי
        public LoginHistoryService(CookBookDBContext db, UserService userService)
        {
            m_db = db;
            _UserService = userService;
        }

        //קבלת אוביקט לפי מזהה נקי מאוביקטים לשימוש פנימי
        public LoginHistory GetLoginHistoryByIdFromDb(int LoginHistoryId)
        {
            return m_db.LoginHistory.Where(l=>l.Id==LoginHistoryId).FirstOrDefault();
        }

        //הוספת היסטוריה
        public bool AddLoginHistory()
        {
            LoginHistory LoginHistoryToAddToDB = new LoginHistory();
            LoginHistoryToAddToDB.UserId = _UserService.GetUserIdByJWT();
            LoginHistoryToAddToDB.DateAdded = System.DateTime.Now;
            m_db.LoginHistory.Add(LoginHistoryToAddToDB);
            int c = m_db.SaveChanges();
            return c > 0;
        }

        //מחיקת היסטוריה לפי מזהה היסטוריה
        public ResponseDTO DeleteLoginHistoryById(int LoginHistoryId)
        {
            LoginHistory LoginHistoryToDelete = GetLoginHistoryByIdFromDb(LoginHistoryId);
            if (LoginHistoryToDelete == null || LoginHistoryToDelete.Id != LoginHistoryId)
            {
                return new ResponseDTO() { Status = Data.DTO.StatusCode.Error, StatusText = "היסטורית ההתחברות לא נמצאה בבסיס נתונים" };
            }
            m_db.LoginHistory.Remove(LoginHistoryToDelete);
            int c = m_db.SaveChanges();
            return c > 0 ?
                  new ResponseDTO() { Status = Data.DTO.StatusCode.Success }
                  :
                  new ResponseDTO() { Status = Data.DTO.StatusCode.Error, StatusText = "לא הצלחנו למחוק את את היסטורית ההתחברות" };
        }

        //קבלת היסטוריה לפי מזהה היסטוריה עם אוביקט
        public LoginHistory GetLoginHistoryById(int LoginHistoryId)
        {
            var LoginHistoryObj = m_db.LoginHistory.Where(l => l.Id == LoginHistoryId).Select(ee => new LoginHistory()
            { 
                Id = ee.Id,
                UserId = ee.UserId,
                DateAdded = ee.DateAdded,
                User = ee.User
            }).FirstOrDefault();
            LoginHistoryObj.User.Password = null;
            return LoginHistoryObj;
        }

        //קבלת רשימת היסטוריה של כל המשתמשים עם אוביקטים
        public List<LoginHistory> GetAllLoginHistory()
        {
            var LoginHistoryObj = m_db.LoginHistory.Select(ee => new LoginHistory()
            {
                Id = ee.Id,
                UserId = ee.UserId,
                DateAdded = ee.DateAdded,
                User = ee.User
            }).ToList();
            LoginHistoryObj.ForEach(l => l.User.Password = null);
            return LoginHistoryObj;
        }

        //קבלת רשימת היסטוריה של משתמש בודד לפי מזהה משתמש
        public List<LoginHistory> GetAllLoginHistoryById(int LoginHistoryId)
        {
            var LoginHistoryObj = m_db.LoginHistory.Where(l => l.Id == LoginHistoryId).Select(ee => new LoginHistory()
            {
                Id = ee.Id,
                UserId = ee.UserId,
                DateAdded = ee.DateAdded,
                User = ee.User
            }).ToList();
            LoginHistoryObj.ForEach(l => l.User.Password = null);
            return LoginHistoryObj;
        }

        /// <summary>
        /// לפתור אתבעית השבוע הבודד אם לא אז להעיף
        /// </summary>
        /// <param name="RequestDate"></param>
        /// <returns></returns>
        //קבלת רשימות של משתמשים שהתחברו אם אפשרות סינון של היום השבוע החודש והשנה וכל הזמן
        //Today=היום   Week=השבוע  Month=החודש   AllTheTime=כל הזמנים   Year= השנה
        public List<LoginHistory> GetLoginHistoryFilteringByDate(string RequestDate)
        {
            if (RequestDate == "Today")
            {
                var LoginHistoryObj = m_db.LoginHistory.Where(l => l.DateAdded == DateTime.Today).Select(ee => new LoginHistory()
                {
                    Id = ee.Id,
                    UserId = ee.UserId,
                    DateAdded = ee.DateAdded,
                    User = ee.User
                }).ToList();
                LoginHistoryObj.ForEach(l => l.User.Password = null);
                return LoginHistoryObj;
            }
            //else if (RequestDate == "Week")
            //{
            //    return m_db.User.Where(u => u.RegisterDate.w == DateTime.Today).Count();
            //}
            else if (RequestDate == "Month")
            {
                    var LoginHistoryObj = m_db.LoginHistory.Where(l => l.DateAdded.Month == DateTime.Today.Month).Select(ee => new LoginHistory()
                    {
                        Id = ee.Id,
                        UserId = ee.UserId,
                        DateAdded = ee.DateAdded,
                        User = ee.User
                    }).ToList();
                    LoginHistoryObj.ForEach(l => l.User.Password = null);
                    return LoginHistoryObj;
                
            }
            else if (RequestDate == "Year")
            {
                var LoginHistoryObj = m_db.LoginHistory.Where(l => l.DateAdded.Year == DateTime.Today.Year).Select(ee => new LoginHistory()
                {
                    Id = ee.Id,
                    UserId = ee.UserId,
                    DateAdded = ee.DateAdded,
                    User = ee.User
                }).ToList();
                LoginHistoryObj.ForEach(l => l.User.Password = null);
                return LoginHistoryObj;
            }
            //else AllTheTime 
            return GetAllLoginHistory();
        }

    }
}
