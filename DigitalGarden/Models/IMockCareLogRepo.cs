namespace MVCView.Models
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
                new CareLog() { Id = 1, PlantId = 1, CareType = "Watered", Notes = "Watered the plant thoroughly", Date = DateTime.Now.AddDays(-1), Plant = _plantRepository.GetPlant(1) },
                new CareLog() { Id = 2, PlantId = 2, CareType = "Fertilized", Notes = "Added liquid fertilizer", Date = DateTime.Now.AddDays(-2), Plant = _plantRepository.GetPlant(2) },
                new CareLog() { Id = 3, PlantId = 3, CareType = "Rotated", Notes = "Rotated for better sunlight exposure", Date = DateTime.Now.AddDays(-3), Plant = _plantRepository.GetPlant(3) }
            };
        }

        public void AddCareLog(CareLog careLog)
        {
            careLog.Id = _careLogs.Max(c => c.Id) + 1;
            careLog.Plant = _plantRepository.GetPlant(careLog.PlantId);
            _careLogs.Add(careLog);
        }

        public IEnumerable<CareLog> GetCareLogs()
        {
            return _careLogs;
        }

        public CareLog? GetCareLog(int id)
        {
            return _careLogs.FirstOrDefault(c => c.Id == id);
        }

        public void DeleteCareLog(int id)
        {
            var careLog = _careLogs.FirstOrDefault(c => c.Id == id);
            if (careLog != null)
            {
                _careLogs.Remove(careLog);
            }
        }

        public void UpdateCareLog(CareLog careLog)
        {
            var existingCareLog = _careLogs.FirstOrDefault(c => c.Id == careLog.Id);
            if (existingCareLog != null)
            {
                existingCareLog.CareType = careLog.CareType;
                existingCareLog.Notes = careLog.Notes;
                existingCareLog.Date = careLog.Date;
                existingCareLog.Plant = _plantRepository.GetPlant(careLog.PlantId);
            }
        }
    }
}
