using AutoMapper;
using Core.Entities;
using Core.Entities.Enums;
using Infrastructure.DataModels;

namespace Infrastructure.Profiles;

public class DailyRecurrencePatternProfile : Profile
{
    public DailyRecurrencePatternProfile()
    {
        CreateMap<EventDataModel, DailyRecurrencePattern>()
            .ForMember(dest => dest.Frequency, opt => opt.MapFrom(src => Frequency.Daily))
            .ForMember(dest => dest.ByWeekDay, opt => opt.MapFrom(src => MapWeekDayIntoList(src.ByWeekDay)));
    }

    private List<int>? MapWeekDayIntoList(string? weekDay)
    {
        return weekDay == null
               ? null
               : [.. weekDay.Split(",").Select(int.Parse)];
    }
}
