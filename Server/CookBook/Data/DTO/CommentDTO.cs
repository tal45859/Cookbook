namespace CookBook.Data.DTO
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }
}
