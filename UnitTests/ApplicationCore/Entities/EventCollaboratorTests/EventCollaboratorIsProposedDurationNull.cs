using Core.Entities;

namespace UnitTests.ApplicationCore.Entities.EventCollaboratorTests;

public class EventCollaboratorIsProposedDurationNull
{
    [Fact]
    public void Should_ReturnsTrue_When_ProposedDurationIsNull()
    {
        EventCollaborator eventCollaborator = new()
        {
            ProposedDuration = null
        };

        bool result = eventCollaborator.IsProposedDurationNull();

        Assert.True(result);
    }

    [Fact]
    public void Should_ReturnsFalse_When_ProposedDurationIsNotNull()
    {
        EventCollaborator eventCollaborator = new()
        {
            ProposedDuration = new Duration(5,6)
        };

        bool result = eventCollaborator.IsProposedDurationNull();

        Assert.False(result);
    }
}
