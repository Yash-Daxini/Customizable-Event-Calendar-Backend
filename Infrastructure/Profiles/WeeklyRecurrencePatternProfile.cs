using AutoMapper;
using Core.Entities.Enums;
using Infrastructure.DataModels;
using Infrastructure.Extensions;
using Core.Entities.RecurrecePattern;

namespace Infrastructure.Profiles;

public class WeeklyRecurrencePatternProfile : Profile
{
    public WeeklyRecurrencePatternProfile()
    {
        CreateMap<EventDataModel, WeeklyRecurrencePattern>()
            .ForMember(dest => dest.Frequency, opt => opt.MapFrom(src => Frequency.Weekly))
            .ForMember(dest => dest.ByWeekDay, opt => opt.MapFrom(src => MapWeekDayIntoList(src.ByWeekDay)));
    }

    private List<int>? MapWeekDayIntoList(string? weekDay)
    {
        return weekDay == null
               ? null
               : [.. weekDay.Split(",").Select(int.Parse)];
    }
}
