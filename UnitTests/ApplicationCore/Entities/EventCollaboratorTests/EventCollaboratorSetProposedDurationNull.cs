using Core.Entities;

namespace UnitTests.ApplicationCore.Entities.EventCollaboratorTests;

public class EventCollaboratorSetProposedDurationNull
{
    [Fact]
    public void SetProposedDurationNullIfAlreadyNull()
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
    public void SetProposedDurationNullIfNotNull()
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
