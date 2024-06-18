using Core.Entities.Enums;
using Infrastructure.DataModels;
using AutoMapper;
using Core.Entities.RecurrecePattern;

namespace Infrastructure.Profiles;

public class YearlyRecurrencePatternProfile : Profile
{
    public YearlyRecurrencePatternProfile()
    {
        CreateMap<EventDataModel, YearlyRecurrencePattern>()
            .ForMember(dest => dest.Frequency, opt => opt.MapFrom(src => Frequency.Yearly))
            .ForMember(dest => dest.ByWeekDay, opt => opt.MapFrom(src => MapWeekDayIntoList(src.ByWeekDay)));
    }

    private List<int>? MapWeekDayIntoList(string? weekDay)
    {
        return weekDay == null
               ? null
               : [.. weekDay.Split(",").Select(int.Parse)];
    }
}
