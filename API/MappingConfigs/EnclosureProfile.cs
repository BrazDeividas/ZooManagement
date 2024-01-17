using AutoMapper;
using API.Models;
using API.DTO;
using System.Linq;

namespace API.MappingConfigs
{
    public class EnclosureProfile : Profile
    {
        public EnclosureProfile()
        {
            CreateMap<Enclosure, EnclosureSendDTO>()
            .ForMember(dest =>
                dest.Species,
                opt => opt.MapFrom(src => src.Species.Select(s => s.Name).ToList())
            )
            .ReverseMap();
        }
    }
}
