using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCView.Data;
using MVCView.Models;

namespace DigitalGarden.Controllers
{
    public class CareLogController : Controller
    {
        private readonly ICareLogRepository _careLogRepository;
        private readonly IPlantRepository _plantRepository;

        public CareLogController(ICareLogRepository careLogRepository, IPlantRepository plantRepository)
        {
            _careLogRepository = careLogRepository;
            _plantRepository = plantRepository;
        }

      public async Task<IActionResult> Index()
        {
                var plants = await _plantRepository.GetPlants();
                ViewBag.Plants = plants;
                var careLogs = await _careLogRepository.GetCareLogs();
            return View(careLogs);
        }

        public async Task<IActionResult> AddCareLog()
        {
            ViewBag.Plants = await _plantRepository.GetPlants();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCareLog(int plantId, string careType, string notes, DateTime date)
        {
            var plant = await _plantRepository.GetPlant(plantId);
            if (plant == null)
            {
                ViewBag.Plants = await _plantRepository.GetPlants();
                ModelState.AddModelError("", "Invalid plant selection.");
                return View();
            }

            var careLog = new CareLog
            {
                PlantId = plantId,
                CareType = careType,
                Notes = notes,
                Date = date,
                Plant = plant
            };

            await _careLogRepository.AddCareLog(careLog);
            return RedirectToAction("Index");
        }



        public async Task<IActionResult> EditCareLog(int id)
        {
            var careLog = await _careLogRepository.GetCareLog(id);
            if (careLog == null)
            {
                return NotFound();
            }
            ViewBag.Plants = await _plantRepository.GetPlants();
            return View(careLog);
        }

        [HttpPost]
        public async Task<IActionResult> EditCareLog(CareLog careLog)
        {
            if (ModelState.IsValid)
            {
                await _careLogRepository.UpdateCareLog(careLog);
                return RedirectToAction("Index");
            }
            ViewBag.Plants = await _plantRepository.GetPlants();
            return View(careLog);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var careLog = await _careLogRepository.GetCareLog(id);
            if (careLog == null)
            {
                return NotFound();
            }
            await _careLogRepository.DeleteCareLog(id);
            return RedirectToAction("Index");
        }
    }
}
