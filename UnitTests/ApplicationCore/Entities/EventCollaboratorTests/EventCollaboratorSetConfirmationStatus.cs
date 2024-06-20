using Core.Entities.Enums;
using Core.Entities;
using UnitTests.Builders;
using FluentAssertions;

namespace UnitTests.ApplicationCore.Entities.EventCollaboratorTests;

public class EventCollaboratorSetConfirmationStatus
{
    [Fact]
    public void Should_SetConfirmationStatusReject_When_ConfirmationStatusIsAlreadyReject()
    {
        EventCollaborator eventCollaborator = new EventCollaboratorBuilder()
                                              .WithConfirmationStatus(ConfirmationStatus.Reject)
                                              .Build();

        eventCollaborator.SetConfirmationStatus(ConfirmationStatus.Reject);

        bool result = eventCollaborator.ConfirmationStatus == ConfirmationStatus.Reject;

        result.Should().BeTrue();
    }


    [Theory]
    [InlineData(ConfirmationStatus.Accept)]
    [InlineData(ConfirmationStatus.Proposed)]
    [InlineData(ConfirmationStatus.Maybe)]
    [InlineData(ConfirmationStatus.Pending)]
    public void Should_SetConfirmationStatusReject_When_ConfirmationStatusIsNotAlreadyReject(ConfirmationStatus confirmationStatus)
    {
        EventCollaborator eventCollaborator = new EventCollaboratorBuilder()
                                              .WithConfirmationStatus(confirmationStatus)
                                              .Build();

        eventCollaborator.SetConfirmationStatus(ConfirmationStatus.Reject);

        bool result = eventCollaborator.ConfirmationStatus == ConfirmationStatus.Reject;

        result.Should().BeTrue();
    }

    [Fact]
    public void Should_SetConfirmationStatusPending_When_ConfirmationStatusIsAlreadyPending()
    {
        EventCollaborator eventCollaborator = new EventCollaboratorBuilder()
                                              .WithConfirmationStatus(ConfirmationStatus.Pending)
                                              .Build();

        eventCollaborator.SetConfirmationStatus(ConfirmationStatus.Pending);

        bool result = eventCollaborator.IsStatusPending();

        result.Should().BeTrue();
    }


    [Theory]
    [InlineData(ConfirmationStatus.Accept)]
    [InlineData(ConfirmationStatus.Proposed)]
    [InlineData(ConfirmationStatus.Maybe)]
    [InlineData(ConfirmationStatus.Reject)]
    public void Should_SetConfirmationStatusPending_When_ConfirmationStatusIsNotAlreadyPending(ConfirmationStatus confirmationStatus)
    {
        EventCollaborator eventCollaborator = new EventCollaboratorBuilder()
                                              .WithConfirmationStatus(confirmationStatus)
                                              .Build();

        eventCollaborator.SetConfirmationStatus(ConfirmationStatus.Pending);

        bool result = eventCollaborator.IsStatusPending();

        result.Should().BeTrue();
    }

    [Fact]
    public void Should_SetConfirmationStatusAccept_When_ConfirmationStatusIsAlreadyAccept()
    {
        EventCollaborator eventCollaborator = new EventCollaboratorBuilder()
                                              .WithConfirmationStatus(ConfirmationStatus.Accept)
                                              .Build();

        eventCollaborator.SetConfirmationStatus(ConfirmationStatus.Accept);

        bool result = eventCollaborator.IsStatusAccept();

        result.Should().BeTrue();
    }


    [Theory]
    [InlineData(ConfirmationStatus.Reject)]
    [InlineData(ConfirmationStatus.Proposed)]
    [InlineData(ConfirmationStatus.Maybe)]
    [InlineData(ConfirmationStatus.Pending)]
    public void Should_SetConfirmationStatusAccept_When_ConfirmationStatusIsNotAlreadyAccept(ConfirmationStatus confirmationStatus)
    {
        EventCollaborator eventCollaborator = new EventCollaboratorBuilder()
                                              .WithConfirmationStatus(confirmationStatus)
                                              .Build();

        eventCollaborator.SetConfirmationStatus(ConfirmationStatus.Accept);

        bool result = eventCollaborator.IsStatusAccept();

        result.Should().BeTrue();
    }


    [Fact]
    public void Should_SetConfirmationStatusProposed_When_ConfirmationStatusIsAlreadyProposed()
    {
        EventCollaborator eventCollaborator = new EventCollaboratorBuilder()
                                              .WithConfirmationStatus(ConfirmationStatus.Proposed)
                                              .Build();

        eventCollaborator.SetConfirmationStatus(ConfirmationStatus.Proposed);

        bool result = eventCollaborator.ConfirmationStatus == ConfirmationStatus.Proposed;

        result.Should().BeTrue();
    }


    [Theory]
    [InlineData(ConfirmationStatus.Reject)]
    [InlineData(ConfirmationStatus.Accept)]
    [InlineData(ConfirmationStatus.Maybe)]
    [InlineData(ConfirmationStatus.Pending)]
    public void Should_SetConfirmationStatusProposed_When_ConfirmationStatusIsNotAlreadyProposed(ConfirmationStatus confirmationStatus)
    {
        EventCollaborator eventCollaborator = new EventCollaboratorBuilder()
                                              .WithConfirmationStatus(confirmationStatus)
                                              .Build();

        eventCollaborator.SetConfirmationStatus(ConfirmationStatus.Proposed);

        bool result = eventCollaborator.ConfirmationStatus == ConfirmationStatus.Proposed;

        result.Should().BeTrue();
    }

    [Fact]
    public void Should_SetConfirmationStatusMaybe_When_ConfirmationStatusIsAlreadyMaybe()
    {
        EventCollaborator eventCollaborator = new EventCollaboratorBuilder()
                                              .WithConfirmationStatus(ConfirmationStatus.Maybe)
                                              .Build();

        eventCollaborator.SetConfirmationStatus(ConfirmationStatus.Maybe);

        bool result = eventCollaborator.ConfirmationStatus == ConfirmationStatus.Maybe;

        result.Should().BeTrue();
    }


    [Theory]
    [InlineData(ConfirmationStatus.Reject)]
    [InlineData(ConfirmationStatus.Accept)]
    [InlineData(ConfirmationStatus.Maybe)]
    [InlineData(ConfirmationStatus.Pending)]
    public void Should_SetConfirmationStatusMaybe_When_ConfirmationStatusIsNotAlreadyMaybe(ConfirmationStatus confirmationStatus)
    {
        EventCollaborator eventCollaborator = new EventCollaboratorBuilder()
                                              .WithConfirmationStatus(confirmationStatus)
                                              .Build();

        eventCollaborator.SetConfirmationStatus(ConfirmationStatus.Maybe);

        bool result = eventCollaborator.ConfirmationStatus == ConfirmationStatus.Maybe;

        result.Should().BeTrue();
    }
}
