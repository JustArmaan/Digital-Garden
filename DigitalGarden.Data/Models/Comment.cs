   namespace MVCView.Models
{
   public class Comment
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string? Content { get; set; }
        public DateTime Date { get; set; }

        public Post? Post { get; set; }
        public User? User { get; set; }
    }
}
