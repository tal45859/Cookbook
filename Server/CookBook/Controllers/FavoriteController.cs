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
    [Authorize(Roles = "Classic")]
    public class FavoriteController : ControllerBase
    {
        //תקציר
        //////////
        //בנאי
        //הוספת מועדף
        //מחיקת מועדף
        //JWT קבלת מועדף לפי מזהה מועדף ולפי 
        //JWT קבלת רשימת מועדפים לפי 

        private readonly FavoriteService _service;

        //בנאי
        public FavoriteController(FavoriteService service)
        {
            _service = service;
        }

        //הוספת מועדף
        [HttpPost,Route("AddFavorite")]
        public ActionResult AddFavorite([FromBody] FavoriteDTO FavoriteToAdd)
        {
            bool IsCreated = _service.AddFavorite(FavoriteToAdd);
            if(IsCreated)
            {
                return Created("", null);
            }
            return BadRequest("לא הצלחנו להוסיף את המועדף");
        }

        //מחיקת מועדף
        [HttpDelete,Route("DeleteFavoriteById/{FavoriteId}")]
        public ActionResult DeleteFavoriteById(int FavoriteId)
        {
            ResponseDTO Response =_service.DeleteFavoriteById(FavoriteId);
            if(Response.Status == Data.DTO.StatusCode.Error)
            {
                return BadRequest(Response.StatusText);
            }
            return Ok();
        }

        //JWT קבלת מועדף לפי מזהה מועדף ולפי 
        [HttpGet,Route("GetFavoriteByIdAndJwt/{FavoriteId}")]
        public ActionResult GetFavoriteByIdAndJwt(int FavoriteId)
        {
            Favorite FavoriteForClient = _service.GetFavoriteByIdAndJwt(FavoriteId);
            if (FavoriteForClient != null) 
            {
                return Ok(FavoriteForClient);
            }
            return BadRequest("לא הצלחנו למצוא את המועדף המבוקש");
        }

        //JWT קבלת רשימת מועדפים לפי 
        [HttpGet, Route("GetAllFavoriteJwt")]
        public ActionResult GetAllFavoriteJwt()
        {
            List<Favorite> LFavoriteForClient = _service.GetAllFavoriteJwt();
            if (LFavoriteForClient != null)
            {
                return Ok(LFavoriteForClient);
            }
            return BadRequest("לא הצלחנו למצוא את המועדף המבוקש");
        }
    }
}
