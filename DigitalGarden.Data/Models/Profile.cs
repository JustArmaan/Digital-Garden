namespace MVCView.Models
{
    public class Profile
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Bio { get; set; }
        public string? Email { get; set; }
        public string? Location { get; set; }
        public DateTime JoinedDate { get; set; }
    }
}
