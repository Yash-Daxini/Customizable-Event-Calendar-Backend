using AutoMapper;
using Core.Domain.Models;
using WebAPI.Dtos;

namespace WebAPI.Profiles;

public class EventCollaboratorResponseDtoProfile : Profile
{
    public EventCollaboratorResponseDtoProfile()
    {
        CreateMap<EventCollaborator, EventCollaboratorResponseDto>()
            .ForMember(dest => dest.ConfirmationStatus, opt => opt.MapFrom(src => src.ConfirmationStatus))
            .ForMember(dest => dest.ParticipantRole, opt => opt.MapFrom(src => src.ParticipantRole));
    }
}
