using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CookBook.Data.Entities
{
    [Table("Recipe")]
    public class Recipe
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("UserId"),Required]
        public int UserId { get; set; }

        [Column("SubcategoryId"), Required]
        public int SubcategoryId { get; set; }

        [Column("RecipeName"),MaxLength(150)]
        public string RecipeName { get; set; }

        [Column("Ingredients")]
        public string Ingredients { get; set; }

        [Column("PreparationMethod")]
        public string PreparationMethod { get; set; }

        [Column("PreparationTime")]
        public int PreparationTime { get; set; }

        [Column("QuantityOfPortions")]
        public int QuantityOfPortions { get; set; }

        [Column(TypeName = "date")]
        public DateTime UploadDate { get; set; }

        [Column("CanBeExpected")]
        public bool CanBeExpected { get; set; }

        [Column("NumberOfViews")]
        public int NumberOfViews { get; set; }

        [Column("NumberOfLikes")]
        public int NumberOfLikes { get; set; }

        [Column("NumberOfNoLieks")]
        public int NumberOfNoLieks { get; set; }

        [Required]
        public virtual User User { get; set; }

        [Required]
        public virtual Subcategory Subcategory { get; set; }

        [Required]
        public virtual ICollection<Favorite> Favorite { get; set; }
    }
}
