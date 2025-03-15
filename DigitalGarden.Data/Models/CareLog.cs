namespace MVCView.Models
{
    public class CareLog
    {
        public int Id { get; set; }
        public int PlantId { get; set; }
        public string? CareType { get; set; }
        public string? Notes { get; set; }
        public DateTime Date { get; set; }

        public Plant? Plant { get; set; }
    }
}
