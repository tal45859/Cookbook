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
    public class CategoryController : ControllerBase
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

        private readonly CategoryService _service;

        //בנאי
        public CategoryController(CategoryService service)
        {
            _service = service;
        }

        // קבלת קטגוריה לפי מזהה
        [HttpGet,Route("GetCategoryById/{CategoryId}")]
        public ActionResult GetCategoryById(int CategoryId)
        {
            Category CategoryForClient= _service.GetCategoryById(CategoryId);
            if (CategoryForClient != null)
            {
                return Ok(CategoryForClient);
            }
            return BadRequest("לא הצלחנו למצוא את הקטגוריה המבוקשת");
        }

        // קבלת קטגוריה לפי שם קטגוריה
        [HttpGet, Route("GetCategoryByCategoryName/{CategoryName}")]
        public ActionResult GetCategoryByCategoryName(string CategoryName)
        {
            Category CategoryForClient = _service.GetCategoryByCategoryName(CategoryName);
            if (CategoryForClient != null)
            {
                return Ok(CategoryForClient);
            }
            return BadRequest("לא הצלחנו למצוא את הקטגוריה המבוקשת");
        }

        // קבלת כל הקטגוריות
        [HttpGet, Route("GetAllCategory")]
        public ActionResult GetAllCategory()
        {
            List<Category> LCategoryForClient = _service.GetAllCategory();
            if (LCategoryForClient != null)
            {
                return Ok(LCategoryForClient);
            }
            return BadRequest("לא הצלחנו למצוא את החיפוש המבוקש");
        }

        // יצירת קטגוריה
        [HttpPost,Route("AddCategory"), Authorize(Roles ="Admin")]
        public ActionResult AddCategory([FromBody] CategoryDTO CategoryToAdd)
        {
            bool IsCreated = _service.AddCategory(CategoryToAdd);
            if(IsCreated)
            {
                return Created("", null);
            }
            return BadRequest("לא הצלחנו לשמור את הקטגוריה");
        }

        // מחיקת קטגוריה
        [HttpDelete, Route("DeleteCategory/{CategoryId}"), Authorize(Roles = "Admin")]
        public ActionResult DeleteCategory(int CategoryId)
        {
            ResponseDTO Response = _service.DeleteCategory(CategoryId);
            if(Response.Status == Data.DTO.StatusCode.Error)
            {
                return BadRequest(Response.StatusText);
            }
            return Ok();
        }

        // עדכון קטגוריה
        [HttpPut, Route("UpdateCategory"), Authorize(Roles = "Admin")]
        public ActionResult UpdateCategory([FromBody] CategoryDTO CategoryToUpdate)
        {
            ResponseDTO Response = _service.UpdateCategory(CategoryToUpdate);
            if (Response.Status == Data.DTO.StatusCode.Error)
            {
                return BadRequest(Response.StatusText);
            }
            return Ok();
        }
    }
}
