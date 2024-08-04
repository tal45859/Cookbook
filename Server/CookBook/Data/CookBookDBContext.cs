using CookBook.Data.Entities;
using Microsoft.EntityFrameworkCore;


namespace CookBook.Data
{
    public class CookBookDBContext : DbContext
    {
        public CookBookDBContext(DbContextOptions<CookBookDBContext> options) : base(options)
        {

        }
        public virtual DbSet<User> User { get; set; }//map User Table
        public virtual DbSet<Category> Category { get; set; }//map Categories Table
        public virtual DbSet<Subcategory> Subcategory { get; set; }//map Subcategory Table
        public virtual DbSet<Recipe> Recipe { get; set; }//map Recipe Table
        public virtual DbSet<Image> Image { get; set; }//map Image Table
        public virtual DbSet<Favorite> Favorite { get; set; }//map Favorite Table
        public virtual DbSet<LoginHistory> LoginHistory { get; set; }//map LoginHistory Table
        public virtual DbSet<Comment> Comment { get; set; }//map Comment Table
        public virtual DbSet<Reporting> Reporting { get; set; }//map Comment Table


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{

        //}
    }
}
