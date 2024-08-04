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
    public class SubcategoryController : ControllerBase
    {
        //תקציר
        ////////////
        //בנאי
        //קבלת תת קטגוריה לפי מזהה תת קטגוריה
        //קבלת רשימת תתי קטגוריה עם אוביקט קטגוריה
        //קבלת אוביקט תת קטגוריה לפי מזהה קטגוריה
        //קבלת רשימת תתי קטגוריה של קטגוריה לפי מזהה קטגוריה
        // הוספת תת קטגוריה
        // עדכון תת קטגוריה
        // מחיקת תת קטגוריה

        private readonly SubcategoryService _service;

        //בנאי
        public SubcategoryController(SubcategoryService service)
        {
            _service = service;
        }

        //קבלת תת קטגוריה לפי מזהה תת קטגוריה
        [HttpGet,Route("GetSubcategoryById/{SubcategoryId}"), AllowAnonymous]
        public ActionResult GetSubcategoryById(int SubcategoryId)
        {
            Subcategory SubcategoryForClient =_service.GetSubcategoryById(SubcategoryId);
            if(SubcategoryForClient != null)
            {
                return Ok(SubcategoryForClient);
            }
            return BadRequest("התת קטגוריה לא נמצאה");
        }

        //קבלת רשימת תתי קטגוריה עם אוביקט קטגוריה
        [HttpGet, Route("GetAllSubcategory"), AllowAnonymous]
        public ActionResult GetAllSubcategory()
        {
            List<Subcategory> LSubcategory = _service.GetAllSubcategory();
            if(LSubcategory != null)
            {
                return Ok(LSubcategory);
            }
            return BadRequest("לא הצלחנו למצוא את החיפוש המבוקש");
        }

        //קבלת אוביקט תת קטגוריה לפי מזהה קטגוריה
        [HttpGet, Route("GetSubcategoryByCategoryId/{CategoryId}"), AllowAnonymous]
        public ActionResult GetSubcategoryByCategoryId(int CategoryId)
        {
            Subcategory SubcategoryForClient = _service.GetSubcategoryByCategoryId(CategoryId);
            if (SubcategoryForClient != null)
            {
                return Ok(SubcategoryForClient);
            }
            return BadRequest("התת קטגוריה לא נמצאה");
        }

        //קבלת רשימת תתי קטגוריה של קטגוריה לפי מזהה קטגוריה
        [HttpGet, Route("GetAllSubcategoryByCategoryId/{CategoryId}"),AllowAnonymous]
        public ActionResult GetAllSubcategoryByCategoryId(int CategoryId)
        {
            List<Subcategory> LSubcategory = _service.GetAllSubcategoryByCategoryId(CategoryId);
            if (LSubcategory != null)
            {
                return Ok(LSubcategory);
            }
            return BadRequest("התת קטגוריה לא נמצאה");
        }

        // הוספת תת קטגוריה
        [HttpPost, Route("AddSubcategory")]
        public ActionResult AddSubcategory([FromBody] SubcategoryDTO SubcategoryToAdd)
        {
            bool IsCreated = _service.AddSubcategory(SubcategoryToAdd);
            if(IsCreated)
            {
                return Created("", null);
            }
            return BadRequest("לא הצלחנו להוסיף את התת קטגוריה");
        }

        // עדכון תת קטגוריה
        [HttpPut, Route("UpdateSubcategory")]
        public ActionResult UpdateSubcategory([FromBody] SubcategoryDTO SubcategoryToUpdate)
        {
            ResponseDTO Response = _service.UpdateSubcategory(SubcategoryToUpdate);
            if(Response.Status ==Data.DTO.StatusCode.Error)
            {
                return BadRequest(Response.StatusText);
            }
            return Ok();
        }

        // מחיקת תת קטגוריה
        [HttpDelete, Route("DeleteSubcategory/{SubcategoryId}")]
        public ActionResult DeleteSubcategory(int SubcategoryId)
        {
            ResponseDTO Response = _service.DeleteSubcategory(SubcategoryId);
            if (Response.Status == Data.DTO.StatusCode.Error)
            {
                return BadRequest(Response.StatusText);
            }
            return Ok();
        }
    }
}
