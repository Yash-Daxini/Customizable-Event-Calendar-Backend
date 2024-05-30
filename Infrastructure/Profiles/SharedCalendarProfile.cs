using AutoMapper;
using Core.Domain.Models;
using Infrastructure.DataModels;

namespace Infrastructure.Profiles;

public class SharedCalendarProfile : Profile
{
    public SharedCalendarProfile()
    {
        CreateMap<SharedCalendarDataModel, SharedCalendar>();
        CreateMap<SharedCalendar, SharedCalendarDataModel>()
            .ForMember(dest => dest.SenderId, opt => opt.MapFrom(src => src.Sender.Id))
            .ForMember(dest => dest.ReceiverId, opt => opt.MapFrom(src => src.Receiver.Id))
            .ForMember(dest => dest.Sender, opt => opt.Ignore())
            .ForMember(dest => dest.Receiver, opt => opt.Ignore());
    }
}
