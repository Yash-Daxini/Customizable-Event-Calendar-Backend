using AutoMapper;
using Core.Entities.Enums;
using Infrastructure.DataModels;
using Core.Entities.RecurrecePattern;

namespace Infrastructure.Profiles;

public class MonthlyRecurrencePatternProfile : Profile
{
    public MonthlyRecurrencePatternProfile()
    {
        CreateMap<EventDataModel, MonthlyRecurrencePattern>()
            .ForMember(dest => dest.Frequency, opt => opt.MapFrom(src => Frequency.Monthly))
            .ForMember(dest => dest.ByWeekDay, opt => opt.MapFrom(src => MapWeekDayIntoList(src.ByWeekDay)));
    }

    private List<int>? MapWeekDayIntoList(string? weekDay)
    {
        return weekDay == null
               ? null
               : [.. weekDay.Split(",").Select(int.Parse)];
    }
}
