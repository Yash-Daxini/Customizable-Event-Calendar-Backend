using Core.Entities;
using FluentAssertions;

namespace UnitTests.ApplicationCore.Entities.EventTests;

public class EventGetEventCollaboratorsForGivenDate
{

    private readonly Event _event;

    public EventGetEventCollaboratorsForGivenDate()
    {
        _event = new()
        {
            EventCollaborators = [
                        new EventCollaborator
                        {
                            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Organizer,
                            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Accept,
                            ProposedDuration = null,
                            EventDate = new DateOnly(2024, 5, 31),
                            User = new User
                            {
                                Id = 48,
                            },
                            EventId = 47
                        },
                        new EventCollaborator
                        {
                            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Participant,
                            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Accept,
                            ProposedDuration = null,
                            EventDate = new DateOnly(2024, 5, 31),
                            User = new User
                            {
                                Id = 49,
                            },
                            EventId = 47
                        },
                        new EventCollaborator
                        {
                            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Organizer,
                            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Accept,
                            ProposedDuration = null,
                            EventDate = new DateOnly(2024, 6, 2),
                            User = new User
                            {
                                Id = 48,
                            },
                            EventId = 47
                        }
                    ]
        };
    }

    [Theory]
    [InlineData(2024, 5, 30)]
    [InlineData(2024, 5, 29)]
    [InlineData(2024, 6, 3)]
    [InlineData(2024, 6, 1)]

    public void Should_ReturnsEmptyList_When_EventCollaboratorNotOccurOnGivenDate(int year, int month, int day)
    {
        DateOnly date = new(year, month, day);

        List<EventCollaborator> eventCollaborators = _event.GetEventCollaboratorsForGivenDate(date);

        eventCollaborators.Should().BeEmpty();
    }

    [Theory]
    [InlineData(2024, 5, 31)]
    public void Should_ReturnsEventCollaboratorList_When_EventCollaboratorOccurOnGivenDate(int year, int month, int day)
    {
        List<EventCollaborator> expectedResult = [
                        new EventCollaborator
                        {
                            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Organizer,
                            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Accept,
                            ProposedDuration = null,
                            EventDate = new DateOnly(2024, 5, 31),
                            User = new User
                            {
                                Id = 48,
                            },
                            EventId = 47
                        },
                        new EventCollaborator
                        {
                            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Participant,
                            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Accept,
                            ProposedDuration = null,
                            EventDate = new DateOnly(2024, 5, 31),
                            User = new User
                            {
                                Id = 49
                            },
                            EventId = 47
                        }
                    ];

        DateOnly date = new(year, month, day);
        List<EventCollaborator> eventCollaborators = _event.GetEventCollaboratorsForGivenDate(date);

        eventCollaborators.Should().BeEquivalentTo(expectedResult);
    }

    [Theory]
    [InlineData(2024, 5, 31)]
    [InlineData(2024, 6, 2)]
    public void Should_ReturnsEmptyList_When_EventCollaboratorsIsNull(int year, int month, int day)
    {
        _event.EventCollaborators = null;

        DateOnly date = new(year, month, day);
        List<EventCollaborator> eventCollaborators = _event.GetEventCollaboratorsForGivenDate(date);

        eventCollaborators.Should().BeEmpty();
    }

    [Theory]
    [InlineData(2024, 5, 31)]
    [InlineData(2024, 6, 2)]
    public void Should_ReturnsEmptyList_When_EventCollaboratorsIsEmpty(int year, int month, int day)
    {
        _event.EventCollaborators = [];

        DateOnly date = new(year, month, day);
        List<EventCollaborator> eventCollaborators = _event.GetEventCollaboratorsForGivenDate(date);

        eventCollaborators.Should().BeEmpty();
    }
}
