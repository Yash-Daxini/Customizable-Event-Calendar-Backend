﻿using Core.Entities.Enums;
using Core.Entities;
using UnitTests.Builders.EntityBuilder;
using FluentAssertions;

namespace UnitTests.ApplicationCore.Entities.EventCollaboratorTests;

public class EventCollaboratorIsStatusAccept
{
    [Fact]
    public void Should_Return_True_When_StatusIsAccept()
    {
        EventCollaborator eventCollaborator = new EventCollaboratorBuilder()
                                              .WithConfirmationStatus(ConfirmationStatus.Accept)
                                              .Build();

        bool result = eventCollaborator.IsStatusAccept();

        result.Should().BeTrue();
    }

    [Theory]
    [InlineData(ConfirmationStatus.Reject)]
    [InlineData(ConfirmationStatus.Proposed)]
    [InlineData(ConfirmationStatus.Pending)]
    [InlineData(ConfirmationStatus.Maybe)]
    public void Should_Return_False_When_StatusIsNotAccept(ConfirmationStatus confirmationStatus)
    {
        EventCollaborator eventCollaborator = new EventCollaboratorBuilder()
                                              .WithConfirmationStatus(confirmationStatus)
                                              .Build();

        bool result = eventCollaborator.IsStatusAccept();

        result.Should().BeFalse();
    }
}
