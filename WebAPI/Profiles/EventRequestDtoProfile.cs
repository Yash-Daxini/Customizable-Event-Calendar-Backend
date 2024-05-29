using AutoMapper;
using Core.Domain.Models;
using WebAPI.Dtos;

namespace WebAPI.Profiles;

public class EventRequestDtoProfile : Profile
{
    public EventRequestDtoProfile()
    {
        CreateMap<EventRequestDto, Event>()
            .ForMember(dest => dest.DateWiseParticipants, opt => opt.MapFrom<DateWiseParticipantsResolver>());
    }
}
