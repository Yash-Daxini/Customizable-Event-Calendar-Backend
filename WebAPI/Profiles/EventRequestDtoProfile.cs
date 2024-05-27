using AutoMapper;
using Core.Domain;
using WebAPI.Dtos;

namespace WebAPI.Profiles;

public class EventRequestDtoProfile : Profile
{
    public EventRequestDtoProfile()
    {
        CreateMap<Event, EventRequestDto>()
            .ForMember(dest => dest.Participants,opt => opt.MapFrom(src => src.GetInviteesOfEvent()));

        CreateMap<EventRequestDto, Event>()
            .ForMember(dest => dest.DateWiseParticipants, opt => opt.MapFrom<DateWiseParticipantsResolver>());
    }
}
