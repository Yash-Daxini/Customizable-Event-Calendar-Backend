using AutoMapper;
using Core.Domain;
using Core.Domain.Enums;
using Infrastructure.Extensions;
using WebAPI.Dtos;

namespace WebAPI.Profiles;

public class EventCollaboratorDtoProfile : Profile
{
    public EventCollaboratorDtoProfile()
    {
        CreateMap<EventCollaborator, EventCollaboratorDto>()
            .ForMember(dest => dest.ConfirmationStatus, opt => opt.MapFrom(src => src.ConfirmationStatus))
            .ForMember(dest => dest.ParticipantRole, opt => opt.MapFrom(src => src.ParticipantRole));

        CreateMap<EventCollaboratorDto, EventCollaborator>()
            .ForMember(dest => dest.ConfirmationStatus, opt => opt.MapFrom(src => src.ConfirmationStatus.ToEnum<ConfirmationStatus>()))
            .ForMember(dest => dest.ParticipantRole, opt => opt.MapFrom(src => src.ParticipantRole.ToEnum<ParticipantRole>()));
    }
}
