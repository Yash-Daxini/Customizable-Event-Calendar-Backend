using Core.Entities.Enums;
using Core.Entities;
using UnitTests.Builders;

namespace UnitTests.ApplicationCore.Entities.EventCollaboratorTests;

public class EventCollaboratorIsOrganizer
{
    [Fact]
    public void Should_ReturnsTrue_When_EventCollaboratorIsOrganizer()
    {
        EventCollaborator eventCollaborator = new EventCollaboratorBuilder()
                                              .WithEventCollaboratorRole(EventCollaboratorRole.Organizer)
                                              .Build();

        bool result = eventCollaborator.IsOrganizer();

        Assert.True(result);
    }

    [Theory]
    [InlineData(EventCollaboratorRole.Participant)]
    [InlineData(EventCollaboratorRole.Collaborator)]
    public void Should_ReturnsFalse_When_EventCollaboratorIsNotOrganizer(EventCollaboratorRole eventCollaboratorRole)
    {
        EventCollaborator eventCollaborator = new EventCollaboratorBuilder()
                                              .WithEventCollaboratorRole(eventCollaboratorRole)
                                              .Build();

        bool result = eventCollaborator.IsOrganizer();

        Assert.False(result);
    }
}
