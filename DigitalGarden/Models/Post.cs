namespace MVCView.Models
{
    public class Post
    {
        public int Id { get; set; }
        public int UserId { get; set; }  // Foreign key
        public string? Content { get; set; }
        public DateTime Date { get; set; }

        public User? User { get; set; }  // Navigation property
        public List<Comment>? Comments { get; set; }  // A post can have multiple comments
    }
}
