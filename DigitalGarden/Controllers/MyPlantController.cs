using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DigitalGarden.Models;
using MVCView.Models;

namespace DigitalGarden.Controllers;

public class MyPlantController : Controller
{
    private readonly IPlantRepository _plantRepository;

    public MyPlantController(IPlantRepository plantRepository)
    {
        _plantRepository = plantRepository;
    }

    public IActionResult Index()
    {
        var plants = _plantRepository.GetPlants();
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

    public IActionResult EditPlant(int id)
    {
        var plant = _plantRepository.GetPlants().FirstOrDefault(p => p.Id == id);
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
    public IActionResult Delete(int id)
    {
        var plant = _plantRepository.GetPlant(id);
        if (plant == null)
        {
            return NotFound();
        }

        _plantRepository.DeletePlant(id);
        return RedirectToAction("Index");
    }


}
