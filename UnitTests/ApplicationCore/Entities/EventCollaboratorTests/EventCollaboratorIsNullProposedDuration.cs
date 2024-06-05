using Core.Entities.Enums;
using Core.Entities;

namespace UnitTests.ApplicationCore.Entities.EventCollaboratorTests;

public class EventCollaboratorIsNullProposedDuration
{
    [Fact]
    public void Should_ReturnsTrue_When_ProposedDurationIsNull()
    {
        EventCollaborator eventCollaborator = new()
        {
            ProposedDuration = null
        };

        bool result = eventCollaborator.IsNullProposedDuration();

        Assert.True(result);
    }

    [Fact]
    public void Should_ReturnsFalse_When_ProposedDurationIsNotNull()
    {
        EventCollaborator eventCollaborator = new()
        {
            ProposedDuration = new Duration(5,6)
        };

        bool result = eventCollaborator.IsNullProposedDuration();

        Assert.False(result);
    }
}
