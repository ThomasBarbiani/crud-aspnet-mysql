using AutoMapper;

namespace NZWalksAPI.Profiles
{
    public class RegionsProfile : Profile
    {
        public RegionsProfile()
        {
            CreateMap<Models.Domain.Region, Models.DTO.Region>()
                .ReverseMap();

            CreateMap<Models.DTO.AddRegionRequest, Models.Domain.Region>()
                .ReverseMap();

            CreateMap<Models.DTO.UpdateRegionRequest, Models.Domain.Region>()
                .ReverseMap();
        }
    }
}
