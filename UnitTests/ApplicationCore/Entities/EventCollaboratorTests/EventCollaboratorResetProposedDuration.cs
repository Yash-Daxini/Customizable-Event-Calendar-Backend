using Core.Entities;
using FluentAssertions;
using UnitTests.Builders;

namespace UnitTests.ApplicationCore.Entities.EventCollaboratorTests;

public class EventCollaboratorResetProposedDuration
{
    [Fact]
    public void Should_SetProposedDurationNull_When_ProposedDurationIsAlreadyNull()
    {
        EventCollaborator eventCollaborator = new EventCollaboratorBuilder()
                                              .WithProposedDuration(null)
                                              .Build();

        eventCollaborator.ResetProposedDuration();

        bool result = eventCollaborator.IsProposedDurationNull();

        result.Should().BeTrue();
    }

    [Fact]
    public void Should_SetProposedDurationNull_When_ProposedDurationIsNotAlreadyNull()
    {
        EventCollaborator eventCollaborator = new EventCollaboratorBuilder()
                                              .WithProposedDuration(new Duration(5, 6))
                                              .Build();

        eventCollaborator.ResetProposedDuration();

        bool result = eventCollaborator.IsProposedDurationNull();

        result.Should().BeTrue();
    }
}
