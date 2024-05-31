using AutoMapper;
using Core.Entities;
using WebAPI.Dtos;

namespace WebAPI.Profiles;

public class EventCollaboratorResponseDtoProfile : Profile
{
    public EventCollaboratorResponseDtoProfile()
    {
        CreateMap<EventCollaborator, EventCollaboratorResponseDto>()
            .ForMember(dest => dest.ConfirmationStatus, opt => opt.MapFrom(src => src.ConfirmationStatus))
            .ForMember(dest => dest.EventCollaboratorRole, opt => opt.MapFrom(src => src.EventCollaboratorRole));
    }
}
