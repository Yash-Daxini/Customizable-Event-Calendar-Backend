using AutoMapper;
using Core.Entities;
using WebAPI.Dtos;

namespace WebAPI.Profiles;

public class NonRecurringEventRequestDtoProfile : Profile
{
    public NonRecurringEventRequestDtoProfile()
    {
        CreateMap<NonRecurringEventRequestDto, Event>()
            .ForMember(dest => dest.RecurrencePattern, opt => opt.MapFrom<NonRecurringEventRecurrencePatternResolver>())
            .ForMember(dest => dest.DateWiseEventCollaborators, opt => opt.MapFrom<NonRecurringEventDateWiseEventCollaboratorsResolver>());
    }
}
