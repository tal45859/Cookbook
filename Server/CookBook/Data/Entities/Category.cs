using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CookBook.Data.Entities
{
    [Table("Category")]
    public class Category
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("CategoryName"),MaxLength(50)]
        public string CategoryName { get; set; }

        [Required]
        public virtual ICollection<Subcategory> Subcategory { get; set; }
    }
}
