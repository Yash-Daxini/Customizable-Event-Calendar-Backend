using AutoMapper;
using Core.Entities;
using Infrastructure.DataModels;

namespace Infrastructure.Profiles;

public class RecurrencePatternResolver : IValueResolver<EventDataModel, Event, RecurrencePattern>
{
    private readonly IMapper _mapper;

    public RecurrencePatternResolver(IMapper mapper)
    {
        _mapper = mapper;
    }

    public RecurrencePattern Resolve(EventDataModel source, Event destination, RecurrencePattern destMember, ResolutionContext context)
    {
        return source.Frequency switch
        {
            "Daily" => _mapper.Map<DailyRecurrencePattern>(source),
            "Weekly" => _mapper.Map<WeeklyRecurrencePattern>(source),
            "Monthly" => _mapper.Map<MonthlyRecurrencePattern>(source),
            "Yearly" => _mapper.Map<YearlyRecurrencePattern>(source),
            _ => _mapper.Map<SingleInstanceRecurrencePattern>(source),
        };
    }
}
