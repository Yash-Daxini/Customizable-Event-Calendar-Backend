using AutoMapper;
using Core.Entities;
using Core.Entities.Enums;
using Infrastructure.Extensions;
using WebAPI.Dtos;

namespace WebAPI.Profiles;

public class EventCollaboratorRequestDtoProfile : Profile
{
    public EventCollaboratorRequestDtoProfile()
    {
        CreateMap<EventCollaboratorRequestDto, EventCollaborator>()
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => new User { Id = src.UserId }))
            .ForMember(dest => dest.ConfirmationStatus, opt => opt.MapFrom(src => src.ConfirmationStatus.ToEnum<ConfirmationStatus>()))
            .ForMember(dest => dest.EventCollaboratorRole, opt => opt.MapFrom(src => src.ParticipantRole.ToEnum<EventCollaboratorRole>()));
    }
}
