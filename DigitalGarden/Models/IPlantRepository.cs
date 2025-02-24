namespace MVCView.Models
{
    public interface IPlantRepository
    {
        void AddPlant(Plant plant);
        IEnumerable<Plant> GetPlants();
        Plant? GetPlant(int id);

        void DeletePlant(int id);
        void UpdatePlant(Plant plant);
    }
}
