namespace MVCView.Models
{
  public class Reminder
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PlantId { get; set; }
        public DateTime Date { get; set; }
        public string? ReminderType { get; set; }

        public User? User { get; set; }
        public Plant? Plant { get; set; } 
    }
}
