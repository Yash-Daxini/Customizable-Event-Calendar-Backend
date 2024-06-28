using Core.Entities.Enums;
using Core.Entities;
using UnitTests.Builders.EntityBuilder;
using FluentAssertions;

namespace UnitTests.ApplicationCore.Entities.EventCollaboratorTests;

public class EventCollaboratorIsParticipant
{
    [Fact]
    public void Should_Returns_True_When_EventCollaboratorIsParticipant()
    {
        EventCollaborator eventCollaborator = new EventCollaboratorBuilder()
                                              .WithEventCollaboratorRole(EventCollaboratorRole.Participant)
                                              .Build();

        bool result = eventCollaborator.IsParticipant();

        result.Should().BeTrue();
    }

    [Theory]
    [InlineData(EventCollaboratorRole.Organizer)]
    [InlineData(EventCollaboratorRole.Collaborator)]
    public void Should_Returns_False_When_EventCollaboratorIsNotParticipant(EventCollaboratorRole eventCollaboratorRole)
    {
        EventCollaborator eventCollaborator = new EventCollaboratorBuilder()
                                              .WithEventCollaboratorRole(eventCollaboratorRole)
                                              .Build();

        bool result = eventCollaborator.IsParticipant();

        result.Should().BeFalse();
    }
}
