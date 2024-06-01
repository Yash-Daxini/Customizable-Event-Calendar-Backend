using Core.Entities;
using Core.Entities.Enums;

namespace UnitTests.ApplicationCore.Entities.EventCollaboratorTests;

public class EventCollaboratorIsPendingStatus
{
    [Fact]
    public void IsPendingStatusReturnTrueIfPendingStatus()
    {
        EventCollaborator eventCollaborator = new()
        {
            ConfirmationStatus = ConfirmationStatus.Pending,
        };

        bool result = eventCollaborator.IsPendingStatus();

        Assert.True(result);
    }

    [Theory]
    [InlineData(ConfirmationStatus.Reject)]
    [InlineData(ConfirmationStatus.Accept)]
    [InlineData(ConfirmationStatus.Proposed)]
    [InlineData(ConfirmationStatus.Maybe)]
    public void IsPendingStatusReturnFalseIfNotPendingStatus(ConfirmationStatus confirmationStatus)
    {
        EventCollaborator eventCollaborator = new()
        {
            ConfirmationStatus = confirmationStatus,
        };

        bool result = eventCollaborator.IsPendingStatus();

        Assert.False(result);
    }
}
