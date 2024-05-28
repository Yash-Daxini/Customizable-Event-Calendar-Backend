using AutoMapper;
using Core.Domain.Enums;
using Core.Domain;
using Infrastructure.DataModels;

namespace Infrastructure.Profiles;

public class EventProfile : Profile
{
    public EventProfile()
    {
        CreateMap<EventDataModel, Event>()
                .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => new Duration()
                {
                    StartHour = src.EventStartHour,
                    EndHour = src.EventEndHour,
                }))
                .ForMember(dest => dest.RecurrencePattern, opt => opt.MapFrom(src => new RecurrencePattern()
                {
                    StartDate = src.EventStartDate,
                    EndDate = src.EventEndDate,
                    Frequency = MapFrequencyToEnum(src.Frequency),
                    Interval = src.Interval,
                    ByWeekDay = MapWeekDayIntoList(src.ByWeekDay),
                    WeekOrder = src.WeekOrder,
                    ByMonth = src.ByMonth,
                    ByMonthDay = src.ByMonthDay,
                }))
                .ForMember(dest => dest.DateWiseParticipants, opt => opt.MapFrom<DateWiseParticipantsResolver>());

        CreateMap<Event, EventDataModel>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.GetEventOrganizer().Id))
                .ForMember(dest => dest.EventStartHour, opt => opt.MapFrom(src => src.Duration.StartHour))
                .ForMember(dest => dest.EventEndHour, opt => opt.MapFrom(src => src.Duration.EndHour))
                .ForMember(dest => dest.EventStartDate, opt => opt.MapFrom(src => src.RecurrencePattern.StartDate))
                .ForMember(dest => dest.EventEndDate, opt => opt.MapFrom(src => src.RecurrencePattern.EndDate))
                .ForMember(dest => dest.Frequency, opt => opt.MapFrom(src => MapEnumToFrequency(src.RecurrencePattern.Frequency)))
                .ForMember(dest => dest.Interval, opt => opt.MapFrom(src => src.RecurrencePattern.Interval))
                .ForMember(dest => dest.ByWeekDay, opt => opt.MapFrom(src => MapWeekDayListToString(src.RecurrencePattern)))
                .ForMember(dest => dest.ByMonthDay, opt => opt.MapFrom(src => src.RecurrencePattern.ByMonthDay))
                .ForMember(dest => dest.ByMonth, opt => opt.MapFrom(src => src.RecurrencePattern.ByMonth))
                .ForMember(dest => dest.EventCollaborators, opt => opt.MapFrom<EventCollaboratorResolver>());
    }

    private static string? MapWeekDayListToString(RecurrencePattern recurrencePattern)
    {
        return recurrencePattern.ByWeekDay == null || recurrencePattern.ByWeekDay.Count == 0 ? null : string.Join(",", recurrencePattern.ByWeekDay);
    }

    private Frequency MapFrequencyToEnum(string? frequency)
    {
        if(frequency is null) return Frequency.None;
        return Enum.Parse<Frequency>(frequency);
    }

    private string? MapEnumToFrequency(Frequency frequency)
    {
        return frequency == Frequency.None ? null : frequency.ToString();
    }

    private List<int>? MapWeekDayIntoList(string? weekDay)
    {
        if (weekDay == null) return null;
        return [.. weekDay.Split(",").Select(int.Parse)];
    }
}
