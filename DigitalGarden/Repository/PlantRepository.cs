using DigitalGarden.Data;
using Microsoft.EntityFrameworkCore;
using MVCView.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCView.Data
{
    public class PlantRepository : IPlantRepository
    {
        private readonly ApplicationDbContext _context;

        public PlantRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Fetch all plants from the database
        public async Task<IEnumerable<Plant>> GetPlants()
        {
            return await _context.Plants.ToListAsync();
        }

        // Fetch a single plant by its ID
        public async Task<Plant> GetPlant(int id)
        {
            var plant = await _context.Plants
                .FirstOrDefaultAsync(p => p.Id == id);

            if (plant == null)
            {
                throw new KeyNotFoundException($"Plant with ID {id} not found.");
            }

            return plant;
        }

        // Add a new plant to the database
        public async Task AddPlant(Plant plant)
        {
            _context.Plants.Add(plant);
            await _context.SaveChangesAsync();
        }

        // Delete a plant by its ID
        public async Task DeletePlant(int id)
        {
            var plant = await _context.Plants
                .FirstOrDefaultAsync(p => p.Id == id);

            if (plant != null)
            {
                _context.Plants.Remove(plant);
                await _context.SaveChangesAsync();
            }
        }

        // Update an existing plant
        public async Task UpdatePlant(Plant plant)
        {
            var existingPlant = await _context.Plants
                .FirstOrDefaultAsync(p => p.Id == plant.Id);

            if (existingPlant != null)
            {
                existingPlant.Name = plant.Name;
                existingPlant.Species = plant.Species;
                existingPlant.WateringSchedule = plant.WateringSchedule;
                existingPlant.Sunlight = plant.Sunlight;
                existingPlant.Notes = plant.Notes;
                existingPlant.ImageUrl = plant.ImageUrl;
                existingPlant.CareInstructions = plant.CareInstructions;

                await _context.SaveChangesAsync();
            }
        }
    }
}
