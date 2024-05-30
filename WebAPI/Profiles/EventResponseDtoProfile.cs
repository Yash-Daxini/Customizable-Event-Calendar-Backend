using AutoMapper;
using Core.Entities;
using WebAPI.Dtos;

namespace WebAPI.Profiles;

public class EventResponseDtoProfile : Profile
{
    public EventResponseDtoProfile()
    {
        CreateMap<Event, EventResponseDto>()
            .ForMember(dest => dest.Occurrences,
                       opt => opt.MapFrom(src => src.DateWiseEventCollaborators
                                                    .Select(eventCollaboratorByDate=>eventCollaboratorByDate.EventDate)));
    }
}
