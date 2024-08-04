using CookBook.Data;
using CookBook.Data.DTO;
using CookBook.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;

namespace CookBook.Services
{
    public class UserService
    {
        //תקציר
        ////////
        //בנאי
        // הזדאות
        //קבלת תוקןJWT
        // JWT קבלת אוביקט משתמש על פי
        // JWT קבלת מזהה משתמש על פי
        // הוספת משתמש
        // קבלת משתמש לפי מזהה
        // קבלת משתמש לשימוש פנימי
        // קבלת כל המשתמשים
        // קבלת כל המנהלים מוגבל למנהל
        // קבלת משתמש על פי מייל
        // קבלת רשימת משתמשים שהם לא מנהלים מוגבל למנהל
        // JWT עדכון משתמש על פי
        // JWT מחיקת משתמש על פי 
        // מחיקת משתמש למנהל 
        // מחיקת כל המועדפים של משתמש בעת מחיקת משתמש 
        // עדכון תפקיד למשתמש מוגבל למנהל 
        // הצפנת סיסמה
        // האם קיים מייל כזה
        //שליחת סיסמה חדשה למיל של המשתשמש ושמירה במאגר הנותנים שלו
        //יצירת סיסמה חדשה מוצפנת
        //עדכון משתמש מחובר או לא

        //לעשות
        //קבלת מספר כמה משתמשים נרשמו אם אפשרות סינון של היום השבוע החודש והשנה וכל הזמן


        private readonly CookBookDBContext m_db;
        private readonly JwtService _jwtService;

        //בנאי
        public UserService(CookBookDBContext db,JwtService jwt)
        {
            m_db = db;
            _jwtService = jwt;
        }
        // הזדאות
        public User GetUserForLogin(string email, string password)
        {
            string passwordAfterMD5 = GetMD5(password);
            return m_db.User.Where(user => user.Mail.ToLower() == email.ToLower() && user.Password == passwordAfterMD5).FirstOrDefault();
        }

        //קבלת תוקןJWT
        public string GetToken(string id, string role)
        {
            return _jwtService.GenerateToken(id, role);
        }

        // JWT קבלת אוביקט משתמש על פי
        public User GetUserByJWT()
        {
            return m_db.User.Where(user => user.Id == int.Parse(_jwtService.GetTokenClaims())).FirstOrDefault();
        }

        // JWT קבלת מזהה משתמש על פי
        public int GetUserIdByJWT()
        {
            return int.Parse(_jwtService.GetTokenClaims());
        }

        // הוספת משתמש
        public bool AddUser(UserDTO UserFromUserToAdd)
        {
            if (GetHaveUser(UserFromUserToAdd.Mail))
            {
                return false;
            }
            User UserToAdd=new User();
            UserToAdd.Mail = UserFromUserToAdd.Mail;
            UserToAdd.FirstName = UserFromUserToAdd.FirstName;
            UserToAdd.LastName = UserFromUserToAdd.LastName;
            UserToAdd.Password =GetMD5(UserFromUserToAdd.Password);
            UserToAdd.Role = "Classic";
            UserToAdd.RegisterDate = System.DateTime.Now;
            UserToAdd.IsActive = true;
            UserToAdd.Birthdate = UserFromUserToAdd.Birthdate;
            UserToAdd.Phone = UserFromUserToAdd.Phone;
            m_db.User.Add(UserToAdd);
            int c = m_db.SaveChanges();
            return c > 0;
        }

        // קבלת משתמש לפי מזהה
        public User GetUserById(int id)
        {
            User user = m_db.User.Where(user => user.Id == id).FirstOrDefault();
            user.Password = null;
            user.Phone = null;
            user.Mail = null;
            return user;
        }

        // קבלת משתמש לשימוש פנימי
        public User GetUserByIdFromDb(int id)
        {
            User user = m_db.User.Where(user => user.Id == id).FirstOrDefault();
            return user;
        }

        // קבלת כל המשתמשים
        public List<User>GetAllUser()
        {
            List<User> LUser = m_db.User.ToList();
            LUser.ForEach(user => user.Password = null) ;
            return LUser;
        }

        // קבלת כל המנהלים מוגבל למנהל
        public List<User>GetAllAdmin()
        {
            List<User> LUser = m_db.User.Where(u => u.Role == "Admin").ToList();
            LUser.ForEach(u=>u.Password = null) ;
            return LUser;
        }

        // קבלת משתמש על פי מייל
        public User GetUserByMail(string mail)
        {
            User user = m_db.User.Where(u => u.Mail == mail).FirstOrDefault();
            user.Password = null;
            return user;
        }

        // קבלת רשימת משתמשים שהם לא מנהלים מוגבל למנהל
        public List<User> GetAllUserNoAdmin()
        {
            List<User> LUser = m_db.User.Where(u => u.Role != "Admin").ToList();
            LUser.ForEach(u => u.Password = null);
            return LUser;
        }

        // JWT עדכון משתמש על פי
        public ResponseDTO UpdateUserByJWT(UserDTO UserFromUserToUpdate)
        {
            User UserToUpdateFromDB = GetUserByJWT();
            if (UserToUpdateFromDB == null || UserToUpdateFromDB.Id != UserFromUserToUpdate.Id)
            {
                return new ResponseDTO() { Status = Data.DTO.StatusCode.Error, StatusText = "User Bot Found in DB" };
            }
            UserToUpdateFromDB.FirstName = UserFromUserToUpdate.FirstName;
            UserToUpdateFromDB.LastName = UserFromUserToUpdate.LastName;
            if (UserFromUserToUpdate.Password != UserToUpdateFromDB.Password && UserFromUserToUpdate.Password != null)
            {
                UserToUpdateFromDB.Password = GetMD5(UserFromUserToUpdate.Password);
            }
            UserToUpdateFromDB.Birthdate = UserFromUserToUpdate.Birthdate;
            UserToUpdateFromDB.Phone = UserFromUserToUpdate.Phone;
            int c = m_db.SaveChanges();
            return c > 0 ?
                 new ResponseDTO() { Status = Data.DTO.StatusCode.Success }
                 :
                 new ResponseDTO() { Status = Data.DTO.StatusCode.Error, StatusText = "לא הצלחנו לעדכן את המשתמש" };
        }

        // JWT מחיקת משתמש על פי 
        public ResponseDTO DeleteUserByJWT()
        {
            User UserToDelete = GetUserByJWT();
            if(UserToDelete == null)
            {
                return new ResponseDTO() { Status=Data.DTO.StatusCode.Error,StatusText="המשתמש לא נמצא בבסיס נתונים"};
            }
            if(UserToDelete.Role == "Classic")
            {
                if (DeleteAllFavorite(UserToDelete.Id).Status == Data.DTO.StatusCode.Error)
                {
                    return new ResponseDTO() { Status = Data.DTO.StatusCode.Error, StatusText = "לא הצלחנו למחוק את המועדפים" };
                }
            }
            m_db.User.Remove(UserToDelete);
            int c=m_db.SaveChanges();
            return c > 0 ?
                 new ResponseDTO() { Status = Data.DTO.StatusCode.Success }
                 :
                 new ResponseDTO() { Status = Data.DTO.StatusCode.Error, StatusText = "לא הצלחנו למחוק את המשתמש" };
        }

        // מחיקת משתמש למנהל 
        public ResponseDTO DeleteUserForAdmin(int userid)
        {
            User UserToDelete = GetUserByIdFromDb(userid);
            if(UserToDelete.Id!=userid|| UserToDelete==null)
            {
                return new ResponseDTO() { Status = Data.DTO.StatusCode.Error, StatusText = $" לא נמצא בבסיס נתונים {userid} המשתמש בעל מזהה" };
            }
            if (UserToDelete.Role == "Classic")
            {
                if (DeleteAllFavorite(UserToDelete.Id).Status == Data.DTO.StatusCode.Error)
                {
                    return new ResponseDTO() { Status = Data.DTO.StatusCode.Error, StatusText = "לא הצלחנו למחוק את המועדפים" };
                }
            }
            m_db.User.Remove(UserToDelete);
            int c = m_db.SaveChanges();
            return c > 0 ?
                 new ResponseDTO() { Status = Data.DTO.StatusCode.Success }
                 :
                 new ResponseDTO() { Status = Data.DTO.StatusCode.Error, StatusText = "לא הצלחנו למחוק את המשתמש" };
        }

        // מחיקת כל המועדפים של משתמש בעת מחיקת משתמש
        private ResponseDTO DeleteAllFavorite(int userid)
        {
            List<Favorite>FList=m_db.Favorite.Where(f=>f.UserId==userid).ToList();
            if(FList.Count == 0)
            {
                return new ResponseDTO() { Status = Data.DTO.StatusCode.Success };
            }
            FList.ForEach(f => m_db.Favorite.Remove(f));
            int c = m_db.SaveChanges();
            return c > 0 ?
             new ResponseDTO() { Status = Data.DTO.StatusCode.Success }
             :
             new ResponseDTO() { Status = Data.DTO.StatusCode.Error };
        }

        // עדכון תפקיד למשתמש מוגבל למנהל
        public ResponseDTO ChangeUserRole(ChangeUserRoleDTO ChangeUserRoleObj)
        {
            User UserToChangeRoleFromDB = GetUserByIdFromDb(ChangeUserRoleObj.UserId);
            if (UserToChangeRoleFromDB == null || ChangeUserRoleObj.UserId != UserToChangeRoleFromDB.Id)
            {
                return new ResponseDTO() { Status = Data.DTO.StatusCode.Error, StatusText = $"{ChangeUserRoleObj.UserId} לא נמצא משתמש עם  מזהה זה " };
            }
            UserToChangeRoleFromDB.Role = ChangeUserRoleObj.Role;
            int c = m_db.SaveChanges();
            return c > 0 ?
               new ResponseDTO() { Status = Data.DTO.StatusCode.Success }
               :
               new ResponseDTO() { Status = Data.DTO.StatusCode.Error, StatusText = "לא הצלחנו לעדכן את המשתמש" };
        }

        // הצפנת סיסמה
        private string GetMD5(string input)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.ASCII.GetBytes(input));
                var strResult = BitConverter.ToString(result);
                return strResult.Replace("-", "");
            }
        }

        // האם קיים מייל כזה
        public bool GetHaveUser(string Mail)
        {
            //גדול זה אומר אמת יחזיר אמת = שקיים משתמש 
            //כל דבר אחרת יחזיר שקר = לא קיים משתמש
            return m_db.User.Where(user => user.Mail == Mail).Count() > 0 ? true : false;
        }

        /// <summary>
        /// לעשות אימל לאתר החדש ולהכניס את הנתונים
        /// </summary>
        /// <param name="mailto"></param>
        /// <returns></returns>
        //שליחת סיסמה חדשה למיל של המשתשמש ושמירה במאגר הנותנים שלו
        public bool ForgotPassword(string mailto)
        {
            User user = GetUserByMail(mailto);
            string newPassword = NewPassword();
            if (user == null || user.Mail != mailto)
            {
                return false;
            }
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("fromthefarmerisrael@gmail.com");
                message.To.Add(new MailAddress(mailto));
                message.Subject = "בקשתך לשינוי סיסמה";
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = "<table><tr><th> Cook Book Israel </th></tr><tr><td> שלום רב סיסמתך החדשה היא:</td></tr><tr><td>" + newPassword + "</td></tr> <tr><td> חברת Cook Book Israel </td></tr></table>";

                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("fromthefarmerisrael@gmail.com", "Tt123456@");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception)
            {
                return false;
            }
            user.Password = GetMD5(newPassword);
            int c = m_db.SaveChanges();
            return c > 0;
        }

        //יצירת סיסמה חדשה מוצפנת
        private string NewPassword()
        {
            string password = "";
            char[] chars = "$%#@!?;:+-^&*abcdefghijklmnopqrstuvwxyz0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            Random r = new Random();
            int LenghtPassword = r.Next(8, 13);//8-12//בגלל ש4 אני שם בכוח
            password += chars[r.Next(0, 13)];//0-12
            password += chars[r.Next(13, 39)];//13-38
            for (int i = 0; i < LenghtPassword; i++)
            {
                int RandomNumber = r.Next(0, 4);
                if (RandomNumber == 0)
                {
                    password += chars[r.Next(0, 13)];//0-12
                }
                else if (RandomNumber == 1)
                {
                    password += chars[r.Next(13, 39)];//13-38
                }
                else if (RandomNumber == 2)
                {
                    password += chars[r.Next(39, 49)];//38-48
                }
                else
                {
                    password += chars[r.Next(49, chars.Length)];//48-chars.Lenght
                }
            }
            password += chars[r.Next(39, 49)];//38-48
            password += chars[r.Next(49, chars.Length)];//48-chars.Lenght      
            return password;
        }

        /// <summary>
        /// לראות איך להשמיש את זה בצורה טובה
        /// </summary>
        /// <param name="Active"></param>
        /// <returns></returns>
        //עדכון משתמש מחובר או לא
        public ResponseDTO UpdateIsActiveForUser(bool Active)
        {
            User UserFromDBToUpdate = GetUserByJWT();
            UserFromDBToUpdate.IsActive = Active;
            int c = m_db.SaveChanges();
            return c > 0 ?
                new ResponseDTO() { Status = Data.DTO.StatusCode.Success }
                :
                new ResponseDTO() { Status = Data.DTO.StatusCode.Error, StatusText = "לא הצלחנו לשמור את השינוים" };
        }


        /// <summary>
        /// לבדוק
        /// ולראות בעתיד איך להוסיף גם את השבוע 
        /// </summary>
        /// <param name="RequestDate"></param>
        /// <returns></returns>
        //קבלת מספר כמה משתמשים נרשמו אם אפשרות סינון של היום השבוע החודש והשנה וכל הזמן
        //Today=היום   Week=השבוע  Month=החודש   AllTheTime=כל הזמנים   Year= השנה
        public List<User> GetCountRegisterDate(string RequestDate)
        {
            List<User> LUser= new List<User>();
            if (RequestDate == "Today")
            {
                LUser = m_db.User.Where(u => u.RegisterDate == DateTime.Today).ToList();//ok
                LUser.ForEach(u => u.Password = null);
                return LUser;
            }
            //else if (RequestDate == "Week")
            //{
            //    return m_db.User.Where(u => u.RegisterDate.w == DateTime.Today).Count();
            //}
            else if (RequestDate == "Month") 
            {
                LUser = m_db.User.Where(u => u.RegisterDate.Month == DateTime.Today.Month).ToList();//ok
                LUser.ForEach(u => u.Password = null);
                return LUser;
            }
            else if(RequestDate == "Year")
            {
                LUser = m_db.User.Where(u => u.RegisterDate.Year == DateTime.Today.Year).ToList();// ok
                LUser.ForEach(u => u.Password = null);
                return LUser;
            }
            //else AllTheTime 
            LUser = m_db.User.ToList().ToList();//ok
            LUser.ForEach(u => u.Password = null);
            return LUser;
        }
    }
}
