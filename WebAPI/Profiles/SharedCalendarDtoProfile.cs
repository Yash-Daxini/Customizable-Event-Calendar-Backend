using AutoMapper;
using Core.Entities;
using WebAPI.Dtos;

namespace WebAPI.Profiles;

public class SharedCalendarDtoProfile : Profile
{
    public SharedCalendarDtoProfile()
    {
        CreateMap<SharedCalendar, SharedCalendarDto>()
            .ForMember(dest => dest.SenderUserId, opt => opt.MapFrom(src => src.Sender.Id))
            .ForMember(dest => dest.ReceiverUserId, opt => opt.MapFrom(src => src.Receiver.Id));

        CreateMap<SharedCalendarDto, SharedCalendar>()
             .ConstructUsing(src => new SharedCalendar(src.Id,
                                                       new User { Id = src.SenderUserId },
                                                       new User { Id = src.ReceiverUserId },
                                                       src.FromDate,
                                                       src.ToDate));
    }
}
