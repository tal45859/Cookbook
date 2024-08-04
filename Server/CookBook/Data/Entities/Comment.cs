using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CookBook.Data.Entities
{
    [Table("Comment")]
    public class Comment
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("RecipeId")]
        public int RecipeId { get; set; }

        [Column("Title"),MaxLength(150)]
        public string Title { get; set; }

        [Column("Body")]
        public string Body { get; set; }
    }
}
