using Core.Entities.Enums;
using Core.Entities;

namespace UnitTests.ApplicationCore.Entities.EventCollaboratorTests;

public class EventCollaboratorSetConfirmationStatusPending
{
    [Fact]
    public void Should_SetConfirmationStatusPending_When_ConfirmationStatusIsAlreadyPending()
    {
        EventCollaborator eventCollaborator = new()
        {
            ConfirmationStatus = ConfirmationStatus.Reject
        };

        eventCollaborator.SetConfirmationStatusPending();

        bool result = eventCollaborator.IsPendingStatus();

        Assert.True(result);
    }


    [Theory]
    [InlineData(ConfirmationStatus.Accept)]
    [InlineData(ConfirmationStatus.Proposed)]
    [InlineData(ConfirmationStatus.Maybe)]
    [InlineData(ConfirmationStatus.Reject)]
    public void Should_SetConfirmationStatusPending_When_ConfirmationStatusIsNotAlreadyPending(ConfirmationStatus confirmationStatus)
    {
        EventCollaborator eventCollaborator = new()
        {
            ConfirmationStatus = confirmationStatus
        };

        eventCollaborator.SetConfirmationStatusPending();

        bool result = eventCollaborator.IsPendingStatus();

        Assert.True(result);
    }
}
