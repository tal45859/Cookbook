using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CookBook.Data.Entities
{
    [Table("LoginHistory")]
    public class LoginHistory
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("UserId"),Required]
        public int UserId { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateAdded { get; set; }

        [Required]
        public virtual User User { get; set; }
    }
}
