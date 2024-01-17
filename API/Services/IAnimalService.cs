using API.DTO;
using API.Models;

namespace API.Services
{
    public interface IAnimalService
    {
        public Task<IEnumerable<Animal>> GetAll();
        public Task<IEnumerable<Animal>> GetAllWithRelatedData();
        public Task<Animal> GetById(int id);
        public Task<Animal> GetByIdWithRelatedData(int id);
        public Task Add(Animal Animal);
        public Task AddMany(List<AnimalsMultipleDTO> Animals);
        public Task Update(Animal Animal);
        public Task Delete(int id);
    }
}