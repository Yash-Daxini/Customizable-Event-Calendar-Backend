using AutoMapper;
using Core.Entities;
using Core.Entities.Enums;
using Infrastructure.Extensions;
using WebAPI.Dtos;

namespace WebAPI.Profiles;

public class RecurrencePatternDtoProfile : Profile
{
    public RecurrencePatternDtoProfile()
    {
        CreateMap<RecurrencePattern, RecurrencePatternDto>()
            .ForMember(dest => dest.Frequency, opt => opt.MapFrom(src => src.Frequency))
            .ForMember(dest => dest.ByWeekDay, opt => opt.MapFrom(src => MapWeekDayList(src.ByWeekDay)));

        CreateMap<RecurrencePatternDto, RecurrencePattern>()
            .ForMember(dest => dest.Frequency, opt => opt.MapFrom(src => src.Frequency.ToEnum<Frequency>()))
            .ForMember(dest => dest.ByWeekDay, opt => opt.MapFrom(src => MapWeekDayList(src.ByWeekDay)));
    }

    private static List<int>? MapWeekDayList(List<int>? byWeekDay)
    {
        return byWeekDay == null || byWeekDay.Count == 0 ? null : byWeekDay;
    }
}
