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
    public class RecipeController : ControllerBase
    {
        //תקציר
        /////////
        //בנאי
        //JWT הוספת מתכון על פי
        //הוספת צפיה למתכון לפי מזהה מתכון
        //הוספת אהבתי או לא אהבתי למתכון לפי מזהה מתכון
        //מחיקת מתכון למשתמש ומנהל
        //עדכון מתכון למי שייצר אותו
        //שינוי מצב האם ניתן לצפות במתכון לפי מזהה מתכון //צריך לעדכן גם את המועדפים שימחק משם
        //JWT קבלת מזהה מתכון אחרון על פי
        //קבלת מתכון לפי מזהה מתכון עם אוביקטים למנהל
        //קבלת מתכון שניתן לצפות בו לפי מזהה מתכון עם אוביקטים
        //JWT קבלת מתכון לפי מזהה מתכון עם אוביקטים למשתמש על פי
        //קבלת רשימת כל המתכונים כולל אוביקטים למנהל
        //קבלת רשימת כל המתכונים שמותר לצפות בהם עם אוביקטים
        // JWT קבלת רשימת כל המתכונים של משתמש למשתמש לפי 
        //קבלת רשימת כל המתכונים שאפשר לצפות בהם של משתמש מסוים לפי מזהה משתמש
        //קבלת רשימת כל המתכונים שניתן לצפות בהם לפי מזהה תת קטגוריה
        //קבלת רשימת כל המתכונים שניתן לצפות בהם לפי מזהה קטגוריה
        //קבלת רשימת המתכונים בצורה ממוינת מי הנצפים ביותר שניתן לצפות בהם
        //JWT קבלת רשימת המתכונים בצורה ממוינת מי הנצפים ביותר של משתמש על פי
        //קבלת רשימת מתכונים בצורה ממוינת מי האהובים ביותר שניתן לצפות בהם
        //JWT קבלת רשימת מתכונים בצורה ממוינת מי האהובים ביותר של משתמש על פי
        //קבלת רשימת מתכונים בצורה ממוינת מי הכי לא אהובים שניתן לצפות בהם
        //JWT קבלת רשימת מתכונים בצורה ממוינת מי הכי לא אהובים של משתמש על פי
        //קבלת רשימת מתכונים בצורה ממוינת מי החדשים ביותר שניתן לצפות בהם
        //JWT קבלת רשימת מתכונים בצורה ממוינת מי החדשים ביותר של משתמש על פי

        private readonly RecipeService _service;

        //בנאי
        public RecipeController(RecipeService service)
        {
            _service = service;
        }

        //JWT הוספת מתכון על פי
        [HttpPost,Route("AddRecipe"),Authorize(Roles = "Classic")]
        public ActionResult AddRecipe([FromBody] RecipeDTO RecipeToAdd)
        {
            bool IsCreated = _service.AddRecipe(RecipeToAdd);
            if(IsCreated)
            {
                return Created("", null);
            }
            return BadRequest("לא הצלחנו לשמור את המתכון");
        }

        //הוספת צפיה למתכון לפי מזהה מתכון
        [HttpGet,Route("AddViewsToRecipe/{RecipeId}"),AllowAnonymous]
        public ActionResult AddViewsToRecipe(int RecipeId)
        {
            ResponseDTO Response = _service.AddViewsToRecipe(RecipeId);
            if(Response.Status == Data.DTO.StatusCode.Error)
            {
                return BadRequest(Response.StatusText);
            }
            return Ok();
        }

        //הוספת אהבתי או לא אהבתי למתכון לפי מזהה מתכון
        [HttpPut,Route("AddLikeOrNoLikeToRecipe/{RecipeId}"),AllowAnonymous]
        public ActionResult AddLikeOrNoLikeToRecipe(int RecipeId ,[FromBody]bool likeOrNoFroUser)
        {
            ResponseDTO Response = _service.AddLikeOrNoLikeToRecipe(RecipeId, likeOrNoFroUser);
            if (Response.Status == Data.DTO.StatusCode.Error)
            {
                return BadRequest(Response.StatusText);
            }
            return Ok();
        }

        //מחיקת מתכון למשתמש ומנהל
        [HttpDelete,Route("DeleteRecipe/{RecipeId}")]
        public ActionResult DeleteRecipe(int RecipeId)
        {
            ResponseDTO Response = _service.DeleteRecipe(RecipeId);
            if (Response.Status == Data.DTO.StatusCode.Error)
            {
                return BadRequest(Response.StatusText);
            }
            return Ok();
        }

        //עדכון מתכון למי שייצר אותו
        [HttpPut, Route("UpdateRecipe"),Authorize(Roles = "Classic")]
        public ActionResult UpdateRecipe([FromBody] RecipeDTO RecipeToUpdate)
        {
            ResponseDTO Response = _service.UpdateRecipe(RecipeToUpdate);
            if (Response.Status == Data.DTO.StatusCode.Error)
            {
                return BadRequest(Response.StatusText);
            }
            return Ok();
        }

        //שינוי מצב האם ניתן לצפות במתכון לפי מזהה מתכון //צריך לעדכן גם את המועדפים שימחק משם
        [HttpPut, Route("UpdateCanBeExpectedForUser/{RecipeId}"), Authorize(Roles = "Classic")]
        public ActionResult UpdateCanBeExpectedForUser(int RecipeId,[FromBody] bool CanShow)
        {
            ResponseDTO Response = _service.UpdateCanBeExpectedForUser(RecipeId, CanShow);
            if (Response.Status == Data.DTO.StatusCode.Error)
            {
                return BadRequest(Response.StatusText);
            }
            return Ok();
        }

        //JWT קבלת מזהה מתכון אחרון על פי
        [HttpGet, Route("GetLastRecipeIdByJWTForUser"),Authorize(Roles ="Classic")]
        public ActionResult GetLastRecipeIdByJWTForUser()
        {
            int Id = _service.GetLastRecipeIdByJWTForUser();
            if(Id==0)
            {
                return BadRequest();
            }
            return Ok(Id);
        }

        //קבלת מתכון לפי מזהה מתכון עם אוביקטים למנהל
        [HttpGet, Route("GetRecipeByIdForAdmin/{RecipeId}"), Authorize(Roles = "Admin")]
        public ActionResult GetRecipeByIdForAdmin(int RecipeId)
        {
            Recipe RecipeToClient = _service.GetRecipeByIdForAdmin(RecipeId);
            if (RecipeToClient != null) 
            {
                return Ok(RecipeToClient);
            }
            return BadRequest("לא הצלחנו למצוא את המתכון המבוקש");
        }

        //קבלת מתכון שניתן לצפות בו לפי מזהה מתכון עם אוביקטים
        [HttpGet, Route("GetRecipeCanBeExpectedById/{RecipeId}"), AllowAnonymous]
        public ActionResult GetRecipeCanBeExpectedById(int RecipeId)
        {
            Recipe RecipeToClient = _service.GetRecipeCanBeExpectedById(RecipeId);
            if (RecipeToClient != null)
            {
                return Ok(RecipeToClient);
            }
            return BadRequest("לא הצלחנו למצוא את המתכון המבוקש");
        }

        //JWT קבלת מתכון לפי מזהה מתכון עם אוביקטים למשתמש על פי
        [HttpGet, Route("GetRecipeByIdAndJWTForUser/{RecipeId}"), Authorize(Roles = "Classic")]
        public ActionResult GetRecipeByIdAndJWTForUser(int RecipeId)
        {
            Recipe RecipeToClient = _service.GetRecipeByIdAndJWTForUser(RecipeId);
            if (RecipeToClient != null)
            {
                return Ok(RecipeToClient);
            }
            return BadRequest("לא הצלחנו למצוא את המתכון המבוקש");
        }

        //קבלת רשימת כל המתכונים כולל אוביקטים למנהל
        [HttpGet, Route("GetAllRecipeForAdmin"), Authorize(Roles = "Admin")]
        public ActionResult GetAllRecipeForAdmin()
        {
            List<Recipe> LRecipeToClient = _service.GetAllRecipeForAdmin();
            if (LRecipeToClient != null)
            {
                return Ok(LRecipeToClient);
            }
            return BadRequest("לא הצלחנו למצוא את המתכונים לפי החיפוש המבוקש");
        }

        //קבלת רשימת כל המתכונים שמותר לצפות בהם עם אוביקטים
        [HttpGet, Route("GetAllRecipeCanBeExpected"), AllowAnonymous]
        public ActionResult GetAllRecipeCanBeExpected()
        {
            List<Recipe> LRecipeToClient = _service.GetAllRecipeCanBeExpected();
            if (LRecipeToClient != null)
            {
                return Ok(LRecipeToClient);
            }
            return BadRequest("לא הצלחנו למצוא את המתכונים לפי החיפוש המבוקש");
        }

        // JWT קבלת רשימת כל המתכונים של משתמש למשתמש לפי 
        [HttpGet, Route("GetAllRecipeJWTForUser"), Authorize(Roles = "Classic")]
        public ActionResult GetAllRecipeJWTForUser(int RecipeId)
        {
            List<Recipe> LRecipeToClient = _service.GetAllRecipeJWTForUser();
            if (LRecipeToClient != null)
            {
                return Ok(LRecipeToClient);
            }
            return BadRequest("לא הצלחנו למצוא את המתכונים לפי החיפוש המבוקש");
        }

        //קבלת רשימת כל המתכונים שאפשר לצפות בהם של משתמש מסוים לפי מזהה משתמש
        [HttpGet, Route("GetAllRecipeCanBeExpectedByUserId/{UserId}"), AllowAnonymous]
        public ActionResult GetAllRecipeCanBeExpectedByUserId(int UserId)
        {
            List<Recipe> LRecipeToClient = _service.GetAllRecipeCanBeExpectedByUserId(UserId);
            if (LRecipeToClient != null)
            {
                return Ok(LRecipeToClient);
            }
            return BadRequest("לא הצלחנו למצוא את המתכונים לפי החיפוש המבוקש");
        }

        //קבלת רשימת כל המתכונים שניתן לצפות בהם לפי מזהה תת קטגוריה
        [HttpGet, Route("GetAllRecipeCanBeExpectedBySubcategoryId/{SubcategoryId}"), AllowAnonymous]
        public ActionResult GetAllRecipeCanBeExpectedBySubcategoryId(int SubcategoryId)
        {
            List<Recipe> LRecipeToClient = _service.GetAllRecipeCanBeExpectedBySubcategoryId(SubcategoryId);
            if (LRecipeToClient != null)
            {
                return Ok(LRecipeToClient);
            }
            return BadRequest("לא הצלחנו למצוא את המתכונים לפי החיפוש המבוקש");
        }

        //קבלת רשימת כל המתכונים שניתן לצפות בהם לפי מזהה קטגוריה
        [HttpGet, Route("GetAllRecipeCanBeExpectedByCategoryId/{CategoryId}"), AllowAnonymous]
        public ActionResult GetAllRecipeCanBeExpectedByCategoryId(int CategoryId)
        {
            List<Recipe> LRecipeToClient = _service.GetAllRecipeCanBeExpectedByCategoryId(CategoryId);
            if (LRecipeToClient != null)
            {
                return Ok(LRecipeToClient);
            }
            return BadRequest("לא הצלחנו למצוא את המתכונים לפי החיפוש המבוקש");
        }

        //קבלת רשימת המתכונים בצורה ממוינת מי הנצפים ביותר שניתן לצפות בהם
        [HttpGet, Route("GetAllRecipeCanBeExpectedByNumberOfViews"), AllowAnonymous]
        public ActionResult GetAllRecipeCanBeExpectedByNumberOfViews()
        {
            List<Recipe> LRecipeToClient = _service.GetAllRecipeCanBeExpectedByNumberOfViews();
            if (LRecipeToClient != null)
            {
                return Ok(LRecipeToClient);
            }
            return BadRequest("לא הצלחנו למצוא את המתכונים לפי החיפוש המבוקש");
        }

        //JWT קבלת רשימת המתכונים בצורה ממוינת מי הנצפים ביותר של משתמש על פי
        [HttpGet, Route("GetAllRecipeByJWTAndByNumberOfViewsForUser"), Authorize(Roles = "Classic")]
        public ActionResult GetAllRecipeByJWTAndByNumberOfViewsForUser()
        {
            List<Recipe> LRecipeToClient = _service.GetAllRecipeByJWTAndByNumberOfViewsForUser();
            if (LRecipeToClient != null)
            {
                return Ok(LRecipeToClient);
            }
            return BadRequest("לא הצלחנו למצוא את המתכונים לפי החיפוש המבוקש");
        }

        //קבלת רשימת מתכונים בצורה ממוינת מי האהובים ביותר שניתן לצפות בהם
        [HttpGet, Route("GetAllRecipeCanBeExpectedByNumberOfLikes"),AllowAnonymous]
        public ActionResult GetAllRecipeCanBeExpectedByNumberOfLikes()
        {
            List<Recipe> LRecipeToClient = _service.GetAllRecipeCanBeExpectedByNumberOfLikes();
            if (LRecipeToClient != null)
            {
                return Ok(LRecipeToClient);
            }
            return BadRequest("לא הצלחנו למצוא את המתכונים לפי החיפוש המבוקש");
        }

        //JWT קבלת רשימת מתכונים בצורה ממוינת מי האהובים ביותר של משתמש על פי
        [HttpGet, Route("GetAllRecipeByJWTAndByNumberOfLikesForUser"), Authorize(Roles = "Classic")]
        public ActionResult GetAllRecipeByJWTAndByNumberOfLikesForUser()
        {
            List<Recipe> LRecipeToClient = _service.GetAllRecipeByJWTAndByNumberOfLikesForUser();
            if (LRecipeToClient != null)
            {
                return Ok(LRecipeToClient);
            }
            return BadRequest("לא הצלחנו למצוא את המתכונים לפי החיפוש המבוקש");
        }

        //קבלת רשימת מתכונים בצורה ממוינת מי הכי לא אהובים שניתן לצפות בהם
        [HttpGet, Route("GetAllRecipeCanBeExpectedByNumberOfNoLieks"), AllowAnonymous]
        public ActionResult GetAllRecipeCanBeExpectedByNumberOfNoLieks()
        {
            List<Recipe> LRecipeToClient = _service.GetAllRecipeCanBeExpectedByNumberOfNoLieks();
            if (LRecipeToClient != null)
            {
                return Ok(LRecipeToClient);
            }
            return BadRequest("לא הצלחנו למצוא את המתכונים לפי החיפוש המבוקש");
        }

        //JWT קבלת רשימת מתכונים בצורה ממוינת מי הכי לא אהובים של משתמש על פי
        [HttpGet, Route("GetAllRecipeByJWTAndByNumberOfNoLieksForUser"), Authorize(Roles = "Classic")]
        public ActionResult GetAllRecipeByJWTAndByNumberOfNoLieksForUser()
        {
            List<Recipe> LRecipeToClient = _service.GetAllRecipeByJWTAndByNumberOfNoLieksForUser();
            if (LRecipeToClient != null)
            {
                return Ok(LRecipeToClient);
            }
            return BadRequest("לא הצלחנו למצוא את המתכונים לפי החיפוש המבוקש");
        }

        //קבלת רשימת מתכונים בצורה ממוינת מי החדשים ביותר שניתן לצפות בהם
        [HttpGet, Route("GetAllRecipeCanBeExpectedByUploadDate"), AllowAnonymous]
        public ActionResult GetAllRecipeCanBeExpectedByUploadDate()
        {
            List<Recipe> LRecipeToClient = _service.GetAllRecipeCanBeExpectedByUploadDate();
            if (LRecipeToClient != null)
            {
                return Ok(LRecipeToClient);
            }
            return BadRequest("לא הצלחנו למצוא את המתכונים לפי החיפוש המבוקש");
        }

        //JWT קבלת רשימת מתכונים בצורה ממוינת מי החדשים ביותר של משתמש על פי
        [HttpGet, Route("GetAllRecipeByJWTAndByUploadDateForUser"), Authorize(Roles = "Classic")]
        public ActionResult GetAllRecipeByJWTAndByUploadDateForUser()
        {
            List<Recipe> LRecipeToClient = _service.GetAllRecipeByJWTAndByUploadDateForUser();
            if (LRecipeToClient != null)
            {
                return Ok(LRecipeToClient);
            }
            return BadRequest("לא הצלחנו למצוא את המתכונים לפי החיפוש המבוקש");
        }
    }
}
