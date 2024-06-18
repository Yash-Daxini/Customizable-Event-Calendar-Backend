using AutoMapper;
using Core.Entities.Enums;
using Infrastructure.DataModels;
using Infrastructure.Extensions;
using Core.Entities.RecurrecePattern;

namespace Infrastructure.Profiles;

public class SingleInstanceRecurrencePatternProfile : Profile
{
    public SingleInstanceRecurrencePatternProfile()
    {
        CreateMap<EventDataModel, SingleInstanceRecurrencePattern>()
            .ForMember(dest => dest.Frequency, opt => opt.MapFrom(src => Frequency.None))
            .ForMember(dest => dest.ByWeekDay, opt => opt.Equals(null));

        CreateMap<SingleInstanceRecurrencePattern, EventDataModel>()
            .ForMember(dest => dest.Frequency, opt => opt.Equals(null))
            .ForMember(dest => dest.ByWeekDay, opt => opt.Equals(null));
    }
}
