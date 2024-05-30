using AutoMapper;
using Core.Entities;
using WebAPI.Dtos;

namespace WebAPI.Profiles;

public class NonRecurringEventRecurrencePatternResolver : IValueResolver<NonRecurringEventRequestDto, Event, RecurrencePattern>
{
    public RecurrencePattern Resolve(NonRecurringEventRequestDto source, Event destination, RecurrencePattern destMember, ResolutionContext context)
    {
        return new RecurrencePattern()
        {
            StartDate = source.StartDate,
            EndDate = source.EndDate,
        };
    }
}
