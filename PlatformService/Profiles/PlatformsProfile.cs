using AutoMapper;
using Grpc;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Profiles
{
    public class PlatformsProfile : Profile
    {
        public PlatformsProfile()
        {
            CreateMap<Platform, PlatformReadDto>();
            CreateMap<PlatformCreateDto, Platform>();
            CreateMap<Platform, PlatformPublishedDto>();
            CreateMap<Platform, GrpcPlatform>()
                .ForMember(dest => dest.PlatformId, opt => opt.MapFrom(src => src.Id));
        }
    }
}
