namespace MVCView.Models
{
    public class MockProfileRepository : IProfileRepository
    {
        private List<Profile> _profiles;

        public MockProfileRepository()
        {
            _profiles = new List<Profile>()
            {
                new Profile() { Id = 1, Name = "Armaan", Bio = "Passionate about technology, gardening, and digital creativity. Always learning and growing! 🌱", Email = "Armaan@gmail.com", Location = "Vancouver, Canada", JoinedDate = DateTime.Now.AddYears(-1) },
                new Profile() { Id = 2, Name = "Alice", Bio = "Lover of all things creative, exploring new ideas and technologies!", Email = "alice@example.com", Location = "Toronto, Canada", JoinedDate = DateTime.Now.AddMonths(-6) },
                new Profile() { Id = 3, Name = "Bob", Bio = "Tech enthusiast and software developer.", Email = "bob@example.com", Location = "Montreal, Canada", JoinedDate = DateTime.Now.AddYears(-2) }
            };
        }

        public void AddProfile(Profile profile)
        {
            profile.Id = _profiles.Max(p => p.Id) + 1;
            _profiles.Add(profile);
        }

        public Profile GetProfile(int id)
        {
            return _profiles.FirstOrDefault(p => p.Id == id);
        }

        public void UpdateProfile(Profile profile)
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
        }

        public void DeleteProfile(int id)
        {
            var profile = _profiles.FirstOrDefault(p => p.Id == id);
            if (profile != null)
            {
                _profiles.Remove(profile);
            }
        }

        public IEnumerable<Profile> GetAllProfiles()
        {
            return _profiles;
        }
    }
}
