using Core.Entities.Enums;
using Core.Entities;
using UnitTests.Builders;
using FluentAssertions;

namespace UnitTests.ApplicationCore.Entities.EventCollaboratorTests;

public class EventCollaboratorIsOrganizer
{
    [Fact]
    public void Should_Returns_True_When_EventCollaboratorIsOrganizer()
    {
        EventCollaborator eventCollaborator = new EventCollaboratorBuilder()
                                              .WithEventCollaboratorRole(EventCollaboratorRole.Organizer)
                                              .Build();

        bool result = eventCollaborator.IsOrganizer();

        result.Should().BeTrue();
    }

    [Theory]
    [InlineData(EventCollaboratorRole.Participant)]
    [InlineData(EventCollaboratorRole.Collaborator)]
    public void Should_Returns_False_When_EventCollaboratorIsNotOrganizer(EventCollaboratorRole eventCollaboratorRole)
    {
        EventCollaborator eventCollaborator = new EventCollaboratorBuilder()
                                              .WithEventCollaboratorRole(eventCollaboratorRole)
                                              .Build();

        bool result = eventCollaborator.IsOrganizer();

        result.Should().BeFalse();
    }
}
