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
        return participantRole switch
        {
            "organizer" => ParticipantRole.Organizer,
            "participant" => ParticipantRole.Participant,
            "collaborator" => ParticipantRole.Collaborator,
        };
    }

    private string MapEnumToParticipantRole(ParticipantRole participantRole)
    {
        return participantRole switch
        {
            ParticipantRole.Organizer => "organizer",
            ParticipantRole.Participant => "participant",
            ParticipantRole.Collaborator => "collaborator",
        };
    }

    private ConfirmationStatus MapConfirmationStatusToEnum(string confirmationStatus)
    {
        return confirmationStatus switch
        {
            "accept" => ConfirmationStatus.Accept,
            "reject" => ConfirmationStatus.Reject,
            "pending" => ConfirmationStatus.Pending,
            "maybe" => ConfirmationStatus.Maybe,
            "proposed" => ConfirmationStatus.Proposed,
            _ => ConfirmationStatus.Pending,
        };
    }

    private string MapEnumToConfirmationStatus(ConfirmationStatus confirmationStatus)
    {
        return confirmationStatus switch
        {
            ConfirmationStatus.Accept => "accept",
            ConfirmationStatus.Reject => "reject",
            ConfirmationStatus.Pending => "pending",
            ConfirmationStatus.Maybe => "maybe",
            ConfirmationStatus.Proposed => "proposed",
            _ => "pending",
        };
    }
}
