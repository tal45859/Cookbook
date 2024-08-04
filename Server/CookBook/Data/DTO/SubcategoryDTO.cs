using CookBook.Data.Entities;

namespace CookBook.Data.DTO
{
    public class SubcategoryDTO
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string SubcategoryName { get; set; }
        public virtual Category Category { get; set; }
    }
}
