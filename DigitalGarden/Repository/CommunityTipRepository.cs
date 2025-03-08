using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DigitalGarden.Data;
using Microsoft.EntityFrameworkCore;
using MVCView.Data;
using MVCView.Models;

namespace MVCView.Repositories
{
    public class CommunityTipRepository : ICommunityTipRepository
    {
        private readonly ApplicationDbContext _context;

        public CommunityTipRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CommunityTip>> GetTips()
        {
            return await _context.CommunityTips.AsNoTracking().ToListAsync();
        }

        public async Task<CommunityTip?> GetTip(int id)
        {
            return await _context.CommunityTips.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task AddTip(CommunityTip tip)
        {
            tip.SubmittedDate = DateTime.Now;
            _context.CommunityTips.Add(tip);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTip(CommunityTip tip)
        {
            var existingTip = await _context.CommunityTips.FindAsync(tip.Id);
            if (existingTip != null)
            {
                existingTip.Title = tip.Title;
                existingTip.Content = tip.Content;
                existingTip.SubmittedBy = tip.SubmittedBy;
                existingTip.SubmittedDate = tip.SubmittedDate;

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteTip(int id)
        {
            var tip = await _context.CommunityTips.FindAsync(id);
            if (tip != null)
            {
                _context.CommunityTips.Remove(tip);
                await _context.SaveChangesAsync();
            }
        }
    }
}
