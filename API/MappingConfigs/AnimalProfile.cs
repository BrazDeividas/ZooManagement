using AutoMapper;
using API.Models;
using API.DTO;
using System.Linq;

namespace API.MappingConfigs
{
    public class AnimalProfile : Profile
    {
        public AnimalProfile()
        {
            CreateMap<Animal, AnimalSendDTO>()
            .ForMember(dest => 
                dest.Enclosure, 
                opt => opt.MapFrom(src => 
                    src.Enclosure == null ? null : src.Enclosure.Name
                ))
            .ReverseMap();
            
            CreateMap<AnimalsMultipleDTO, Animal>();
        }
    }
}
