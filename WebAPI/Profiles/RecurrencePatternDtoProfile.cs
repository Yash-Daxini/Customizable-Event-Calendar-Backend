using AutoMapper;
using Core.Entities;
using WebAPI.Dtos;

namespace WebAPI.Profiles;

public class RecurrencePatternDtoProfile : Profile
{
    public RecurrencePatternDtoProfile()
    {
        CreateMap<RecurrencePattern, RecurrencePatternDto>();
    }
}
