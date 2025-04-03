using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DigitalGarden.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVCView.Models;

namespace MVCView.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfileRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ApplicationUser?> GetProfileByUserId(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllProfiles()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<ApplicationUser?> GetProfileById(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task AddProfile(ApplicationUser user)
        {
            await _userManager.CreateAsync(user);
        }

        public async Task UpdateProfile(ApplicationUser user)
        {
            var existingUser = await _userManager.FindByIdAsync(user.Id);
            if (existingUser == null) return;

            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Gender = user.Gender;
            existingUser.City = user.City;
            existingUser.PostalCode = user.PostalCode;
            existingUser.GardeningExperience = user.GardeningExperience;
            existingUser.IsGardener = user.IsGardener;

            if (existingUser.Email != user.Email)
            {
                existingUser.Email = user.Email;
                existingUser.NormalizedEmail = _userManager.NormalizeEmail(user.Email);
            }

            if (existingUser.UserName != user.UserName)
            {
                existingUser.UserName = user.UserName;
                existingUser.NormalizedUserName = _userManager.NormalizeName(user.UserName);
            }

            // Save changes
            await _userManager.UpdateAsync(existingUser);
        }

        public async Task DeleteProfile(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
        }
    }
}
