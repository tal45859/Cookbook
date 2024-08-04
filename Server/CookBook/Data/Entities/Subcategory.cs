using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CookBook.Data.Entities
{
    [Table("Subcategory")]
    public class Subcategory
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("CategoryId"),Required]
        public int CategoryId { get; set; }

        [Column("SubcategoryName"),MaxLength(50)]
        public string SubcategoryName { get; set; }

        [Required]
        public virtual Category Category { get; set; }

        [Required]
        public virtual ICollection<Recipe> Recipe { get; set; }
    }
}
