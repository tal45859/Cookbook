using System;

namespace CookBook.Data.DTO
{
    public class ImageDTO
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public string Url { get; set; }
        public DateTime UploadDate { get; set; }
    }
}
