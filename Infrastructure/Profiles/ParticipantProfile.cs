using AutoMapper;
using Core.Domain;
using Core.Domain.Enums;
using Infrastructure.DataModels;

namespace Infrastructure.Profiles;

public class ParticipantProfile : Profile
{

    public ParticipantProfile()
    {
        CreateMap<EventCollaboratorDataModel, EventCollaborator>()
            .ForMember(dest => dest.ParticipantRole, opt => opt.MapFrom(src => MapParticipantRoleToEnum(src.ParticipantRole)))
            .ForMember(dest => dest.ConfirmationStatus, opt => opt.MapFrom(src => MapConfirmationStatusToEnum(src.ConfirmationStatus)))
            .ForMember(dest => dest.ProposedDuration, opt => opt.MapFrom(src => MapDuration(src.ProposedStartHour, src.ProposedEndHour)))
            .ForMember(dest => dest.EventDate, opt => opt.MapFrom(src => src.EventDate));

        CreateMap<EventCollaborator, EventCollaboratorDataModel>()
            .ForMember(dest => dest.ParticipantRole, opt => opt.MapFrom(src => MapEnumToParticipantRole(src.ParticipantRole)))
            .ForMember(dest => dest.ConfirmationStatus, opt => opt.MapFrom(src => MapEnumToConfirmationStatus(src.ConfirmationStatus)))
            .ForMember(dest => dest.ProposedStartHour, opt => opt.MapFrom(src => MapProposedStartHour(src.ProposedDuration)))
            .ForMember(dest => dest.ProposedEndHour, opt => opt.MapFrom(src => MapProposedEndHour(src.ProposedDuration)))
            .ForMember(dest => dest.EventDate, opt => opt.MapFrom(src => src.EventDate))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.Id));
    }

    private static object? MapProposedStartHour(Duration proposedDuration)
    {
        return proposedDuration?.StartHour;
    }

    private static object? MapProposedEndHour(Duration proposedDuration)
    {
        return proposedDuration?.EndHour;
    }

    private static Duration? MapDuration(int? startHour, int? endHour)
    {
        if (startHour is null || endHour is null) return null;

        return new Duration
        {
            StartHour = (int)startHour,
            EndHour = (int)endHour
        };
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
