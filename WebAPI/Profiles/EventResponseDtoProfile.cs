using AutoMapper;
using Core.Domain.Models;
using WebAPI.Dtos;

namespace WebAPI.Profiles;

public class EventResponseDtoProfile : Profile
{
    public EventResponseDtoProfile()
    {
        CreateMap<Event, EventResponseDto>()
            .ForMember(dest => dest.Occurrences,
                       opt => opt.MapFrom(src => src.DateWiseParticipants
                                                    .Select(participantByDate=>participantByDate.EventDate)));
    }
}
