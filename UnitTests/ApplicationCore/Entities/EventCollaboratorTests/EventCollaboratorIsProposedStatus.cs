using Core.Entities.Enums;
using Core.Entities;

namespace UnitTests.ApplicationCore.Entities.EventCollaboratorTests;

public class EventCollaboratorIsProposedStatus
{
    [Fact]
    public void IsProposedStatusReturnTrueIfProposedStatus()
    {
        EventCollaborator eventCollaborator = new()
        {
            ConfirmationStatus = ConfirmationStatus.Proposed,
        };

        bool result = eventCollaborator.IsProposedStatus();

        Assert.True(result);
    }

    [Theory]
    [InlineData(ConfirmationStatus.Reject)]
    [InlineData(ConfirmationStatus.Accept)]
    [InlineData(ConfirmationStatus.Pending)]
    [InlineData(ConfirmationStatus.Maybe)]
    public void IsProposedStatusReturnFalseIfNotProposedStatus(ConfirmationStatus confirmationStatus)
    {
        EventCollaborator eventCollaborator = new()
        {
            ConfirmationStatus = confirmationStatus,
        };

        bool result = eventCollaborator.IsProposedStatus();

        Assert.False(result);
    }
}
