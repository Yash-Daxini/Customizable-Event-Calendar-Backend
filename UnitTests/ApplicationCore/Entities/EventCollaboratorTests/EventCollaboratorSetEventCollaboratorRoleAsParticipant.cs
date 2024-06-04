using Core.Entities;
using Core.Entities.Enums;

namespace UnitTests.ApplicationCore.Entities.EventCollaboratorTests;

public class EventCollaboratorSetEventCollaboratorRoleAsParticipant
{
    [Fact]
    public void Should_SetEventCollaboratorRoleAsParticipant_When_EventCollaboratorRoleIsAlreadyParticipant()
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
    public void Should_SetEventCollaboratorRoleAsParticipant_When_EventCollaboratorRoleIsNotAlreadyParticipant(EventCollaboratorRole eventCollaboratorRole)
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
