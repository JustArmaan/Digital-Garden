using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVCView.Models
{
    public interface IProfileRepository
    {
        Task AddProfile(Profile profile);
        Task<Profile?> GetProfile(int id);
        Task UpdateProfile(Profile profile);
        Task DeleteProfile(int id);
        Task<IEnumerable<Profile>> GetAllProfiles();
    }
}
