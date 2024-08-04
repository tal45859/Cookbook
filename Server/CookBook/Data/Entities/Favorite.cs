using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CookBook.Data.Entities
{
    [Table("Favorite")]
    public class Favorite
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("UserId")]
        public int UserId { get; set; } 

        [Column("RecipeId"),Required]
        public int RecipeId { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateAdded { get; set; }


        [Required]
        public virtual Recipe Recipe { get; set; }
    }
}