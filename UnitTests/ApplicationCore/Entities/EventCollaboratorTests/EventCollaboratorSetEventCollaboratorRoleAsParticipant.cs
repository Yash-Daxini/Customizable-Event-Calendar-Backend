using Core.Entities;
using Core.Entities.Enums;

namespace UnitTests.ApplicationCore.Entities.EventCollaboratorTests;

public class EventCollaboratorSetEventCollaboratorRoleAsParticipant
{
    [Fact]
    public void SetEventCollaboratorRoleAsParticipantIfAlreadyParticipant()
    {
        EventCollaborator eventCollaborator = new()
        {
            EventCollaboratorRole = EventCollaboratorRole.Participant,
        };

        eventCollaborator.SetEventCollaboratorRoleAsParticipant();

        bool result = eventCollaborator.IsEventParticipant();

        Assert.True(result);
    }

    [Theory]
    [InlineData(EventCollaboratorRole.Organizer)]
    [InlineData(EventCollaboratorRole.Collaborator)]
    public void SetEventCollaboratorRoleAsParticipantIfNotParticipant(EventCollaboratorRole eventCollaboratorRole)
    {
        EventCollaborator eventCollaborator = new()
        {
            EventCollaboratorRole = eventCollaboratorRole,
        };

        eventCollaborator.SetEventCollaboratorRoleAsParticipant();

        bool result = eventCollaborator.IsEventParticipant();

        Assert.True(result);
    }
}
