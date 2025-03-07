using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DigitalGarden.Models;
using MVCView.Models;
using System.Threading.Tasks;

namespace DigitalGarden.Controllers;

public class MyPlantController : Controller
{
    private readonly IPlantRepository _plantRepository;

    public MyPlantController(IPlantRepository plantRepository)
    {
        _plantRepository = plantRepository;
    }

    public async Task<IActionResult> Index()
    {
        var plants = await _plantRepository.GetPlants();
        return View(plants);
    }
    public IActionResult AddPlant()
    {
        return View();
    }

    [HttpPost]
    public IActionResult AddPlant(Plant plant)
    {
        if (ModelState.IsValid)
        {
                _plantRepository.AddPlant(new Plant
                {
                   Name = plant.Name,
                    Species = plant.Species,
                    WateringSchedule = plant.WateringSchedule,
                    Sunlight = plant.Sunlight,
                   Notes = plant.Notes,
                  ImageUrl = plant.ImageUrl,
                  CareInstructions = plant.CareInstructions
                });

                return RedirectToAction("Index");
        }
        return View(plant);
    }

 public async Task<IActionResult> EditPlant(int id)
{
    var plants = await _plantRepository.GetPlants();
    var plant = plants.FirstOrDefault(p => p.Id == id);

    if (plant == null)
    {
        return RedirectToAction("Index");
    }

    return View(plant);
}


    [HttpPost]
    public IActionResult EditPlant(Plant plant)
    {
        if (ModelState.IsValid)
        {
            _plantRepository.UpdatePlant(plant);
            return RedirectToAction("Index");
        }
        return View(plant);
    }


    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var plant = await _plantRepository.GetPlant(id);
        if (plant == null)
        {
            return NotFound();
        }

        await _plantRepository.DeletePlant(id);
        return RedirectToAction("Index");
    }


}
