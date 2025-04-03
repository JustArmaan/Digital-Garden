using System.Collections.Generic;
using System.Threading.Tasks;
using DigitalGarden.Models;

namespace MVCView.Models
{
    public interface IProfileRepository
    {
            Task<ApplicationUser?> GetProfileByUserId(string userId);
            Task<IEnumerable<ApplicationUser>> GetAllProfiles();
            Task<ApplicationUser?> GetProfileById(string id);
            Task AddProfile(ApplicationUser user);
            Task UpdateProfile(ApplicationUser user);
            Task DeleteProfile(string id);

    }
}
