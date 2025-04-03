using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVCView.Repositories;

namespace MVCView.Models
{
    public class MockCommunityTipRepository : ICommunityTipRepository
    {
        private readonly List<CommunityTip> _tips;

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

        public Task<IEnumerable<CommunityTip>> GetTips()
        {
            return Task.FromResult(_tips.AsEnumerable());
        }

        public Task<CommunityTip?> GetTip(int id)
        {
            return Task.FromResult(_tips.FirstOrDefault(t => t.Id == id));
        }

        public Task AddTip(CommunityTip tip)
        {
            tip.Id = _tips.Any() ? _tips.Max(t => t.Id) + 1 : 1;
            _tips.Add(tip);
            return Task.CompletedTask;
        }

        public Task UpdateTip(CommunityTip tip)
        {
            var existingTip = _tips.FirstOrDefault(t => t.Id == tip.Id);
            if (existingTip != null)
            {
                existingTip.Title = tip.Title;
                existingTip.Content = tip.Content;
                existingTip.SubmittedBy = tip.SubmittedBy;
                existingTip.SubmittedDate = tip.SubmittedDate;
            }
            return Task.CompletedTask;
        }

        public Task DeleteTip(int id)
        {
            var tip = _tips.FirstOrDefault(t => t.Id == id);
            if (tip != null)
            {
                _tips.Remove(tip);
            }
            return Task.CompletedTask;
        }
          public Task<IEnumerable<CommunityTip>> GetAllCommunityTips()
        {
            return Task.FromResult(_tips.AsEnumerable());
        }
         public Task DeleteCommunityTip(int id)
        {
            var tip = _tips.FirstOrDefault(t => t.Id == id);
            if (tip != null)
            {
                _tips.Remove(tip);
            }
            return Task.CompletedTask;
        }
    }
}
