using AutoMapper;
using NZWalks_API.Models.Domain;
using NZWalks_API.Models.DTO;

namespace NZWalks_API.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<RegionCreateDto, Region>().ReverseMap();
            CreateMap<WalkCreateDto, Walk>().ReverseMap();
        }
    }
}
