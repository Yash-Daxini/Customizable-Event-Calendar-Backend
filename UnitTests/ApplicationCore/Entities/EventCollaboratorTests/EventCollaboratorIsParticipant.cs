using Core.Entities.Enums;
using Core.Entities;
using UnitTests.Builders;

namespace UnitTests.ApplicationCore.Entities.EventCollaboratorTests;

public class EventCollaboratorIsParticipant
{
    [Fact]
    public void Should_ReturnsTrue_When_EventCollaboratorIsParticipant()
    {
        EventCollaborator eventCollaborator = new EventCollaboratorBuilder()
                                              .WithEventCollaboratorRole(EventCollaboratorRole.Participant)
                                              .Build();

        bool result = eventCollaborator.IsParticipant();

        Assert.True(result);
    }

    [Theory]
    [InlineData(EventCollaboratorRole.Organizer)]
    [InlineData(EventCollaboratorRole.Collaborator)]
    public void Should_ReturnsFalse_When_EventCollaboratorIsNotParticipant(EventCollaboratorRole eventCollaboratorRole)
    {
        EventCollaborator eventCollaborator = new EventCollaboratorBuilder()
                                              .WithEventCollaboratorRole(eventCollaboratorRole)
                                              .Build();

        bool result = eventCollaborator.IsParticipant();

        Assert.False(result);
    }
}
