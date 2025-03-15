using System.Collections.Generic;
using System.Threading.Tasks;
using MVCView.Models;

namespace MVCView.Repositories
{
public interface ICommunityTipRepository
{
    Task<IEnumerable<CommunityTip>> GetTips();
    Task<CommunityTip?> GetTip(int id);
    Task AddTip(CommunityTip tip);
    Task UpdateTip(CommunityTip tip);
    Task DeleteTip(int id);
}

}
