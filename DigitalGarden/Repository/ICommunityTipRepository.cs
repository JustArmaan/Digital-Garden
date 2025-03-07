using System.Collections.Generic;

namespace MVCView.Models
{
    public interface ICommunityTipRepository
    {
        IEnumerable<CommunityTip> GetTips();
        CommunityTip GetTip(int id);
        void AddTip(CommunityTip tip);
        void UpdateTip(CommunityTip tip);
        void DeleteTip(int id);
    }
}
