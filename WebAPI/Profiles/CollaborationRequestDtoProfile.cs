using AutoMapper;
using Core.Entities;
using Core.Entities.Enums;
using WebAPI.Dtos;

namespace WebAPI.Profiles;

public class CollaborationRequestDtoProfile : Profile
{
    public CollaborationRequestDtoProfile()
    {
        CreateMap<CollaborationRequestDto, EventCollaborator>()
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => new User { Id = src.UserId }))
            .ForMember(dest => dest.ConfirmationStatus, opt => opt.MapFrom(src => ConfirmationStatus.Accept))
            .ForMember(dest => dest.EventCollaboratorRole, opt => opt.MapFrom(src => EventCollaboratorRole.Collaborator));
    }
}
