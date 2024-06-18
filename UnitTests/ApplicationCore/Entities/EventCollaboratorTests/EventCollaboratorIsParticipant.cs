using Core.Entities.Enums;
using Core.Entities;

namespace UnitTests.ApplicationCore.Entities.EventCollaboratorTests;

public class EventCollaboratorIsParticipant
{
    [Fact]
    public void Should_ReturnsTrue_When_EventCollaboratorIsParticipant()
    {
        EventCollaborator eventCollaborator = new()
        {
            EventCollaboratorRole = EventCollaboratorRole.Participant,
        };

        bool result = eventCollaborator.IsParticipant();

        Assert.True(result);
    }

    [Theory]
    [InlineData(EventCollaboratorRole.Organizer)]
    [InlineData(EventCollaboratorRole.Collaborator)]
    public void Should_ReturnsFalse_When_EventCollaboratorIsNotParticipant(EventCollaboratorRole eventCollaboratorRole)
    {
        EventCollaborator eventCollaborator = new()
        {
            EventCollaboratorRole = eventCollaboratorRole,
        };

        bool result = eventCollaborator.IsParticipant();

        Assert.False(result);
    }
}
