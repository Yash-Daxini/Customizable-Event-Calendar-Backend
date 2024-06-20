using AutoMapper;
using Core.Entities;
using Core.Entities.RecurrecePattern;
using WebAPI.Dtos;

namespace WebAPI.Profiles;

public class NonRecurringEventRequestDtoProfile : Profile
{
    public NonRecurringEventRequestDtoProfile()
    {
        CreateMap<NonRecurringEventRequestDto, Event>()
            .ForMember(dest => dest.RecurrencePattern, opt => opt.MapFrom(src => new SingleInstanceRecurrencePattern()
            {
                StartDate = src.StartDate,
                EndDate = src.EndDate,
                Frequency = Core.Entities.Enums.Frequency.None,
                Interval = 1,
                ByWeekDay = null,
            }))
            .ForMember(dest => dest.EventCollaborators, opt => opt.MapFrom(src => src.EventCollaborators));
    }
}
