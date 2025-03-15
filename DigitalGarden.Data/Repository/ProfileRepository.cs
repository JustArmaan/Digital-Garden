using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DigitalGarden.Data;
using Microsoft.EntityFrameworkCore;
using MVCView.Data;
using MVCView.Models;

namespace MVCView.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly ApplicationDbContext _context;

        public ProfileRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Profile>> GetAllProfiles()
        {
            return await _context.Profiles.AsNoTracking().ToListAsync();
        }

        public async Task<Profile?> GetProfile(int id)
        {
            return await _context.Profiles.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddProfile(Profile profile)
        {
            if (profile == null) throw new ArgumentNullException(nameof(profile));

            await _context.Profiles.AddAsync(profile);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProfile(Profile profile)
        {
            if (profile == null) throw new ArgumentNullException(nameof(profile));

            var existingProfile = await _context.Profiles.FindAsync(profile.Id);
            if (existingProfile == null) return; // Profile not found, do nothing

            _context.Entry(existingProfile).CurrentValues.SetValues(profile);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProfile(int id)
        {
            var profile = await _context.Profiles.FindAsync(id);
            if (profile != null)
            {
                _context.Profiles.Remove(profile);
                await _context.SaveChangesAsync();
            }
        }
    }
}
