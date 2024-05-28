using AutoMapper;
using Core.Domain;
using Core.Domain.Enums;
using WebAPI.Dtos;

namespace WebAPI.Profiles;

public class RecurrencePatternDtoProfile : Profile
{
    public RecurrencePatternDtoProfile()
    {
        CreateMap<RecurrencePattern, RecurrencePatternDto>()
            .ForMember(dest => dest.Frequency, opt => opt.MapFrom(src => MapEnumToFrequency(src.Frequency)))
            .ForMember(dest => dest.ByWeekDay, opt => opt.MapFrom(src => MapWeekDayList(src.ByWeekDay)));

        CreateMap<RecurrencePatternDto, RecurrencePattern>()
            .ForMember(dest => dest.Frequency, opt => opt.MapFrom(src => MapFrequencyToEnum(src.Frequency)))
            .ForMember(dest => dest.ByWeekDay, opt => opt.MapFrom(src => MapWeekDayList(src.ByWeekDay)));
    }

    private static List<int>? MapWeekDayList(List<int>? byWeekDay)
    {
        return byWeekDay == null || byWeekDay.Count == 0 ? null : byWeekDay;
    }

    private Frequency MapFrequencyToEnum(string frequency)
    {
        return Enum.Parse<Frequency>(frequency);
    }

    private string? MapEnumToFrequency(Frequency frequency)
    {
        return frequency.ToString();
    }
}
