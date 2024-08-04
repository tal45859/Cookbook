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
    [Authorize(Roles = "Admin")]
    public class ReportingController : ControllerBase
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

        private readonly ReportingService _service;

        //בנאי
        public ReportingController(ReportingService service)
        {
            _service = service;
        }

        //קבלת דיווח לפי מזהה
        [HttpGet,Route("GetReportingById/{ReportingId}")]
        public ActionResult GetReportingById(int ReportingId)
        {
            Reporting ReportingForClient = _service.GetReportingById(ReportingId);
            if(ReportingForClient != null)
            {
                return Ok(ReportingForClient);
            }
            return BadRequest("לא הצלחנו למצוא את הדיווח המבוקש");
        }

        //הוספת דיווח
        [HttpPost,Route("AddReporting"),AllowAnonymous]
        public ActionResult AddReporting([FromBody] ReportingDTO ReportingToAdd)
        {
            bool IsCreated = _service.AddReporting(ReportingToAdd);
            if(IsCreated)
            {
                return Created("", null);
            }
            return BadRequest("לא הצלנו לשמור את הדיווח");
        }

        //מחיקת דיווח
        [HttpDelete,Route("DeleteReportingById/{ReportingId}")]
        public ActionResult DeleteReportingById(int ReportingId)
        {
            ResponseDTO Response =_service.DeleteReportingById(ReportingId);
            if(Response.Status == Data.DTO.StatusCode.Error)
            {
                return BadRequest(Response.StatusText);
            }
            return Ok();
        }

        //שינוי מצב דיווח לפתור והוספת הערת סיבת סיום
        [HttpPut,Route("UpdateReporting")]
        public ActionResult UpdateReporting([FromBody] ReportingDTO ReportingToUpdate)
        {
            ResponseDTO Response = _service.UpdateReporting(ReportingToUpdate);
            if (Response.Status == Data.DTO.StatusCode.Error)
            {
                return BadRequest(Response.StatusText);
            }
            return Ok();
        }

        //קבלת רשימת דיווחים לא פתורים
        [HttpGet,Route("GetAllReportingActive")]
        public ActionResult GetAllReportingActive()
        {
            List<Reporting> LReporting = _service.GetAllReportingActive();
            if(LReporting != null)
            {
                return Ok(LReporting);
            }
            return BadRequest("לא נמצאו תוצאות לקרטריון החיפוש");
        }

        //קבלת רשימת דיווחים פתורים
        [HttpGet, Route("GetAllReportingNoActive")]
        public ActionResult GetAllReportingNoActive()
        {
            List<Reporting> LReporting = _service.GetAllReportingNoActive();
            if (LReporting != null)
            {
                return Ok(LReporting);
            }
            return BadRequest("לא נמצאו תוצאות לקרטריון החיפוש");
        }

        //קבלת רשימת כל הדיווחים
        [HttpGet, Route("GetAllReporting")]
        public ActionResult GetAllReporting()
        {
            List<Reporting> LReporting = _service.GetAllReporting();
            if (LReporting != null)
            {
                return Ok(LReporting);
            }
            return BadRequest("לא נמצאו תוצאות לקרטריון החיפוש");
        }

        //קבלת רשימת דיווחים לפי מזהה מתכון
        [HttpGet, Route("GetAllReportingByRecipeId/{RecipeId}")]
        public ActionResult GetAllReportingByRecipeId(int RecipeId)
        {
            List<Reporting> LReporting = _service.GetAllReportingByRecipeId(RecipeId);
            if (LReporting != null)
            {
                return Ok(LReporting);
            }
            return BadRequest("לא נמצאו תוצאות לקרטריון החיפוש");
        }
    }
}
