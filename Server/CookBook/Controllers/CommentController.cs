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
    public class CommentController : ControllerBase
    {
        //תקציר
        /////////
        //בנאי
        //קבלת תגובה לפי מזהה תגובה
        //הוספת תגובה
        //מחיקת תגובה רק למנהל
        //קבלת רשימת תגובות לפי מזהה מתכון
        //קבלת כל התגובות למנהל

        private readonly CommentService _service;

        //בנאי
        public CommentController(CommentService service)
        {
            _service = service;
        }

        //קבלת תגובה לפי מזהה תגובה
        [HttpGet,Route("GetCommentById/{CommentId}")]
        public ActionResult GetCommentById(int CommentId)
        {
            Comment CommentForClient = _service.GetCommentById(CommentId);
            if(CommentForClient != null)
            {
                return Ok(CommentForClient);
            }
            return BadRequest("לא הצלחנו למצוא את התגובה המבוקשת");
        }

        //הוספת תגובה
        [HttpPost,Route("AddComment")]
        public ActionResult AddComment([FromBody] CommentDTO CommentToAdd)
        {
            bool IsCreated =_service.AddComment(CommentToAdd);
            if(IsCreated)
            {
                return Created("", null);
            }
            return BadRequest("לא הצלחנו להוסיף את התגובה");
        }

        //מחיקת תגובה רק למנהל
        [HttpDelete, Route("DeleteCommentByID/{CommentId}"), Authorize(Roles = "Admin")]
        public ActionResult DeleteCommentByID(int CommentId)
        {
            ResponseDTO Response = _service.DeleteCommentByID(CommentId);
            if(Response.Status == Data.DTO.StatusCode.Error)
            {
                return BadRequest(Response.StatusText);
            }
            return Ok();
        }

        //קבלת רשימת תגובות לפי מזהה מתכון
        [HttpGet,Route("GetAllCommentByRecipeId/{RecipeId}")]
        public ActionResult GetAllCommentByRecipeId(int RecipeId)
        {
            List<Comment> LCommentForClient = _service.GetAllCommentByRecipeId(RecipeId);
            if(LCommentForClient != null)
            {
                return Ok(LCommentForClient);
            }
            return BadRequest("לא הצלחנו למצוא תוצאה לחיפוש המבוקש");
        }

        //קבלת כל התגובות למנהל
        [HttpGet,Route("GetAllCommentForAdmin"),Authorize(Roles ="Admin")]
        public ActionResult GetAllCommentForAdmin()
        {
            List<Comment> LCommentForClient = _service.GetAllCommentForAdmin();
            if (LCommentForClient != null)
            {
                return Ok();
            }
            return BadRequest("לא הצלחנו למצוא תוצאה לחיפוש המבוקש");
        }
    }
}
