using Core.Entities;
using FluentAssertions;

namespace UnitTests.ApplicationCore.Entities.EventTests;

public class EventCreateDateWiseEventCollaboratorsList
{
    [Fact]
    public void Should_PrepareEventCollaboratorsListFromOccurrences_When_EventCollaboratorsAvailable()
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
                            Id = 48
                        },
                        EventId = 47
                    },
                    new EventCollaborator
                    {
                        EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Participant,
                        ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Pending,
                        ProposedDuration = null,
                        EventDate = new DateOnly(2024, 5, 31),
                        User = new User
                        {
                            Id = 49
                        },
                        EventId = 47
                    },
                    new EventCollaborator
                    {
                        EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Organizer,
                        ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Accept,
                        ProposedDuration = null,
                        EventDate = new DateOnly(2024, 6, 1),
                        User = new User
                        {
                            Id = 48
                        },
                        EventId = 47
                    },
                    new EventCollaborator
                    {
                        EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Participant,
                        ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Pending,
                        ProposedDuration = null,
                        EventDate = new DateOnly(2024, 6, 1),
                        User = new User
                        {
                            Id = 49
                        },
                        EventId = 47
                    }
        ];

        List<DateOnly> occurrences = [new DateOnly(2024, 5, 31),
                                      new DateOnly(2024, 6, 1)];

        Event eventObj = new()
        {
            EventCollaborators = [
                        new EventCollaborator
                        {
                            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Organizer,
                            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Accept,
                            ProposedDuration = null,
                            User = new User
                            {
                                Id = 48,
                            },
                            EventId = 47
                        },
                        new EventCollaborator
                        {
                            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Participant,
                            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Pending,
                            ProposedDuration = null,
                            User = new User
                            {
                                Id = 49,
                            },
                            EventId = 47
                        },
                    ]
        };

        eventObj.PrepareCollaboratorsFromOccurrences(occurrences);

        eventObj.EventCollaborators.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public void Should_BeEmptyList_When_EventCollaboratorsNotAvailable()
    {
        List<DateOnly> occurrences = [new DateOnly(2024, 5, 31),
                                      new DateOnly(2024, 6, 1)];

        Event eventObj = new()
        {
            EventCollaborators = []
        };

        eventObj.PrepareCollaboratorsFromOccurrences(occurrences);

        eventObj.EventCollaborators.Should().BeEmpty();
    }

    [Fact]
    public void Should_BeEmptyList_When_OccurrencesListIsEmpty()
    {
        List<DateOnly> occurrences = [];

        Event eventObj = new()
        {
            EventCollaborators = [
                        new EventCollaborator
                        {
                            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Organizer,
                            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Accept,
                            ProposedDuration = null,
                            User = new User
                            {
                                Id = 48,
                            },
                            EventId = 47
                        },
                        new EventCollaborator
                        {
                            EventCollaboratorRole = Core.Entities.Enums.EventCollaboratorRole.Participant,
                            ConfirmationStatus = Core.Entities.Enums.ConfirmationStatus.Pending,
                            ProposedDuration = null,
                            User = new User
                            {
                                Id = 49,
                            },
                            EventId = 47
                        },
                    ]
        };

        eventObj.PrepareCollaboratorsFromOccurrences(occurrences);

        eventObj.EventCollaborators.Should().BeEmpty();
    }

    [Fact]
    public void Should_BeEmptyList_When_EventCollaboratorsIsNull()
    {
        List<DateOnly> occurrences = [];

        Event eventObj = new()
        {
            EventCollaborators = null
        };

        eventObj.PrepareCollaboratorsFromOccurrences(occurrences);

        eventObj.EventCollaborators.Should().BeEmpty();
    }
}
