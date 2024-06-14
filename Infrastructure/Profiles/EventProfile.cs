using AutoMapper;
using Core.Entities.Enums;
using Infrastructure.DataModels;
using Core.Entities;

namespace Infrastructure.Profiles;

public class EventProfile : Profile
{
    public EventProfile()
    {
        CreateMap<EventDataModel, Event>()
                .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => new Duration(src.StartHour, src.EndHour)))
                .ForMember(dest => dest.RecurrencePattern, opt => opt.MapFrom<RecurrencePatternResolver>())
                .ForMember(dest => dest.DateWiseEventCollaborators, opt => opt.MapFrom<DateWiseEventCollaboratorsResolver>());

        CreateMap<Event, EventDataModel>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.GetEventOrganizer().Id))
                .ForMember(dest => dest.StartHour, opt => opt.MapFrom(src => src.Duration.StartHour))
                .ForMember(dest => dest.EndHour, opt => opt.MapFrom(src => src.Duration.EndHour))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.RecurrencePattern.StartDate))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.RecurrencePattern.EndDate))
                .ForMember(dest => dest.Frequency, opt => opt.MapFrom(src => MapEnumToFrequency(src.RecurrencePattern.Frequency)))
                .ForMember(dest => dest.Interval, opt => opt.MapFrom(src => src.RecurrencePattern.Interval))
                .ForMember(dest => dest.ByWeekDay, opt => opt.MapFrom(src => MapWeekDayListToString(src.RecurrencePattern.ByWeekDay)))
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

    private static string? MapWeekDayListToString(List<int>? weekDay)
    {
        return weekDay == null || weekDay.Count == 0 
               ? null 
               : string.Join(",", weekDay);
    }

    private string? MapEnumToFrequency(Frequency frequency)
    {
        return frequency == Frequency.None
               ? null
               : frequency.ToString();
    }
}
