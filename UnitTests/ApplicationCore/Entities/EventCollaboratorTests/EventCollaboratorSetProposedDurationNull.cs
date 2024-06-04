using Core.Entities;

namespace UnitTests.ApplicationCore.Entities.EventCollaboratorTests;

public class EventCollaboratorSetProposedDurationNull
{
    [Fact]
    public void Should_SetProposedDurationNull_When_ProposedDurationIsAlreadyNull()
    {
        EventCollaborator eventCollaborator = new()
        {
            ProposedDuration = null,
        };

        eventCollaborator.SetProposedDurationNull();

        bool result = eventCollaborator.IsNullProposedDuration();

        Assert.True(result);
    }

    [Fact]
    public void Should_SetProposedDurationNull_When_ProposedDurationIsNotAlreadyNull()
    {
        EventCollaborator eventCollaborator = new()
        {
            ProposedDuration = new(),
        };

        eventCollaborator.SetProposedDurationNull();

        bool result = eventCollaborator.IsNullProposedDuration();

        Assert.True(result);
    }
}
