using CookBook.Data.Entities;
using System;

namespace CookBook.Data.DTO
{
    public class FavoriteDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RecipeId { get; set; }
        public DateTime DateAdded { get; set; }
        public virtual Recipe Recipe { get; set; }

    }
}
