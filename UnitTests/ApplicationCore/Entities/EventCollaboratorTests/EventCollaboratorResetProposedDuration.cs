using Core.Entities;

namespace UnitTests.ApplicationCore.Entities.EventCollaboratorTests;

public class EventCollaboratorResetProposedDuration
{
    [Fact]
    public void Should_SetProposedDurationNull_When_ProposedDurationIsAlreadyNull()
    {
        EventCollaborator eventCollaborator = new()
        {
            ProposedDuration = null,
        };

        eventCollaborator.ResetProposedDuration();

        bool result = eventCollaborator.IsProposedDurationNull();

        Assert.True(result);
    }

    [Fact]
    public void Should_SetProposedDurationNull_When_ProposedDurationIsNotAlreadyNull()
    {
        EventCollaborator eventCollaborator = new()
        {
            ProposedDuration = new(5, 6),
        };

        eventCollaborator.ResetProposedDuration();

        bool result = eventCollaborator.IsProposedDurationNull();

        Assert.True(result);
    }
}
