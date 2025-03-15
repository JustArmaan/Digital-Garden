using MVCView.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVCView.Data
{
public interface ICareLogRepository
{
    Task AddCareLog(CareLog careLog);
    Task<IEnumerable<CareLog>> GetCareLogs();
    Task<CareLog?> GetCareLog(int id);
    Task DeleteCareLog(int id);
    Task UpdateCareLog(CareLog careLog);
}
}
