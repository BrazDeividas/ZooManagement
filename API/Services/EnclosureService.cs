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

        
    }
}