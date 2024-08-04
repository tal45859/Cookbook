using CookBook.Data.DTO;
using CookBook.Data.Entities;
using CookBook.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CookBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Classic")]
    public class UserController : ControllerBase
    {
        //תקציר
        ////////////
        //בנאי
        //הזדאות וקבלת תוקן
        // JWT קבלת אוביקט משתמש על פי
        // JWT קבלת מזהה משתמש על פי
        // הוספת משתמש
        // קבלת משתמש לפי מזהה
        // קבלת כל המשתמשים מוגבל למהל
        // קבלת כל המנהלים מוגבל למנהל
        // קבלת רשימת משתמשים שהם לא מנהלים מוגבל למנהל
        // קבלת משתמש על פי מייל מוגבל למנהל
        // האם קיים מייל כזה
        // JWT עדכון משתמש על פי
        // JWT מחיקת משתמש על פי 
        // מחיקת משתמש למנהל 
        // עדכון תפקיד למשתמש מוגבל למנהל 
        //שליחת סיסמה חדשה למיל של המשתשמש ושמירה במאגר הנותנים שלו       
        //עדכון משתמש מחובר או לא
        //קבלת מספר כמה משתמשים נרשמו אם אפשרות סינון של היום השבוע החודש והשנה וכל הזמן

        private readonly UserService _service;

        //בנאי
        public UserController(UserService service)
        {
            _service = service;
        }

        //הזדאות וקבלת תוקן
        [HttpPost, Route("{auth}"),AllowAnonymous]
        public IActionResult Auth([FromBody] AuthRequestDTO request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("יש להזין אימל וסיסמה");
            }
            User UserFoundFromDb = _service.GetUserForLogin(request.Email, request.Password);
            if (UserFoundFromDb != null)
            {
               
                string token = _service.GetToken(UserFoundFromDb.Id.ToString(), UserFoundFromDb.Role);
                return Ok(token);
            }
            return Unauthorized("משתמש לא מזוהה במערכת");
        }

        // JWT קבלת אוביקט משתמש על פי
        
        [HttpGet,Route("GetUserByToken"),Authorize(Roles = "Admin,Classic")]
        public ActionResult GetUserByJWT()
        {
            User UserForClient = _service.GetUserByJWT();
            if(UserForClient!=null)
            {
                return Ok(UserForClient);
            }
            return BadRequest("לא הצלחנו למצוא את המשתמש");
        }

        // JWT קבלת מזהה משתמש על פי
        [HttpGet, Route("GetUserIdByToken"), Authorize(Roles = "Admin,Classic")]
        public ActionResult GetUserIdByJWT()
        {
            int UserId = _service.GetUserIdByJWT();
            if (UserId > 0)
            {
                return Ok(UserId);
            }
            return BadRequest("לא הצלחנו למצוא את המשתמש");
        }

        // הוספת משתמש
        [HttpPost,Route("AddUser"),AllowAnonymous]
        public ActionResult AddUser([FromBody]UserDTO UserToAdd)
        {
            bool IsCreated = _service.AddUser(UserToAdd);
            if(IsCreated)
            {
                return Created("", null);
            }
            return BadRequest("לא הצלחנו להוסיף את המשתמש");
        }

        // קבלת משתמש לפי מזהה 
        [HttpGet, Route("GetUserById/{Id}"), AllowAnonymous]
        public ActionResult GetUserById(int Id)
        {
            User UserForClient = _service.GetUserById(Id);
            if(UserForClient != null)
            {
                return Ok(UserForClient);
            }
            return BadRequest("לא הצלחנו למצוא את המשתמש");
        }

        // קבלת כל המשתמשים מוגבל למהל
        [HttpGet, Route("GetAllUser"), Authorize(Roles = "Admin")]
        public ActionResult GetAllUser()
        {
            List<User> LUser = _service.GetAllUser();
            if (LUser != null)
            {
                return Ok(LUser);
            }
            return BadRequest("לא מצאנו משתמשים אשר עולים לסוג חיפוש זה");
        }

        // קבלת כל המנהלים מוגבל למנהל
        [HttpGet, Route("GetAllAdmin"), Authorize(Roles = "Admin")]
        public ActionResult GetAllAdmin()
        {
            List<User> LUser = _service.GetAllAdmin();
            if (LUser != null)
            {
                return Ok(LUser);
            }
            return BadRequest("לא מצאנו משתמשים אשר עולים לסוג חיפוש זה");
        }

        // קבלת רשימת משתמשים שהם לא מנהלים מוגבל למנהל
        [HttpGet,Route("GetAllUserNoAdmin"),Authorize(Roles ="Admin")]
        public ActionResult GetAllUserNoAdmin()
        {
            List<User>LUser=_service.GetAllUserNoAdmin();
            if(LUser!=null)
            {
                return Ok(LUser);
            }
            return BadRequest("לא מצאנו משתמשים אשר עולים לסוג חיפושש זה");
        }

        // קבלת משתמש על פי מייל מוגבל למנהל
        [HttpGet, Route("GetUserByMail/{Mail}"), Authorize(Roles = "Admin")]
        public ActionResult GetUserByMail(string Mail)
        {
            User UserForClient = _service.GetUserByMail(Mail);
            if (UserForClient != null)
            {
                return Ok(UserForClient);
            }
            return BadRequest("לא הצלחנו למצוא את המשתמש");
        }

        // האם קיים מייל כזה
         [HttpGet, Route("GetHaveUser/{Mail}"),AllowAnonymous]
        public ActionResult GetHaveUser(string Mail)
        {
            bool HaveUser =_service.GetHaveUser(Mail);
            if(HaveUser)
            {
                BadRequest(HaveUser);//המייל קיים במערכת
            }
            return Ok(HaveUser);//המייל לא קיים במערכת
        }

        // JWT עדכון משתמש על פי
        [HttpPut, Route("UpdateUserByToken")]
        public ActionResult UpdateUserByJWT([FromBody]UserDTO UserToUpdate)
        {
            ResponseDTO ResponseForUser = _service.UpdateUserByJWT(UserToUpdate);
            if(ResponseForUser.Status == Data.DTO.StatusCode.Error)
            {
                return BadRequest(ResponseForUser.StatusText);
            }
            return Ok();
        }

        // JWT מחיקת משתמש על פי 
        [HttpDelete, Route("DeleteUserByToken")]
        public ActionResult DeleteUserByJWT()
        {
            ResponseDTO ResponseForUser = _service.DeleteUserByJWT();
            if (ResponseForUser.Status == Data.DTO.StatusCode.Error)
            {
                return BadRequest(ResponseForUser.StatusText);
            }
            return Ok();
        }

        // מחיקת משתמש למנהל 
        [HttpDelete, Route("DeleteUserByIdForAdmin/{Id}"), Authorize(Roles = "Admin")]
        public ActionResult DeleteUserByIdForAdmin(int Id)
        {
            ResponseDTO ResponseForUser = _service.DeleteUserForAdmin(Id);
            if (ResponseForUser.Status == Data.DTO.StatusCode.Error)
            {
                return BadRequest(ResponseForUser.StatusText);
            }
            return Ok();
        }

        // עדכון תפקיד למשתמש מוגבל למנהל 
        [HttpPut, Route("ChangeUserRoleForAdmin"), Authorize(Roles = "Admin")]
        public ActionResult ChangeUserRoleForAdmin([FromBody] ChangeUserRoleDTO ChangeUserRoleObj)
        {
            ResponseDTO ResponseForUser = _service.ChangeUserRole(ChangeUserRoleObj);
            if (ResponseForUser.Status == Data.DTO.StatusCode.Error)
            {
                return BadRequest(ResponseForUser.StatusText);
            }
            return Ok();
        }

        //שליחת סיסמה חדשה למיל של המשתשמש ושמירה במאגר הנותנים שלו
       
        [HttpGet,Route("ForgotPassword/{Mail}"), AllowAnonymous]
        public ActionResult ForgotPassword(string Mail)
        {
            bool IsHaveNewPassword = _service.ForgotPassword(Mail);
            if (IsHaveNewPassword)
            {
                return Ok(IsHaveNewPassword);
            }
            return BadRequest(IsHaveNewPassword);
        }

        //עדכון משתמש מחובר או לא
        [HttpPut, Route("UpdateIsActiveForUser/{Active}")]
        public ActionResult UpdateIsActiveForUser(bool Active)
        {
            ResponseDTO Response = _service.UpdateIsActiveForUser(Active);
            if(Response.Status == Data.DTO.StatusCode.Error)
            {
                return BadRequest(Response.StatusText);
            }
            return Ok();
        }

        //קבלת מספר כמה משתמשים נרשמו אם אפשרות סינון של היום השבוע החודש והשנה וכל הזמן
        //שבוע לא עובד כרגע
        //Today=היום   Week=השבוע  Month=החודש   AllTheTime=כל הזמנים   Year= השנה
        [HttpGet, Route("GetCountRegisterDate/{RequestDate}"), Authorize(Roles = "Admin")]
        public ActionResult GetCountRegisterDate(string RequestDate)
        {
            List<User> LUser = _service.GetCountRegisterDate(RequestDate);
            return Ok(LUser);
        }

    }
}
