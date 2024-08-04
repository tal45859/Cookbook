using CookBook.Data;
using CookBook.Data.DTO;
using CookBook.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace CookBook.Services
{
    public class CommentService
    {
        //תקציר
        /////////
        //בנאי
        //קבלת תגובה לפי מזהה תגובה
        //הוספת תגובה
        //מחיקת תגובה רק למנהל
        //קבלת רשימת תגובות לפי מזהה מתכון
        //קבלת כל התגובות למנהל

        private readonly CookBookDBContext m_db;

        //בנאי
        public CommentService(CookBookDBContext db)
        {
            m_db = db;
        }

        //קבלת תגובה לפי מזהה תגובה
        public Comment GetCommentById(int CommentId)
        {
            return m_db.Comment.Where(c => c.Id == CommentId).FirstOrDefault();
        }

        //הוספת תגובה
        public bool AddComment(CommentDTO CommentToAddFromUser)
        {
            Comment CommentToAdd =new Comment();
            CommentToAdd.RecipeId = CommentToAddFromUser.RecipeId;
            CommentToAdd.Title = CommentToAddFromUser.Title;
            CommentToAdd.Body = CommentToAddFromUser.Body;
            m_db.Comment.Add(CommentToAdd);
            int c = m_db.SaveChanges();
            return c > 0;
        }

        //מחיקת תגובה רק למנהל
        public ResponseDTO DeleteCommentByID(int CommentId)
        {
            Comment CommentToDelete =GetCommentById(CommentId);
            if(CommentToDelete == null || CommentToDelete.Id!= CommentId)
            {
                return new ResponseDTO() { Status = Data.DTO.StatusCode.Error, StatusText = "התגובה לא נמצא בבסיס נתונים" };
            }
            m_db.Comment.Remove(CommentToDelete);
            int c = m_db.SaveChanges();
            return c > 0 ?
                 new ResponseDTO() { Status = Data.DTO.StatusCode.Success }
                 :
                 new ResponseDTO() { Status = Data.DTO.StatusCode.Error, StatusText = "לא הצלחנו למחוק את התגובה" };
        }

        //קבלת רשימת תגובות לפי מזהה מתכון
        public List<Comment> GetAllCommentByRecipeId(int RecipeId)
        {
            return m_db.Comment.Where(c=>c.RecipeId== RecipeId).ToList();
        }

        //קבלת כל התגובות למנהל
        public List<Comment> GetAllCommentForAdmin()
        {
            return m_db.Comment.ToList();
        }
    }
}
