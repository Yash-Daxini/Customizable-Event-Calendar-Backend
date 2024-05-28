using AutoMapper;
using Core.Domain;
using Core.Domain.Enums;
using WebAPI.Dtos;

namespace WebAPI.Profiles;

public class EventCollaboratorDtoProfile : Profile
{
    public EventCollaboratorDtoProfile()
    {
        CreateMap<EventCollaborator, EventCollaboratorDto>()
            .ForMember(dest => dest.ConfirmationStatus, opt => opt.MapFrom(src => MapEnumToConfirmationStatus(src.ConfirmationStatus)))
            .ForMember(dest => dest.ParticipantRole, opt => opt.MapFrom(src => MapEnumToParticipantRole(src.ParticipantRole)));

        CreateMap<EventCollaboratorDto, EventCollaborator>()
            .ForMember(dest => dest.ConfirmationStatus, opt => opt.MapFrom(src => MapConfirmationStatusToEnum(src.ConfirmationStatus)))
            .ForMember(dest => dest.ParticipantRole, opt => opt.MapFrom(src => MapParticipantRoleToEnum(src.ParticipantRole)));
    }

    private ParticipantRole MapParticipantRoleToEnum(string participantRole)
    {
        return Enum.Parse<ParticipantRole>(participantRole);
    }

    private string MapEnumToParticipantRole(ParticipantRole participantRole)
    {
        return participantRole.ToString();
    }

    private ConfirmationStatus MapConfirmationStatusToEnum(string confirmationStatus)
    {
        return Enum.Parse<ConfirmationStatus>(confirmationStatus);
    }

    private string MapEnumToConfirmationStatus(ConfirmationStatus confirmationStatus)
    {
        return confirmationStatus.ToString();
    }
}
