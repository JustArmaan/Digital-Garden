using System.Linq;
using System.Threading.Tasks;
using MVCView.Models;
using System.Collections.Generic;

namespace MVCView.Data
{
    public class MockCareLogRepository : ICareLogRepository
    {
        private List<CareLog> _careLogs;
        private readonly IPlantRepository _plantRepository;

        public MockCareLogRepository(IPlantRepository plantRepository)
        {
            _plantRepository = plantRepository;
            _careLogs = new List<CareLog>()
            {
                new CareLog() { Id = 1, PlantId = 1, CareType = "Watered", Notes = "Watered the plant thoroughly", Date = DateTime.Now.AddDays(-1) },
                new CareLog() { Id = 2, PlantId = 2, CareType = "Fertilized", Notes = "Added liquid fertilizer", Date = DateTime.Now.AddDays(-2) },
                new CareLog() { Id = 3, PlantId = 3, CareType = "Rotated", Notes = "Rotated for better sunlight exposure", Date = DateTime.Now.AddDays(-3) }
            };
        }

        public async Task AddCareLog(CareLog careLog)
        {
            careLog.Id = _careLogs.Max(c => c.Id) + 1;
            careLog.Plant = await _plantRepository.GetPlant(careLog.PlantId);
            _careLogs.Add(careLog);
        }

        public async Task<IEnumerable<CareLog>> GetCareLogs()
        {
            // Async fetching of plants could be done here if needed, but in this case we're returning the list.
            foreach (var careLog in _careLogs)
            {
                careLog.Plant = await _plantRepository.GetPlant(careLog.PlantId);  // This is an async call for plant data
            }
            return _careLogs;
        }

        public async Task<CareLog?> GetCareLog(int id)
        {
            var careLog = _careLogs.FirstOrDefault(c => c.Id == id);
            if (careLog != null)
            {
                careLog.Plant = await _plantRepository.GetPlant(careLog.PlantId);  // Async plant fetching
            }
            return careLog;
        }

        public async Task DeleteCareLog(int id)
        {
            var careLog = _careLogs.FirstOrDefault(c => c.Id == id);
            if (careLog != null)
            {
                _careLogs.Remove(careLog);
            }
        }

        public async Task UpdateCareLog(CareLog careLog)
        {
            var existingCareLog = _careLogs.FirstOrDefault(c => c.Id == careLog.Id);
            if (existingCareLog != null)
            {
                existingCareLog.CareType = careLog.CareType;
                existingCareLog.Notes = careLog.Notes;
                existingCareLog.Date = careLog.Date;
                existingCareLog.Plant = await _plantRepository.GetPlant(careLog.PlantId);  // Async plant fetching
            }
        }
    }
}
