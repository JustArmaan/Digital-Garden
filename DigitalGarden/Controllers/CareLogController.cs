using Microsoft.AspNetCore.Mvc;
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

      public IActionResult Index()
        {
            var careLogs = _careLogRepository.GetCareLogs() ?? new List<CareLog>();
            return View(careLogs);
        }

        public IActionResult AddCareLog()
        {
            ViewBag.Plants = _plantRepository.GetPlants();
            return View();
        }

        [HttpPost]
        public IActionResult AddCareLog(int plantId, string careType, string notes, DateTime date)
        {
            var plant = _plantRepository.GetPlant(plantId);
            if (plant == null)
            {
                ViewBag.Plants = _plantRepository.GetPlants();
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

            _careLogRepository.AddCareLog(careLog);
            return RedirectToAction("Index");
        }



        public IActionResult EditCareLog(int id)
        {
            var careLog = _careLogRepository.GetCareLog(id);
            if (careLog == null)
            {
                return NotFound();
            }
            ViewBag.Plants = _plantRepository.GetPlants();
            return View(careLog);
        }

        [HttpPost]
        public IActionResult EditCareLog(CareLog careLog)
        {
            if (ModelState.IsValid)
            {
                _careLogRepository.UpdateCareLog(careLog);
                return RedirectToAction("Index");
            }
            ViewBag.Plants = _plantRepository.GetPlants();
            return View(careLog);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var careLog = _careLogRepository.GetCareLog(id);
            if (careLog == null)
            {
                return NotFound();
            }
            _careLogRepository.DeleteCareLog(id);
            return RedirectToAction("Index");
        }
    }
}
