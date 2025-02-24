namespace MVCView.Models
{
    public class MockPlantRepository : IPlantRepository
    {
        private List<Plant> _plantList;

        public MockPlantRepository()
        {
            _plantList = new List<Plant>()
            {
                new Plant() { Id = 1, Name = "Aloe Vera", ImageUrl = "https://www.meadowsfarms.com/great-big-greenhouse-gardening-blog/wp-content/uploads/sites/2/2023/02/doug-blog-aloe-vera.jpg.webp", LastWatered = DateTime.Now.AddDays(-5), CareInstructions = "Water once every 3 weeks, likes bright indirect light." },
                new Plant() { Id = 2, Name = "Snake Plant", ImageUrl = "https://cdn.mos.cms.futurecdn.net/pNug7bBksRVsL54EEE5Wu9.jpg", LastWatered = DateTime.Now.AddDays(-3), CareInstructions = "Water when soil is dry, prefers low light." },
                new Plant() { Id = 3, Name = "Spider Plant", ImageUrl = "https://encrypted-tbn1.gstatic.com/images?q=tbn:ANd9GcRQst_HGF1VulUVdBe6312mPhmW-B5dOEZ0h1QvrZkqUYtrYexfYLpaxo5PvAwI2FnTfk7DsGoIpd2205ob9doGtw", LastWatered = DateTime.Now.AddDays(-2), CareInstructions = "Water once a week, needs indirect sunlight." },
                new Plant() { Id = 4, Name = "Monstera", ImageUrl = "https://frondlyplants.com/cdn/shop/files/Monstera_Deliciosa_XL.jpg?v=1739217493", LastWatered = DateTime.Now.AddDays(-10), CareInstructions = "Water when soil is dry, enjoys bright indirect light." },
                new Plant() { Id = 5, Name = "Ficus", ImageUrl = "https://paeonia-boutique.ca/cdn/shop/articles/bloomscape_ficus-altissima-std_stone.jpg?v=1699841649", LastWatered = DateTime.Now.AddDays(-7), CareInstructions = "Water once every 2 weeks, prefers bright indirect light." },
                new Plant() { Id = 6, Name = "Peace Lily", ImageUrl = "https://www.mydomaine.com/thmb/N3StDx3PyGbF0Pwafv-P9-qiNZU=/900x0/filters:no_upscale():strip_icc()/1566417254329_20190821-1566417255317-b9314f1d9f7a4668a466c5ffb1913a8f.jpg", LastWatered = DateTime.Now.AddDays(-1), CareInstructions = "Water once a week, low to medium light." },
                new Plant() { Id = 7, Name = "Cactus", ImageUrl = "https://hips.hearstapps.com/hmg-prod/images/thimble-cactus-royalty-free-image-1695063544.jpg?crop=1.00xw:0.834xh;0,0.115xh&resize=980:*", LastWatered = DateTime.Now.AddDays(-20), CareInstructions = "Water every 2-3 weeks, loves full sunlight." },
                new Plant() { Id = 8, Name = "Bamboo Palm", ImageUrl = "https://hips.hearstapps.com/hmg-prod/images/bamboo-plant-dracaena-sanderiana-in-white-flower-royalty-free-image-1714407490.jpg?crop=0.668xw:1.00xh;0.332xw,0&resize=640:*", LastWatered = DateTime.Now.AddDays(-15), CareInstructions = "Water once every 10 days, prefers indirect light." }
            };
        }

        public IEnumerable<Plant> GetPlants()
        {
            return _plantList;
        }

        public Plant GetPlant(int id)
        {
            return _plantList.FirstOrDefault(p => p.Id == id)!;
        }

        public void AddPlant(Plant plant)
        {
            plant.Id = _plantList.Max(e => e.Id) + 1;
            _plantList.Add(plant);
        }
        public void DeletePlant(int id)
        {
            Plant plant = _plantList.FirstOrDefault(p => p.Id == id);
            if (plant != null)
            {
                _plantList.Remove(plant);
            }
        }
        public void UpdatePlant(Plant plant)
        {
            var existingPlant = _plantList.FirstOrDefault(p => p.Id == plant.Id);
            if (existingPlant != null)
            {
                existingPlant.Name = plant.Name;
                existingPlant.Species = plant.Species;
                existingPlant.WateringSchedule = plant.WateringSchedule;
                existingPlant.Sunlight = plant.Sunlight;
                existingPlant.Notes = plant.Notes;
                existingPlant.ImageUrl = plant.ImageUrl;
                existingPlant.CareInstructions = plant.CareInstructions;
            }
            
        }


    }
}
