using AutoMapper;
using Core.Domain.Enums;
using Core.Domain.Models;
using Infrastructure.Extensions;
using WebAPI.Dtos;

namespace WebAPI.Profiles;

public class EventCollaborationRequestDtoProfile : Profile
{
    public EventCollaborationRequestDtoProfile()
    {
        CreateMap<EventCollaborationRequestDto,EventCollaborator>()
            .ForMember(dest => dest.ConfirmationStatus, opt => opt.MapFrom(src => src.ConfirmationStatus.ToEnum<ConfirmationStatus>()))
            .ForMember(dest => dest.ParticipantRole, opt => opt.MapFrom(src => src.ParticipantRole.ToEnum<ParticipantRole>()));
    }
}
