using AutoMapper;
using Core.Domain;
using WebAPI.Dtos;

namespace WebAPI.Profiles;

public class DurationDtoProfile : Profile
{
    public DurationDtoProfile()
    {
        CreateMap<Duration,DurationDto>();
        CreateMap<DurationDto,Duration>();
    }
}
