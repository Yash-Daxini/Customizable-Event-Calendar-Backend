﻿using Core.Entities;
using FluentAssertions;
using UnitTests.Builders;

namespace UnitTests.ApplicationCore.Entities.EventCollaboratorTests;

public class EventCollaboratorIsProposedDurationNull
{
    [Fact]
    public void Should_ReturnsTrue_When_ProposedDurationIsNull()
    {
        EventCollaborator eventCollaborator = new EventCollaboratorBuilder()
                                              .WithProposedDuration(null)
                                              .Build();

        bool result = eventCollaborator.IsProposedDurationNull();

        result.Should().BeTrue();
    }

    [Fact]
    public void Should_ReturnsFalse_When_ProposedDurationIsNotNull()
    {
        EventCollaborator eventCollaborator = new EventCollaboratorBuilder()
                                              .WithProposedDuration(new Duration(5, 6))
                                              .Build();

        bool result = eventCollaborator.IsProposedDurationNull();

        result.Should().BeFalse();
    }
}
