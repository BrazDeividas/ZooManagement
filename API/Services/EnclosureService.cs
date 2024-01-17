using API.Models;
using API.Repositories;

namespace API.Services
{
    public class EnclosureService : IEnclosureService
    {
        private readonly IRepository<Enclosure> _enclosureRepository;
        public EnclosureService(IRepository<Enclosure> enclosureRepository)
        {
            _enclosureRepository = enclosureRepository;
        }

        public async Task<IEnumerable<Enclosure>> GetAll()
        {
            return await _enclosureRepository.GetAll();
        }

        public async Task<IEnumerable<Enclosure>> GetAllWithRelatedData()
        {
            return await _enclosureRepository.GetAllWithRelatedDataAsync(e => e.Animals, e => e.Species);
        }

        public async Task<Enclosure> GetById(int id)
        {
            return await _enclosureRepository.GetById(id);
        }

        public async Task<Enclosure> GetByIdWithRelatedData(int id)
        {
            return await _enclosureRepository.GetOneWithRelatedDataAsync(e => e.Id == id, e => e.Animals, e => e.Species);
        }   

        public async Task Add(Enclosure enclosure)
        {
            enclosure.MaxCapacity = MatchSize(enclosure.Size);
            await _enclosureRepository.Add(enclosure);
        }

        public async Task AddMany(List<Enclosure> enclosures)
        {
            foreach(var enclosure in enclosures)
            {
                enclosure.MaxCapacity = MatchSize(enclosure.Size);
                await _enclosureRepository.Add(enclosure);
            }
        }

        public async Task Update(Enclosure enclosure)
        {
            await _enclosureRepository.Update(enclosure);
        }
        
        public async Task Delete(int id)
        {
            var enclosure = await _enclosureRepository.GetById(id);
            await _enclosureRepository.Delete(enclosure);
        }

        public async Task<Enclosure> AssignEnclosure(Animal animal)
        {
            var enclosures = await _enclosureRepository.GetAll();

            //same species enclosures
            var sameSpeciesEnclosure = enclosures.FirstOrDefault(e => 
            e.Animals.Any(a => a.Species == animal.Species) &&
            e.Animals.Count + 1 <= e.MaxCapacity);
            
            if(sameSpeciesEnclosure != null)
            {
                sameSpeciesEnclosure.Animals.Add(animal);
                sameSpeciesEnclosure.CurrentCapacity += 1;
                await _enclosureRepository.Update(sameSpeciesEnclosure);
                return sameSpeciesEnclosure;
            }

            //Herbivore type enclosures
            if(animal.Food == "Herbivore")
            {
                var herbivoreEnclosure = enclosures.FirstOrDefault(e => 
                e.Animals.Any(a => a.Food == "Herbivore") &&
                e.Animals.Count + 1 <= e.MaxCapacity);

                if(herbivoreEnclosure != null)
                {
                    herbivoreEnclosure.Animals.Add(animal);
                    herbivoreEnclosure.CurrentCapacity += 1;
                    var species = herbivoreEnclosure.Species.Find(s => s.Name == animal.Species);
                    if (species == null)
                    {
                        herbivoreEnclosure.Species.Add(new Species { Name = animal.Species });
                    }
                    await _enclosureRepository.Update(herbivoreEnclosure);
                    return herbivoreEnclosure;
                }
            }

            //empty enclosures
            var emptyEnclosure = enclosures.FirstOrDefault(e =>
            e.Animals.Count == 0 &&
            e.MaxCapacity >= 1);

            if (emptyEnclosure != null)
            {
                emptyEnclosure.Animals.Add(animal);
                emptyEnclosure.CurrentCapacity += 1;
                var species = emptyEnclosure.Species.Find(s => s.Name == animal.Species);
                if (species == null)
                    {
                        emptyEnclosure.Species.Add(new Species { Name = animal.Species });
                    }
                await _enclosureRepository.Update(emptyEnclosure);
                return emptyEnclosure;
            }

            //carnivores together exception
            if(animal.Food == "Carnivore")
            {
                var carnivoreEnclosure = enclosures.FirstOrDefault(e =>
                e.Species.Count == 1 &&
                e.Animals.Any(a => a.Food == "Carnivore") &&
                e.Animals.Count + 1 <= e.MaxCapacity);

                if(carnivoreEnclosure != null)
                {
                    carnivoreEnclosure.Animals.Add(animal);
                    carnivoreEnclosure.CurrentCapacity += 1;
                    carnivoreEnclosure.Species.Add(new Species { Name = animal.Species });
                    await _enclosureRepository.Update(carnivoreEnclosure);
                    return carnivoreEnclosure;
                }
            }

            throw new Exception("No suitable enclosure found");
        }

        public int MatchSize(string size)
        {
            switch(size)
            {
                case "Small":
                    return 5;
                case "Medium":
                    return 8;
                case "Large":
                    return 10;
                case "Huge":
                    return 15;
                default:
                    return 0;
            }
        }

        public async Task RemoveAnimal(Animal animal)
        {
            var enclosure = _enclosureRepository.GetAll().Result.FirstOrDefault(e => e.Animals.Contains(animal));
            
            if(enclosure != null)
            {
                enclosure.Animals.Remove(animal);
                enclosure.CurrentCapacity -= 1;
                await Update(enclosure);
                return;  
            }
        }
    }
}