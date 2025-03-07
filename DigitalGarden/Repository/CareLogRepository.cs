using DigitalGarden.Data;
using Microsoft.EntityFrameworkCore;
using MVCView.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCView.Data
{
    public class CareLogRepository : ICareLogRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IPlantRepository _plantRepository;

        public CareLogRepository(ApplicationDbContext context, IPlantRepository plantRepository)
        {
            _context = context;
            _plantRepository = plantRepository;
        }

        public async Task AddCareLog(CareLog careLog)
        {
            careLog.Plant = await _plantRepository.GetPlant(careLog.PlantId);
            _context.CareLogs.Add(careLog);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CareLog>> GetCareLogs()
        {
            return await _context.CareLogs
                                 .Include(c => c.Plant) // Include related plant data
                                 .ToListAsync();
        }

        public async Task<CareLog?> GetCareLog(int id)
        {
            return await _context.CareLogs
                                 .Include(c => c.Plant) // Include related plant data
                                 .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task DeleteCareLog(int id)
        {
            var careLog = await _context.CareLogs
                                        .FirstOrDefaultAsync(c => c.Id == id);
            if (careLog != null)
            {
                _context.CareLogs.Remove(careLog);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateCareLog(CareLog careLog)
        {
            var existingCareLog = await _context.CareLogs
                                                .FirstOrDefaultAsync(c => c.Id == careLog.Id);
            if (existingCareLog != null)
            {
                existingCareLog.CareType = careLog.CareType;
                existingCareLog.Notes = careLog.Notes;
                existingCareLog.Date = careLog.Date;
                existingCareLog.Plant = await _plantRepository.GetPlant(careLog.PlantId);
                await _context.SaveChangesAsync();
            }
        }
    }
}
