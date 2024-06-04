using Core.Entities.Enums;
using Core.Entities;

namespace UnitTests.ApplicationCore.Entities.EventCollaboratorTests;

public class EventCollaboratorIsEventOrganizer
{
    [Fact]
    public void Should_ReturnsTrue_When_EventCollaboratorIsOrganizer()
    {
        EventCollaborator eventCollaborator = new()
        {
            EventCollaboratorRole = EventCollaboratorRole.Organizer,
        };

        bool result = eventCollaborator.IsEventOrganizer();

        Assert.True(result);
    }

    [Theory]
    [InlineData(EventCollaboratorRole.Participant)]
    [InlineData(EventCollaboratorRole.Collaborator)]
    public void Should_ReturnsFalse_When_EventCollaboratorIsNotOrganizer(EventCollaboratorRole eventCollaboratorRole)
    {
        EventCollaborator eventCollaborator = new()
        {
            EventCollaboratorRole = eventCollaboratorRole,
        };

        bool result = eventCollaborator.IsEventOrganizer();

        Assert.False(result);
    }
}
