using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DigitalGarden.Models;

namespace MVCView.Models
{
    public class MockProfileRepository : IProfileRepository
    {
        private readonly List<ApplicationUser> _users;

        public MockProfileRepository()
        {
            _users = new List<ApplicationUser>()
            {
                new ApplicationUser() { Id = "user1", UserName = "Armaan", Email = "Armaan@gmail.com", FirstName = "Armaan", LastName = "Singh", Gender = "Male", City = "Vancouver", PostalCode = "V5K 0A1", DateRegistered = DateTime.Now.AddYears(-1), IsGardener = true, GardeningExperience = "Intermediate" },
                new ApplicationUser() { Id = "user2", UserName = "Alice", Email = "alice@example.com", FirstName = "Alice", LastName = "Johnson", Gender = "Female", City = "Toronto", PostalCode = "M5V 2N8", DateRegistered = DateTime.Now.AddMonths(-6), IsGardener = false, GardeningExperience = "None" },
                new ApplicationUser() { Id = "user3", UserName = "Bob", Email = "bob@example.com", FirstName = "Bob", LastName = "Smith", Gender = "Male", City = "Montreal", PostalCode = "H2X 1X7", DateRegistered = DateTime.Now.AddYears(-2), IsGardener = true, GardeningExperience = "Expert" }
            };
        }

        public async Task AddProfile(ApplicationUser user)
        {
            _users.Add(user);
            await Task.CompletedTask;
        }

        public async Task<ApplicationUser?> GetProfileById(string id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            return await Task.FromResult(user);
        }

        public async Task<ApplicationUser?> GetProfileByUserId(string userId)
        {
            var user = _users.FirstOrDefault(u => u.Id == userId);
            return await Task.FromResult(user);
        }

        public async Task UpdateProfile(ApplicationUser user)
        {
            var existingUser = _users.FirstOrDefault(u => u.Id == user.Id);
            if (existingUser != null)
            {
                existingUser.UserName = user.UserName;
                existingUser.Email = user.Email;
                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName;
                existingUser.Gender = user.Gender;
                existingUser.City = user.City;
                existingUser.PostalCode = user.PostalCode;
                existingUser.DateRegistered = user.DateRegistered;
                existingUser.IsGardener = user.IsGardener;
                existingUser.GardeningExperience = user.GardeningExperience;
            }
            await Task.CompletedTask;
        }

        public async Task DeleteProfile(string userId)
        {
            var user = _users.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                _users.Remove(user);
            }
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllProfiles()
        {
            return await Task.FromResult(_users);
        }
    }
}
