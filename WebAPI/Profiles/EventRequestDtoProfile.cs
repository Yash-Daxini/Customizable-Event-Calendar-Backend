using AutoMapper;
using Core.Domain;
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
