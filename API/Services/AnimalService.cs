using API.Models;
using API.Repositories;

namespace API.Services
{
    public class AnimalService : IAnimalService
    {
        private readonly IRepository<Animal> _animalRepository;
        public AnimalService(IRepository<Animal> animalRepository)
        {
            _animalRepository = animalRepository;
        }

        
    }
}