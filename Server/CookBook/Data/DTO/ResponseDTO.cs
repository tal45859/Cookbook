namespace CookBook.Data.DTO
{
    public enum StatusCode
    {
        Success = 1000,
        Error,
        Warning

    }
    public class ResponseDTO
    {
        public StatusCode Status { get; set; }
        public string StatusText { get; set; }
    }
}
