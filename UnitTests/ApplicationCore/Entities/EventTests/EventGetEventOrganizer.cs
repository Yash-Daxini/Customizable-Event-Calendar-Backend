using Core.Entities;
using FluentAssertions;

namespace UnitTests.ApplicationCore.Entities.EventTests;

public class EventGetEventOrganizer
{
    [Fact]
    public void Should_ReturnOrganizerOfEvent_When_MultipleEventCollaboratorsPresent()
    {
        Event eventObj = new()
        {
            EventCollaborators = [
                        new EventCollaborator
                        {
                            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Organizer,
                            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Accept,
                            ProposedDuration = null,
                            EventDate = new DateOnly(),
                            User = new User
                            {
                                Id = 48,
                                Name = "a",
                                Email = "a@gmail.com",
                                Password = "a"
                            },
                            EventId = 47
                        },
                        new EventCollaborator
                        {
                            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Participant,
                            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Pending,
                            ProposedDuration = null,
                            EventDate = new DateOnly(),
                            User = new User
                            {
                                Id = 49,
                                Name = "b",
                                Email = "b@gmail.com",
                                Password = "b"
                            },
                            EventId = 47
                        }
            ]
        };

        User actualUser = new()
        {
            Id = 48,
            Name = "a",
            Email = "a@gmail.com",
            Password = "a"
        };

        User? expectedUser = eventObj.GetEventOrganizer();

        actualUser.Should().BeEquivalentTo(expectedUser);
    }

    [Fact]
    public void Should_ReturnOrganizerOfEvent_When_OnlyOrganizerPresent()
    {
        Event eventObj = new()
        {
            EventCollaborators = [
                        new EventCollaborator
                        {
                            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Organizer,
                            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Accept,
                            ProposedDuration = null,
                            EventDate = new DateOnly(),
                            User = new User
                            {
                                Id = 48,
                                Name = "a",
                                Email = "a@gmail.com",
                                Password = "a"
                            },
                            EventId = 47
                        }
            ]
        };

        User expectedUser = new()
        {
            Id = 48,
            Name = "a",
            Email = "a@gmail.com",
            Password = "a"
        };

        User? actualUser = eventObj.GetEventOrganizer();

        actualUser.Should().BeEquivalentTo(expectedUser);
    }

    [Fact]
    public void Should_ReturnOrganizerOfEventAsNull_When_EventCollaboratorsIsNull()
    {
        Event eventObj = new() { EventCollaborators = null };

        User? actualResult = eventObj.GetEventOrganizer();

        actualResult.Should().BeNull();
    }

    [Fact]
    public void Should_ReturnOrganizerOfEventAsNull_When_EventCollaboratorsIsEmpty()
    {
        Event eventObj = new() { EventCollaborators = [] };

        User? actualResult = eventObj.GetEventOrganizer();

        actualResult.Should().BeNull();
    }
}