using AutoMapper;
using Core.Entities;
using Core.Entities.Enums;
using Infrastructure.DataModels;
using Infrastructure.Extensions;

namespace Infrastructure.Profiles;

public class DailyRecurrencePatternProfile : Profile
{
    public DailyRecurrencePatternProfile()
    {
        CreateMap<EventDataModel, DailyRecurrencePattern>()
            .ForMember(dest => dest.Frequency, opt => opt.MapFrom(src => MapFrequencyToEnum(src.Frequency)))
            .ForMember(dest => dest.ByWeekDay, opt => opt.MapFrom(src => MapWeekDayIntoList(src.ByWeekDay)));

        CreateMap<DailyRecurrencePattern,EventDataModel>()
            .ForMember(dest => dest.Frequency, opt => opt.MapFrom(src => MapEnumToFrequency(src.Frequency)))
            .ForMember(dest => dest.ByWeekDay, opt => opt.MapFrom(src => MapWeekDayListToString(src.ByWeekDay)));
    }

    private List<int>? MapWeekDayIntoList(string? weekDay)
    {
        return weekDay == null
               ? null
               : [.. weekDay.Split(",").Select(int.Parse)];
    }

    private Frequency MapFrequencyToEnum(string? frequency)
    {
        return frequency is null
               ? Frequency.None
               : frequency.ToEnum<Frequency>();
    }

    private string? MapEnumToFrequency(Frequency frequency)
    {
        return frequency == Frequency.None
               ? null
               : frequency.ToString();
    }

    private static string? MapWeekDayListToString(List<int>? weekDay)
    {
        return weekDay == null || weekDay.Count == 0 
               ? null 
               : string.Join(",", weekDay);
    }
}
