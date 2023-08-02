using AutoMapper;
using VNWalks.Shared.EntityModels.SqlServer.DTO;
using VNWalks.Shared.EntityModels.SqlServer.EntityModels;

namespace VNWalks.Web.API.Mappings;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Region, RegionDTO>().ReverseMap();
        CreateMap<CreateRegionRequestDTO, Region>().ReverseMap();
        CreateMap<Walk, WalkDTO>().ReverseMap();
        CreateMap<CreateWalkRequestDTO, Walk>().ReverseMap();
        CreateMap<Difficulty, DifficultyDTO>().ReverseMap();
    }

}
