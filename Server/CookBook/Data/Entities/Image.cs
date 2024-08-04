using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CookBook.Data.Entities
{
    [Table("Image")]
    public class Image
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("RecipeId")]
        public int RecipeId { get; set; }

        [Column("Url")]
        public string Url { get; set; }

        [Column(TypeName = "date")]
        public DateTime UploadDate { get; set; }
    }
}
