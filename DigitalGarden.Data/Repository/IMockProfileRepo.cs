using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCView.Models
{
    public class MockProfileRepository : IProfileRepository
    {
        private readonly List<Profile> _profiles;

        public MockProfileRepository()
        {
            _profiles = new List<Profile>()
            {
                new Profile() { Id = 1, Name = "Armaan", Bio = "Passionate about technology, gardening, and digital creativity. Always learning and growing! 🌱", Email = "Armaan@gmail.com", Location = "Vancouver, Canada", JoinedDate = DateTime.Now.AddYears(-1) },
                new Profile() { Id = 2, Name = "Alice", Bio = "Lover of all things creative, exploring new ideas and technologies!", Email = "alice@example.com", Location = "Toronto, Canada", JoinedDate = DateTime.Now.AddMonths(-6) },
                new Profile() { Id = 3, Name = "Bob", Bio = "Tech enthusiast and software developer.", Email = "bob@example.com", Location = "Montreal, Canada", JoinedDate = DateTime.Now.AddYears(-2) }
            };
        }

        public async Task AddProfile(Profile profile)
        {
            profile.Id = _profiles.Any() ? _profiles.Max(p => p.Id) + 1 : 1;
            _profiles.Add(profile);
            await Task.CompletedTask;
        }

        public async Task<Profile?> GetProfile(int id)
        {
            return await Task.FromResult(_profiles.FirstOrDefault(p => p.Id == id));
        }

        public async Task UpdateProfile(Profile profile)
        {
            var existingProfile = _profiles.FirstOrDefault(p => p.Id == profile.Id);
            if (existingProfile != null)
            {
                existingProfile.Name = profile.Name;
                existingProfile.Bio = profile.Bio;
                existingProfile.Email = profile.Email;
                existingProfile.Location = profile.Location;
                existingProfile.JoinedDate = profile.JoinedDate;
            }
            await Task.CompletedTask;
        }

        public async Task DeleteProfile(int id)
        {
            var profile = _profiles.FirstOrDefault(p => p.Id == id);
            if (profile != null)
            {
                _profiles.Remove(profile);
            }
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<Profile>> GetAllProfiles()
        {
            return await Task.FromResult(_profiles);
        }
    }
}
