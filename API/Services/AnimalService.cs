using API.DTO;
using API.Models;
using API.Repositories;
using AutoMapper;

namespace API.Services
{
    public class AnimalService : IAnimalService
    {
        private readonly IRepository<Animal> _animalRepository;
        private readonly IEnclosureService _enclosureService;
        private readonly IMapper _mapper;
        public AnimalService(IRepository<Animal> animalRepository, IEnclosureService enclosureService, IMapper mapper)
        {
            _animalRepository = animalRepository;
            _enclosureService = enclosureService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Animal>> GetAll()
        {
            return await _animalRepository.GetAll();
        }

        public async Task<IEnumerable<Animal>> GetAllWithRelatedData()
        {
            return await _animalRepository.GetAllWithRelatedDataAsync(a => a.Enclosure);
        }

        public async Task<Animal> GetById(int id)
        {
            return await _animalRepository.GetById(id);
        }

        public async Task<Animal> GetByIdWithRelatedData(int id)
        {
            return await _animalRepository.GetOneWithRelatedDataAsync(a => a.Id == id, a => a.Enclosure);
        }   

        public async Task Add(Animal animal)
        {
            var enclosure = await _enclosureService.AssignEnclosure(animal);
            if (enclosure == null)
            {
                throw new Exception("No enclosure available for this animal");
            }
            animal.Enclosure = enclosure;
            Console.WriteLine(animal.Enclosure);
            await _animalRepository.Add(animal);
        }

        public async Task AddMany(List<AnimalsMultipleDTO> animals)
        {
            List<Animal> animalsToAdd = new List<Animal>();
            foreach (var animalDTO in animals)
            {
                for (int i = 0; i < animalDTO.Amount; ++i)
                {
                    var newAnimal = new Animal
                    {
                        Species = animalDTO.Species,
                        Food = animalDTO.Food
                    };

                    var enclosure = await _enclosureService.AssignEnclosure(newAnimal);

                    if (enclosure == null)
                    {
                        throw new Exception("No enclosure available for this animal");
                    }

                    newAnimal.Enclosure = enclosure;

                    animalsToAdd.Add(newAnimal);
                }
            }
            await _animalRepository.AddRange(animalsToAdd);
        }

        public async Task Update(Animal animal)
        {
            await _animalRepository.Update(animal);
        }

        public async Task Delete(int id)
        {
            var animal = await _animalRepository.GetById(id);
            await _enclosureService.RemoveAnimal(animal);
            await _animalRepository.Delete(animal);
        }
    }
}