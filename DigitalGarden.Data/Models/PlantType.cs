namespace MVCView.Models
{
    public class PlantType
    {
        public int Id { get; set; }
        public string? Species { get; set; }
        public string? DefaultCareInstructions { get; set; }

        public List<Plant>? Plants { get; set; }  // One plant type can be linked to many plants
    }
}
