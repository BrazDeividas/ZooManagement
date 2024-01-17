using API.Models;

namespace API.Services
{
    public interface IEnclosureService
    {
        public Task<IEnumerable<Enclosure>> GetAll();
        public Task<IEnumerable<Enclosure>> GetAllWithRelatedData();
        public Task<Enclosure> GetById(int id);
        public Task<Enclosure> GetByIdWithRelatedData(int id);
        public Task Add(Enclosure enclosure);
        public Task AddMany(List<Enclosure> enclosures);
        public Task Update(Enclosure enclosure);
        public Task Delete(int id);
        public Task<Enclosure> AssignEnclosure(Animal animal);
        public int MatchSize(string size);
        public Task RemoveAnimal(Animal animal);
    }
}