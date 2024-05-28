using AutoMapper;
using Core.Domain;
using Infrastructure.DataModels;

namespace Infrastructure.Profiles;

public class SharedCalendarProfile : Profile
{
    public SharedCalendarProfile()
    {
        CreateMap<SharedCalendarDataModel, SharedCalendar>();
        CreateMap<SharedCalendar, SharedCalendarDataModel>()
            .ForMember(dest => dest.SenderUserId, opt => opt.MapFrom(src => src.SenderUser.Id))
            .ForMember(dest => dest.ReceiverUserId, opt => opt.MapFrom(src => src.ReceiverUser.Id))
            .ForMember(dest => dest.SenderUser, opt => opt.Ignore())
            .ForMember(dest => dest.ReceiverUser, opt => opt.Ignore());
    }
}
