namespace MVCView.Models
{
    public class Garden
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int UserId { get; set; }

        public User? User { get; set; }
        public List<Plant>? Plants { get; set; } 
    }
}
