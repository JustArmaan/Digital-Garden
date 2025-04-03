using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DigitalGarden.Models;
using MVCView.Models;
using DigitalGarden.Filters;

namespace DigitalGarden.Controllers.Api
{
    [Route("api/plants")]
    [ApiController]

    [VirtualDomFilter]
    public class PlantApiController : ControllerBase
    {
        private readonly IPlantRepository _plantRepository;

        public PlantApiController(IPlantRepository plantRepository)
        {
            _plantRepository = plantRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetPlants()
        {
            var plants = await _plantRepository.GetPlants();
            return Ok(plants);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlant(int id)
        {
            var plant = await _plantRepository.GetPlant(id);
            if (plant == null)
            {
                return NotFound();
            }
            return Ok(plant);
        }

        [HttpPost]
        public async Task<IActionResult> AddPlant(Plant plant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _plantRepository.AddPlant(plant);
            return CreatedAtAction(nameof(GetPlant), new { id = plant.Id }, plant);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlant(int id, Plant plant)
        {
            if (id != plant.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _plantRepository.UpdatePlant(plant);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlant(int id)
        {
            var plant = await _plantRepository.GetPlant(id);
            if (plant == null)
            {
                return NotFound();
            }

            await _plantRepository.DeletePlant(id);
            return NoContent();
        }
    }
}
