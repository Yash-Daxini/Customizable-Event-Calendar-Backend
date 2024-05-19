using Infrastructure.DataModels;
using Infrastructure.DomainEntities;
using Infrastructure.Enums;
using Infrastructure.Repositories;

namespace Infrastructure.Mappers;

public class ParticipantMapper
{
    public ParticipantModel MapEventCollaboratorToParticipantModel(EventCollaborator eventCollaborator)
    {
        return new ParticipantModel
        {
            Id = eventCollaborator.Id,
            ParticipantRole = MapParticipantRoleToEnum(eventCollaborator.ParticipantRole),
            ConfirmationStatus = MapConfirmationStatusToEnum(eventCollaborator.ConfirmationStatus),
            ProposedDuration = GetProposedDuration(eventCollaborator),
            EventDate = eventCollaborator.EventDate,
            User = MapUserIdToUserModel(eventCollaborator.UserId)
        };
    }

    private static DurationModel GetProposedDuration(EventCollaborator eventCollaborator)
    {
        return eventCollaborator.ProposedStartHour != null && eventCollaborator.ProposedEndHour != null
               ? new DurationModel()
               {
                   StartHour = (int)eventCollaborator.ProposedStartHour,
                   EndHour = (int)eventCollaborator.ProposedEndHour
               }
               : null;
    }

    public EventCollaborator MapParticipantModelToEventCollaborator(ParticipantModel participantModel, int eventId)
    {
        return new EventCollaborator
        {
            Id = participantModel.Id,
            EventId = eventId,
            UserId = participantModel.User.Id,
            ParticipantRole = MapEnumToParticipantRole(participantModel.ParticipantRole),
            ConfirmationStatus = MapEnumToConfirmationStatus(participantModel.ConfirmationStatus),
            ProposedStartHour = participantModel.ProposedDuration?.StartHour,
            ProposedEndHour = participantModel.ProposedDuration?.EndHour,
            EventDate = participantModel.EventDate,
        };
    }

    private UserModel MapUserIdToUserModel(int userId)
    {
        return new UserMapper().MapUserEntityToModel(new UserRepository().GetById(data => new User(data), userId));
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
