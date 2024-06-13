using AutoMapper;
using Core.Entities;
using Core.Entities.Enums;
using Infrastructure.Extensions;
using WebAPI.Dtos;

namespace WebAPI.Profiles;

public class RecurringEventRequestDtoProfile : Profile
{
    public RecurringEventRequestDtoProfile()
    {
        CreateMap<RecurringEventRequestDto, Event>()
            .ForMember(dest => dest.RecurrencePattern, opt => opt.MapFrom(src => MapRecurrencePatternFromEventDataModel(src.RecurrencePattern)))
            .ForMember(dest => dest.DateWiseEventCollaborators, opt => opt.MapFrom<RecurringEventDateWiseEventCollaboratorsResolver>());
    }

    private static List<int>? MapWeekDayIntoList(List<int>? byWeekDay)
    {
        return byWeekDay == null || byWeekDay.Count == 0 ? null : byWeekDay;
    }

    private Frequency MapFrequencyToEnum(string? frequency)
    {
        return frequency is null
               ? Frequency.None
               : frequency.ToEnum<Frequency>();
    }

    private RecurrencePattern MapRecurrencePatternFromEventDataModel(RecurrencePatternDto recurrencePatternDto)
    {
        Frequency frequency = MapFrequencyToEnum(recurrencePatternDto.Frequency);

        if (frequency == Frequency.Daily || frequency == Frequency.Weekly)
            return CreateDailyAndWeeklyRecurrencePattern(recurrencePatternDto, frequency);
        else if (frequency == Frequency.Monthly)
            return CreateMonthlyRecurrecePattern(recurrencePatternDto, frequency);
        else if (frequency == Frequency.Yearly)
            return CreateYearlyRecurrencePattern(recurrencePatternDto, frequency);
        else
            return CreateSingleInstanceRecurrencePattern(recurrencePatternDto, frequency);
    }

    private RecurrencePattern CreateSingleInstanceRecurrencePattern(RecurrencePatternDto recurrencePatternDto, Frequency frequency)
    {
        return new SingleInstanceRecurrencePattern()
        {
            StartDate = recurrencePatternDto.StartDate,
            EndDate = recurrencePatternDto.EndDate,
            Interval = recurrencePatternDto.Interval,
            Frequency = frequency,
            ByWeekDay = MapWeekDayIntoList(recurrencePatternDto.ByWeekDay)
        };
    }

    private RecurrencePattern CreateYearlyRecurrencePattern(RecurrencePatternDto recurrencePatternDto, Frequency frequency)
    {
        return new YearlyRecurrencePattern()
        {
            StartDate = recurrencePatternDto.StartDate,
            EndDate = recurrencePatternDto.EndDate,
            Frequency = frequency,
            Interval = recurrencePatternDto.Interval,
            ByWeekDay = MapWeekDayIntoList(recurrencePatternDto.ByWeekDay),
            ByMonthDay = recurrencePatternDto.ByMonthDay,
            WeekOrder = recurrencePatternDto.WeekOrder,
            ByMonth = recurrencePatternDto.ByMonth,
        };
    }

    private RecurrencePattern CreateMonthlyRecurrecePattern(RecurrencePatternDto recurrencePatternDto, Frequency frequency)
    {
        return new MonthlyRecurrencePattern()
        {
            StartDate = recurrencePatternDto.StartDate,
            EndDate = recurrencePatternDto.EndDate,
            Frequency = frequency,
            Interval = recurrencePatternDto.Interval,
            ByWeekDay = MapWeekDayIntoList(recurrencePatternDto.ByWeekDay),
            ByMonthDay = recurrencePatternDto.ByMonthDay,
            WeekOrder = recurrencePatternDto.WeekOrder
        };
    }

    private RecurrencePattern CreateDailyAndWeeklyRecurrencePattern(RecurrencePatternDto recurrencePatternDto, Frequency frequency)
    {
        return new DailyRecurrencePattern()
        {
            StartDate = recurrencePatternDto.StartDate,
            EndDate = recurrencePatternDto.EndDate,
            Frequency = frequency,
            Interval = recurrencePatternDto.Interval,
            ByWeekDay = MapWeekDayIntoList(recurrencePatternDto.ByWeekDay)
        };
    }
}
