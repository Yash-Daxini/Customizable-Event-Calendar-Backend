using Core.Entities.Enums;
using Core.Entities;
using Infrastructure.DataModels;
using AutoMapper;

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
