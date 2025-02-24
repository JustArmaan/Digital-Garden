namespace MVCView.Models
{
    public class Plant
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime LastWatered { get; set; }
        public string? CareInstructions { get; set; }
        public string? Species { get; set; }
        public int WateringSchedule { get; set; }
        public string? Sunlight { get; set; }
        public string? Notes { get; set; }
    }
}
