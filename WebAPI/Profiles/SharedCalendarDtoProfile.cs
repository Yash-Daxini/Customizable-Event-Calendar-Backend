using AutoMapper;
using Core.Domain.Models;
using WebAPI.Dtos;

namespace WebAPI.Profiles;

public class SharedCalendarDtoProfile : Profile
{
    public SharedCalendarDtoProfile()
    {
        CreateMap<SharedCalendar,SharedCalendarDto>();
        CreateMap<SharedCalendarDto,SharedCalendar>();
    }
}
