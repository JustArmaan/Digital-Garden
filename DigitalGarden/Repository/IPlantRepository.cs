namespace MVCView.Models
{
    public interface IPlantRepository
    {
        Task<IEnumerable<Plant>> GetPlants();
        Task<Plant> GetPlant(int id);
        Task AddPlant(Plant plant);
        Task DeletePlant(int id);
        Task UpdatePlant(Plant plant);
    }
}
