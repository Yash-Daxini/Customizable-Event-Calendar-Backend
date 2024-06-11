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
             .ConstructUsing(src => new SharedCalendar(src.Id, 
                                                       new User { Id = src.SenderUserId },
                                                       new User { Id = src.ReceiverUserId },
                                                       src.FromDate,
                                                       src.ToDate));
    }
}
