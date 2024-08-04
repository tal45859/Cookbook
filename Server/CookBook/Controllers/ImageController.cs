using CookBook.Data.DTO;
using CookBook.Data.Entities;
using CookBook.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;

namespace CookBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Classic")]
    public class ImageController : ControllerBase
    {
        //תקציר
        //////////////
        //בנאי
        //הוספת תמונה חדשה לבסיס נתונים
        //הוספת תמונה ישירות לתיקיה
        //קבלת תמונה לפי מזהה
        //קבלת רשימת תמונות לפי מזהה מתכון
        //מחיקת תמונה לוודא שזה משתמש ששיכת לו התמונה
        //מחיקת תמונה לוודא שזה מנהל
        //בדיקת תקינות התמונה
        //קבלת כל התמונות של המתכונים שאפשר לראות
        //JWTקבלת כל התמונות על פי 

        private readonly ImageService _service;

        //בנאי
        public ImageController(ImageService service)
        {
            _service = service;
        }

        //הוספת תמונה חדשה לבסיס נתונים
        [HttpPost, Route("AddImageToDB"),Authorize(Roles = "Classic")]
        public ActionResult AddImageToDB([FromBody] ImageDTO ImageToAdd)
        {
            bool IsCreated = _service.AddImage(ImageToAdd);
            if (IsCreated)
            {
                return Created("", null);
            }
            return BadRequest("לא הצלחנו לשמור את התמונה");
        }

        //הוספת תמונה ישירות לתיקיה
        [HttpPost, Route("AddImageToFolder"), Authorize(Roles = "Classic"), DisableRequestSizeLimit]
        public IActionResult UploadImageToFolder()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("StaticFiles", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"').Replace(" ", "");
                    var fullPath = Path.Combine(pathToSave, fileName);
                    //string urlToDB = "https://localhost:44328/StaticFiles/Images/StaticFiles/Images/" + fileName.ToString();
                    string urlToDB = "https://localhost:44328/StaticFiles/Images/" + fileName.ToString();

                    if (IsAPhotoFile(fileName))
                    {
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }

                        //return Ok(urlToDB);
                        return Ok();
                    }
                    return BadRequest();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        //קבלת תמונה לפי מזהה
        [HttpGet,Route("GetImageById/{ImageId}"),AllowAnonymous]
        public ActionResult GetImageById(int ImageId)
        {
            Image ImageForClient = _service.GetImageById(ImageId);
            if (ImageForClient != null)
            {
                return Ok(ImageForClient);
            }
            return BadRequest("לא הצלחנו למצוא את התמונה המבוקשת");
        }

        //קבלת רשימת תמונות לפי מזהה מתכון
        [HttpGet, Route("GetAllImageByRecipeId/{RecipeId}"), AllowAnonymous]
        public ActionResult GetAllImageByRecipeId(int RecipeId)
        {
            List<Image> LImageForClient = _service.GetAllImageByRecipeId(RecipeId);
            if (LImageForClient != null)
            {
                return Ok(LImageForClient);
            }
            return BadRequest("לא הצלחנו למצוא את התמונה המבוקשת");
        }

        //מחיקת תמונה לוודא שזה משתמש ששיכת לו התמונה
        [HttpDelete,Route("DeleteImageForUser/{ImageId}"), Authorize(Roles = "Classic")]
        public ActionResult DeleteImageForUser(int ImageId)
        {
            ResponseDTO Response = _service.DeleteImageForUser(ImageId);
            if(Response.Status==Data.DTO.StatusCode.Error)
            {
                return BadRequest(Response.StatusText);
            }
            return Ok();
        }

        //מחיקת תמונה לוודא שזה מנהל
        [HttpDelete, Route("DeleteImageForAdmin/{ImageId}"), Authorize(Roles = "Admin")]
        public ActionResult DeleteImageForAdmin(int ImageId)
        {
            ResponseDTO Response = _service.DeleteImageForAdmin(ImageId);
            if (Response.Status == Data.DTO.StatusCode.Error)
            {
                return BadRequest(Response.StatusText);
            }
            return Ok();
        }

        //בדיקת תקינות התמונה
        private bool IsAPhotoFile(string fileName)
        {
            return fileName.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase)
                   || fileName.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase)
                   || fileName.EndsWith(".png", StringComparison.OrdinalIgnoreCase);
        }

        //קבלת כל התמונות של המתכונים שאפשר לראות
        [HttpGet, Route("GetAllImageCanBeExpected"),AllowAnonymous]
        public ActionResult GetAllImageCanBeExpected()
        {
            List<Image> LImage = _service.GetAllImageCanBeExpected();
            if(LImage != null)
            {
                return Ok(LImage);
            }
            return BadRequest();
        }

        //JWTקבלת כל התמונות על פי 
        [HttpGet, Route("GetAllImageByJWT"),Authorize(Roles = "Classic")]
        public ActionResult GetAllImageByJWT()
        {
            List<Image> LImage = _service.GetAllImageByJWT();
            if (LImage != null)
            {
                return Ok(LImage);
            }
            return BadRequest();
        }

    }
}
