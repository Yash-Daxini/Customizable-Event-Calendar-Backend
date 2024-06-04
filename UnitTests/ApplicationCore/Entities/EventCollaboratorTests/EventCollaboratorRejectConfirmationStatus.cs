using Core.Entities.Enums;
using Core.Entities;

namespace UnitTests.ApplicationCore.Entities.EventCollaboratorTests;

public class EventCollaboratorRejectConfirmationStatus
{
    [Fact]
    public void Should_ReturnsTrue_When_EventCollaboratorWithRejectStatus()
    {
        EventCollaborator eventCollaborator = new()
        {
            ConfirmationStatus = ConfirmationStatus.Reject
        };

        eventCollaborator.RejectConfirmationStatus();

        bool result = eventCollaborator.ConfirmationStatus == ConfirmationStatus.Reject;

        Assert.True(result);
    }


    [Theory]
    [InlineData(ConfirmationStatus.Accept)]
    [InlineData(ConfirmationStatus.Proposed)]
    [InlineData(ConfirmationStatus.Maybe)]
    [InlineData(ConfirmationStatus.Pending)]
    public void Should_ReturnsFalse_When_EventCollaboratorNotWithRejectStatus(ConfirmationStatus confirmationStatus)
    {
        EventCollaborator eventCollaborator = new()
        {
            ConfirmationStatus = confirmationStatus
        };

        eventCollaborator.RejectConfirmationStatus();

        bool result = eventCollaborator.ConfirmationStatus == ConfirmationStatus.Reject;

        Assert.True(result);
    }
}
