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
    public class LoginHistoryController : ControllerBase
    {
        //תקציר
        //////////
        //בנאי
        //הוספת היסטוריה
        //מחיקת היסטוריה לפי מזהה היסטוריה
        //קבלת היסטוריה לפי מזהה היסטוריה עם אוביקט
        //קבלת רשימת היסטוריה של כל המשתמשים עם אוביקטים
        //קבלת רשימת היסטוריה של משתמש בודד לפי מזהה משתמש
        //קבלת רשימות של משתמשים שהתחברו אם אפשרות סינון של היום השבוע החודש והשנה וכל הזמן

        private readonly LoginHistoryService _service;

        //בנאי
        public LoginHistoryController(LoginHistoryService service)
        {
            _service = service;
        }

        //הוספת היסטוריה
        [HttpPost,Route("AddLoginHistory")]
        public ActionResult AddLoginHistory()
        {
            bool IsCreated = _service.AddLoginHistory();
            if(IsCreated)
            {
                return Created("", null);
            }
            return BadRequest("לא הצלחנו לשמור את ההתחברות");
        }


        //מחיקת היסטוריה לפי מזהה היסטוריה
        [HttpDelete,Route("DeleteLoginHistoryById/{LoginHistoryId}"),Authorize(Roles ="Admin")]
        public ActionResult DeleteLoginHistoryById(int LoginHistoryId)
        {
            ResponseDTO Response =_service.DeleteLoginHistoryById(LoginHistoryId);
            if(Response.Status == Data.DTO.StatusCode.Error)
            {
                return BadRequest(Response.StatusText);
            }
            return Ok();
        }

        //קבלת היסטוריה לפי מזהה היסטוריה עם אוביקט
        [HttpGet,Route("GetLoginHistoryById/{LoginHistoryId}"), Authorize(Roles = "Admin")]
        public ActionResult GetLoginHistoryById(int LoginHistoryId)
        {
            LoginHistory LoginHistoryForClient = _service.GetLoginHistoryById(LoginHistoryId);
            if (LoginHistoryForClient != null)
            {
                return Ok(LoginHistoryForClient);
            }
            return BadRequest("לא הצלחנו למצוא את ההיסטוריה המבוקשת");
        }

        //קבלת רשימת היסטוריה של כל המשתמשים עם אוביקטים
        [HttpGet, Route("GetAllLoginHistory"), Authorize(Roles = "Admin")]
        public ActionResult GetAllLoginHistory()
        {
            List<LoginHistory> LLoginHistoryForClient = _service.GetAllLoginHistory();
            if (LLoginHistoryForClient != null)
            {
                return Ok(LLoginHistoryForClient);
            }
            return BadRequest("לא הצלחנו למצוא את רשימת ההיסטוריה המבוקשת");
        }

        //קבלת רשימת היסטוריה של משתמש בודד לפי מזהה משתמש
        [HttpGet, Route("GetAllLoginHistoryById/{LoginHistoryId}"), Authorize(Roles = "Admin")]
        public ActionResult GetAllLoginHistoryById(int LoginHistoryId)
        {
            List<LoginHistory> LLoginHistoryForClient = _service.GetAllLoginHistoryById(LoginHistoryId);
            if (LLoginHistoryForClient != null)
            {
                return Ok(LLoginHistoryForClient);
            }
            return BadRequest("לא הצלחנו למצוא את רשימת ההיסטוריה המבוקשת");
        }

        //קבלת רשימות של משתמשים שהתחברו אם אפשרות סינון של היום השבוע החודש והשנה וכל הזמן
        //כרגע שבוע לא עובד 
        //Today=היום   Week=השבוע  Month=החודש   AllTheTime=כל הזמנים   Year= השנה
        [HttpGet, Route("GetLoginHistoryFilteringByDate/{RequestDate}"), Authorize(Roles = "Admin")]
        public ActionResult GetLoginHistoryFilteringByDate(string RequestDate)
        {
            List<LoginHistory> LLoginHistoryForClient = _service.GetLoginHistoryFilteringByDate(RequestDate);
            if (LLoginHistoryForClient != null)
            {
                return Ok(LLoginHistoryForClient);
            }
            return BadRequest("לא הצלחנו למצוא את רשימת ההיסטוריה המבוקשת");
        }
    }
}
