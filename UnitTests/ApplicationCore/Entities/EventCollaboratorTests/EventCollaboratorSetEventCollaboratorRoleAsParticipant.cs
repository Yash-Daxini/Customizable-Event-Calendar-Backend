﻿using Core.Entities.Enums;
using Core.Entities;
using UnitTests.Builders;

namespace UnitTests.ApplicationCore.Entities.EventCollaboratorTests;

public class EventCollaboratorSetEventCollaboratorRoleAsParticipant
{
    [Fact]
    public void Should_SetEventCollaboratorRoleAsParticipant_When_EventCollaboratorRoleIsAlreadyParticipant()
    {
        EventCollaborator eventCollaborator = new EventCollaboratorBuilder()
                                              .WithEventCollaboratorRole(EventCollaboratorRole.Participant)
                                              .Build();

        eventCollaborator.SetEventCollaboratorRoleAsParticipant();

        bool result = eventCollaborator.IsParticipant();

        Assert.True(result);
    }

    [Theory]
    [InlineData(EventCollaboratorRole.Organizer)]
    [InlineData(EventCollaboratorRole.Collaborator)]
    public void Should_SetEventCollaboratorRoleAsParticipant_When_EventCollaboratorRoleIsNotAlreadyParticipant(EventCollaboratorRole eventCollaboratorRole)
    {
        EventCollaborator eventCollaborator = new EventCollaboratorBuilder()
                                              .WithEventCollaboratorRole(eventCollaboratorRole)
                                              .Build();

        eventCollaborator.SetEventCollaboratorRoleAsParticipant();

        bool result = eventCollaborator.IsParticipant();

        Assert.True(result);
    }
}
