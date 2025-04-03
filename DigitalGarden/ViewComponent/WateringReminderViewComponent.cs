using Microsoft.AspNetCore.Mvc;
using MVCView.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalGarden.ViewComponents
{
    public class WateringReminderViewComponent : ViewComponent
    {
        private readonly IPlantRepository _plantRepository;

        public WateringReminderViewComponent(IPlantRepository plantRepository)
        {
            _plantRepository = plantRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync(int days = 3)
        {
            var plants = await _plantRepository.GetPlants();
            var plantsNeedingWater = plants
                .Where(p => (DateTime.Now - p.LastWatered).TotalDays >= p.WateringSchedule - days)
                .OrderBy(p => p.LastWatered)
                .ToList();

            return View(plantsNeedingWater);
        }
    }
}
