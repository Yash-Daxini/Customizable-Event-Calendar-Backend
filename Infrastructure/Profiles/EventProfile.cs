using AutoMapper;
using Core.Entities.Enums;
using Infrastructure.DataModels;
using Infrastructure.Extensions;
using Core.Entities;

namespace Infrastructure.Profiles;

public class EventProfile : Profile
{
    public EventProfile()
    {
        CreateMap<EventDataModel, Event>()
                .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => new Duration(src.EventStartHour, src.EventEndHour)))
                .ForMember(dest => dest.RecurrencePattern, opt => opt.MapFrom(src => MapRecurrencePatternFromEventDataModel(src)))
                .ForMember(dest => dest.DateWiseEventCollaborators, opt => opt.MapFrom<DateWiseEventCollaboratorsResolver>());

        CreateMap<Event, EventDataModel>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.GetEventOrganizer().Id))
                .ForMember(dest => dest.EventStartHour, opt => opt.MapFrom(src => src.Duration.StartHour))
                .ForMember(dest => dest.EventEndHour, opt => opt.MapFrom(src => src.Duration.EndHour))
                .ForMember(dest => dest.EventStartDate, opt => opt.MapFrom(src => src.RecurrencePattern.StartDate))
                .ForMember(dest => dest.EventEndDate, opt => opt.MapFrom(src => src.RecurrencePattern.EndDate))
                .ForMember(dest => dest.Frequency, opt => opt.MapFrom(src => MapEnumToFrequency(src.RecurrencePattern.Frequency)))
                .ForMember(dest => dest.Interval, opt => opt.MapFrom(src => src.RecurrencePattern.Interval))
                .ForMember(dest => dest.ByWeekDay, opt => opt.MapFrom(src => MapWeekDayListToString(src.RecurrencePattern)))
                .ForMember(dest => dest.EventCollaborators, opt => opt.MapFrom<EventCollaboratorResolver>())
                .ForMember(dest => dest.ByMonthDay, opt => opt.MapFrom(src => MapMonthDay(src.RecurrencePattern)))
                .ForMember(dest => dest.WeekOrder, opt => opt.MapFrom(src => MapWeekOrder(src.RecurrencePattern)))
                .ForMember(dest => dest.ByMonth, opt => opt.MapFrom(src => MapMonth(src.RecurrencePattern)));
    }

    private int? MapMonthDay(dynamic recurrencePattern)
    {
        if (recurrencePattern.Frequency == Frequency.Monthly || recurrencePattern.Frequency == Frequency.Yearly)
            return recurrencePattern.ByMonthDay;

        return null;
    }

    private int? MapWeekOrder(dynamic recurrencePattern)
    {
        if (recurrencePattern.Frequency == Frequency.Monthly || recurrencePattern.Frequency == Frequency.Yearly)
            return recurrencePattern.WeekOrder;

        return null;
    }
    
    private int? MapMonth(dynamic recurrencePattern)
    {
        if (recurrencePattern.Frequency == Frequency.Yearly)
            return recurrencePattern.ByMonth;

        return null;
    }
    

    private RecurrencePattern MapRecurrencePatternFromEventDataModel(EventDataModel eventDataModel)
    {
        Frequency frequency = MapFrequencyToEnum(eventDataModel.Frequency);

        if (frequency == Frequency.Daily || frequency == Frequency.Weekly)
            return CreateDailyAndWeeklyRecurrencePattern(eventDataModel, frequency);
        else if (frequency == Frequency.Monthly)
            return CreateMonthlyRecurrecePattern(eventDataModel, frequency);
        else if (frequency == Frequency.Yearly)
            return CreateYearlyRecurrencePattern(eventDataModel, frequency);
        else
            return CreateSingleInstanceRecurrencePattern(eventDataModel, frequency);
    }

    private bool IsMonthlyRecurrencePattern(RecurrencePattern recurrencePattern)
    {
        return recurrencePattern.Frequency == Frequency.Monthly;
    }

    private bool IsYearlyRecurrencePattern(RecurrencePattern recurrencePattern)
    {
        return recurrencePattern.Frequency == Frequency.Yearly;
    }

    private RecurrencePattern CreateSingleInstanceRecurrencePattern(EventDataModel eventDataModel, Frequency frequency)
    {
        return new SingleInstanceRecurrencePattern()
        {
            StartDate = eventDataModel.EventStartDate,
            EndDate = eventDataModel.EventEndDate,
            Interval = eventDataModel.Interval,
            Frequency = frequency,
            ByWeekDay = MapWeekDayIntoList(eventDataModel.ByWeekDay)
        };
    }

    private RecurrencePattern CreateYearlyRecurrencePattern(EventDataModel eventDataModel, Frequency frequency)
    {
        return new YearlyRecurrencePattern()
        {
            StartDate = eventDataModel.EventStartDate,
            EndDate = eventDataModel.EventEndDate,
            Frequency = frequency,
            Interval = eventDataModel.Interval,
            ByWeekDay = MapWeekDayIntoList(eventDataModel.ByWeekDay),
            ByMonthDay = eventDataModel.ByMonthDay,
            WeekOrder = eventDataModel.WeekOrder,
            ByMonth = eventDataModel.ByMonth,
        };
    }

    private RecurrencePattern CreateMonthlyRecurrecePattern(EventDataModel eventDataModel, Frequency frequency)
    {
        return new MonthlyRecurrencePattern()
        {
            StartDate = eventDataModel.EventStartDate,
            EndDate = eventDataModel.EventEndDate,
            Frequency = frequency,
            Interval = eventDataModel.Interval,
            ByWeekDay = MapWeekDayIntoList(eventDataModel.ByWeekDay),
            ByMonthDay = eventDataModel.ByMonthDay,
            WeekOrder = eventDataModel.WeekOrder
        };
    }

    private RecurrencePattern CreateDailyAndWeeklyRecurrencePattern(EventDataModel eventDataModel, Frequency frequency)
    {
        return new DailyRecurrencePattern()
        {
            StartDate = eventDataModel.EventStartDate,
            EndDate = eventDataModel.EventEndDate,
            Frequency = frequency,
            Interval = eventDataModel.Interval,
            ByWeekDay = MapWeekDayIntoList(eventDataModel.ByWeekDay)
        };
    }

    private static string? MapWeekDayListToString(RecurrencePattern recurrencePattern)
    {
        return recurrencePattern.ByWeekDay == null || recurrencePattern.ByWeekDay.Count == 0 ? null : string.Join(",", recurrencePattern.ByWeekDay);
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

    private List<int>? MapWeekDayIntoList(string? weekDay)
    {
        return weekDay == null
               ? null
               : [.. weekDay.Split(",").Select(int.Parse)];
    }
}
