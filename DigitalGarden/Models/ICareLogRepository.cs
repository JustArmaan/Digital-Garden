namespace MVCView.Models
{
    public interface ICareLogRepository
    {
        void AddCareLog(CareLog careLog);
        IEnumerable<CareLog> GetCareLogs();
        CareLog? GetCareLog(int id);
        void DeleteCareLog(int id);
        void UpdateCareLog(CareLog careLog);
    }
}
