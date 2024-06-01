using Core.Entities.Enums;
using Core.Entities;

namespace UnitTests.ApplicationCore.Entities.EventCollaboratorTests;

public class EventCollaboratorIsNullProposedDuration
{
    [Fact]
    public void IsNullProposedDurationRetursTrueIfNull()
    {
        EventCollaborator eventCollaborator = new()
        {
            ProposedDuration = null
        };

        bool result = eventCollaborator.IsNullProposedDuration();

        Assert.True(result);
    }

    [Fact]
    public void IsNullProposedDurationRetursFalseIfNotNull()
    {
        EventCollaborator eventCollaborator = new()
        {
            ProposedDuration = new Duration()
        };

        bool result = eventCollaborator.IsNullProposedDuration();

        Assert.False(result);
    }
}
