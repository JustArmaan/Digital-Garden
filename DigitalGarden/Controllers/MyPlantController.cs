using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DigitalGarden.Models;
using MVCView.Models;
using System.Threading.Tasks;
using DigitalGarden.Filters;

namespace DigitalGarden.Controllers;

[VirtualDomFilter]
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
public async Task<IActionResult> AddPlant(Plant plant)
{
    if (ModelState.IsValid)
    {
        await _plantRepository.AddPlant(plant);
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
    public async Task<IActionResult> EditPlant(Plant plant)
    {
        if (ModelState.IsValid)
        {
            await _plantRepository.UpdatePlant(plant);
            return RedirectToAction("Index");
        }
        return View(plant);
    }


    [HttpPost]
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var plant = await _plantRepository.GetPlant(id);
        if (plant == null)
        {
            return NotFound();
        }

        await _plantRepository.DeletePlant(id);

        if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
        {
            return Ok();
        }

        return RedirectToAction("Index");
    }

        [HttpGet]
    public async Task<IActionResult> GetPlantDetails(int id)
    {
        var plants = await _plantRepository.GetPlants();
        var plant = plants.FirstOrDefault(p => p.Id == id);

        if (plant == null)
        {
            return NotFound();
        }

        return PartialView("_PlantDetails", plant);
    }

    [HttpGet]
    public async Task<IActionResult> GetPlantList()
    {
        var plants = await _plantRepository.GetPlants();
        return PartialView("_PlantList", plants);
    }

    public IActionResult SPA()
    {
        return View();
    }
}
