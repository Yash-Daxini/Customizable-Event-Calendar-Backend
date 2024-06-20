using AutoMapper;
using Core.Entities;
using WebAPI.Dtos;

namespace WebAPI.Profiles;

public class EventResponseDtoProfile : Profile
{
    public EventResponseDtoProfile()
    {
        CreateMap<Event, EventResponseDto>()
            .ForMember(dest => dest.RecurrencePattern, opt => opt.MapFrom(src => src.RecurrencePattern))
            .ForMember(dest => dest.Occurrences,
                       opt => opt.MapFrom(src => src.EventCollaborators
                                                    .Select(eventCollaboratorByDate => eventCollaboratorByDate.EventDate)
                                                    .Distinct()));
    }
}
