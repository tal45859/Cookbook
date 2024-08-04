using CookBook.Data.Entities;
using System;

namespace CookBook.Data.DTO
{
    public class RecipeDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SubcategoryId { get; set; }
        public string RecipeName { get; set; }
        public string Ingredients { get; set; }
        public string PreparationMethod { get; set; }
        public int PreparationTime { get; set; }
        public int QuantityOfPortions { get; set; }
        public DateTime UploadDate { get; set; }
        public bool CanBeExpected { get; set; }
        public int NumberOfViews { get; set; }
        public int NumberOfLikes { get; set; }
        public int NumberOfNoLieks { get; set; }
        public virtual User User { get; set; }
        public virtual Subcategory Subcategory { get; set; }
    }
}
