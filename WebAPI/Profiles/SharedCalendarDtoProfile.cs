using AutoMapper;
using Core.Entities;
using WebAPI.Dtos;

namespace WebAPI.Profiles;

public class SharedCalendarDtoProfile : Profile
{
    public SharedCalendarDtoProfile()
    {
        CreateMap<SharedCalendar, SharedCalendarDto>();
        CreateMap<SharedCalendarDto, SharedCalendar>()
            .ForMember(dest => dest.Sender, opt => opt.MapFrom(src => new User { Id = src.SenderUserId }))
            .ForMember(dest => dest.Receiver, opt => opt.MapFrom(src => new User { Id = src.ReceiverUserId }));
    }
}
