using Core.Entities.Enums;
using Core.Entities;

namespace UnitTests.ApplicationCore.Entities.EventCollaboratorTests;

public class EventCollaboratorSetConfirmationStatus
{
    [Fact]
    public void Should_SetConfirmationStatusReject_When_ConfirmationStatusIsAlreadyReject()
    {
        EventCollaborator eventCollaborator = new()
        {
            ConfirmationStatus = ConfirmationStatus.Reject
        };

        eventCollaborator.SetConfirmationStatus(ConfirmationStatus.Reject);

        bool result = eventCollaborator.ConfirmationStatus == ConfirmationStatus.Reject;

        Assert.True(result);
    }


    [Theory]
    [InlineData(ConfirmationStatus.Accept)]
    [InlineData(ConfirmationStatus.Proposed)]
    [InlineData(ConfirmationStatus.Maybe)]
    [InlineData(ConfirmationStatus.Pending)]
    public void Should_SetConfirmationStatusReject_When_ConfirmationStatusIsNotAlreadyReject(ConfirmationStatus confirmationStatus)
    {
        EventCollaborator eventCollaborator = new()
        {
            ConfirmationStatus = confirmationStatus
        };

        eventCollaborator.SetConfirmationStatus(ConfirmationStatus.Reject);

        bool result = eventCollaborator.ConfirmationStatus == ConfirmationStatus.Reject;

        Assert.True(result);
    }

    [Fact]
    public void Should_SetConfirmationStatusPending_When_ConfirmationStatusIsAlreadyPending()
    {
        EventCollaborator eventCollaborator = new()
        {
            ConfirmationStatus = ConfirmationStatus.Pending,
        };

        eventCollaborator.SetConfirmationStatus(ConfirmationStatus.Pending);

        bool result = eventCollaborator.IsStatusPending();

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

        eventCollaborator.SetConfirmationStatus(ConfirmationStatus.Pending);

        bool result = eventCollaborator.IsStatusPending();

        Assert.True(result);
    }

    [Fact]
    public void Should_SetConfirmationStatusAccept_When_ConfirmationStatusIsAlreadyAccept()
    {
        EventCollaborator eventCollaborator = new()
        {
            ConfirmationStatus = ConfirmationStatus.Accept
        };

        eventCollaborator.SetConfirmationStatus(ConfirmationStatus.Accept);

        bool result = eventCollaborator.IsStatusAccept();

        Assert.True(result);
    }


    [Theory]
    [InlineData(ConfirmationStatus.Reject)]
    [InlineData(ConfirmationStatus.Proposed)]
    [InlineData(ConfirmationStatus.Maybe)]
    [InlineData(ConfirmationStatus.Pending)]
    public void Should_SetConfirmationStatusAccept_When_ConfirmationStatusIsNotAlreadyAccept(ConfirmationStatus confirmationStatus)
    {
        EventCollaborator eventCollaborator = new()
        {
            ConfirmationStatus = confirmationStatus
        };

        eventCollaborator.SetConfirmationStatus(ConfirmationStatus.Accept);

        bool result = eventCollaborator.IsStatusAccept();

        Assert.True(result);
    }


    [Fact]
    public void Should_SetConfirmationStatusProposed_When_ConfirmationStatusIsAlreadyProposed()
    {
        EventCollaborator eventCollaborator = new()
        {
            ConfirmationStatus = ConfirmationStatus.Proposed
        };

        eventCollaborator.SetConfirmationStatus(ConfirmationStatus.Proposed);

        bool result = eventCollaborator.ConfirmationStatus == ConfirmationStatus.Proposed;

        Assert.True(result);
    }


    [Theory]
    [InlineData(ConfirmationStatus.Reject)]
    [InlineData(ConfirmationStatus.Accept)]
    [InlineData(ConfirmationStatus.Maybe)]
    [InlineData(ConfirmationStatus.Pending)]
    public void Should_SetConfirmationStatusProposed_When_ConfirmationStatusIsNotAlreadyProposed(ConfirmationStatus confirmationStatus)
    {
        EventCollaborator eventCollaborator = new()
        {
            ConfirmationStatus = confirmationStatus
        };

        eventCollaborator.SetConfirmationStatus(ConfirmationStatus.Proposed);

        bool result = eventCollaborator.ConfirmationStatus == ConfirmationStatus.Proposed;

        Assert.True(result);
    }

    [Fact]
    public void Should_SetConfirmationStatusMaybe_When_ConfirmationStatusIsAlreadyMaybe()
    {
        EventCollaborator eventCollaborator = new()
        {
            ConfirmationStatus = ConfirmationStatus.Maybe
        };

        eventCollaborator.SetConfirmationStatus(ConfirmationStatus.Maybe);

        bool result = eventCollaborator.ConfirmationStatus == ConfirmationStatus.Maybe;

        Assert.True(result);
    }


    [Theory]
    [InlineData(ConfirmationStatus.Reject)]
    [InlineData(ConfirmationStatus.Accept)]
    [InlineData(ConfirmationStatus.Maybe)]
    [InlineData(ConfirmationStatus.Pending)]
    public void Should_SetConfirmationStatusMaybe_When_ConfirmationStatusIsNotAlreadyMaybe(ConfirmationStatus confirmationStatus)
    {
        EventCollaborator eventCollaborator = new()
        {
            ConfirmationStatus = confirmationStatus
        };

        eventCollaborator.SetConfirmationStatus(ConfirmationStatus.Maybe);

        bool result = eventCollaborator.ConfirmationStatus == ConfirmationStatus.Maybe;

        Assert.True(result);
    }
}
