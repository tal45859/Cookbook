using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CookBook.Data.Entities
{
    [Table("Reporting")]
    public class Reporting
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; } 

        [Column("Cause")]
        public string Cause { get; set; }

        [Column("RecipeId")]
        public int RecipeId { get; set; }

        [Column("IsActive")]
        public bool IsActive { get; set; }

        [Column("ClosingExplanation")]
        public string ClosingExplanation { get; set; }
    }
}
