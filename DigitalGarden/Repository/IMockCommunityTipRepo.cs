using System;
using System.Collections.Generic;
using System.Linq;

namespace MVCView.Models
{
    public class MockCommunityTipRepository : ICommunityTipRepository
    {
        private List<CommunityTip> _tips;

        public MockCommunityTipRepository()
        {
            _tips = new List<CommunityTip>()
            {
                new CommunityTip()
                {
                    Id = 1,
                    Title = "💧 Water in the Morning",
                    Content = "Watering plants early in the morning helps reduce evaporation and gives them time to absorb moisture before the heat of the day.",
                    SubmittedBy = "Alex Green",
                    SubmittedDate = DateTime.Now.AddDays(-1)
                },
                new CommunityTip()
                {
                    Id = 2,
                    Title = "🌞 Rotate Indoor Plants",
                    Content = "To ensure even growth, rotate your indoor plants every few weeks so they get sunlight on all sides.",
                    SubmittedBy = "Mia Flores",
                    SubmittedDate = DateTime.Now.AddDays(-3)
                },
                new CommunityTip()
                {
                    Id = 3,
                    Title = "🐛 Use Coffee Grounds for Soil",
                    Content = "Sprinkling used coffee grounds around plants adds nitrogen to the soil and helps deter pests.",
                    SubmittedBy = "Sam Leaf",
                    SubmittedDate = DateTime.Now.AddDays(-5)
                }
            };
        }

        public IEnumerable<CommunityTip> GetTips()
        {
            return _tips;
        }

        public CommunityTip GetTip(int id)
        {
            return _tips.FirstOrDefault(t => t.Id == id);
        }

        public void AddTip(CommunityTip tip)
        {
            tip.Id = _tips.Any() ? _tips.Max(t => t.Id) + 1 : 1;
            _tips.Add(tip);
        }

        public void UpdateTip(CommunityTip tip)
        {
            var existingTip = _tips.FirstOrDefault(t => t.Id == tip.Id);
            if (existingTip != null)
            {
                existingTip.Title = tip.Title;
                existingTip.Content = tip.Content;
                existingTip.SubmittedBy = tip.SubmittedBy;
                existingTip.SubmittedDate = tip.SubmittedDate;
            }
        }

        public void DeleteTip(int id)
        {
            var tip = _tips.FirstOrDefault(t => t.Id == id);
            if (tip != null)
            {
                _tips.Remove(tip);
            }
        }
    }
}
