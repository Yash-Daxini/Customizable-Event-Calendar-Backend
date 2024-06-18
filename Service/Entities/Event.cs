using Core.Entities.RecurrecePattern;
using Core.Interfaces;

namespace Core.Entities;

public class Event : IEntity
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Location { get; set; }

    public string Description { get; set; }

    public Duration Duration { get; set; }

    public RecurrencePattern RecurrencePattern { get; set; }

    public List<EventCollaboratorsByDate> DateWiseEventCollaborators { get; set; } = [];

    public User? GetEventOrganizer()
    {
        if (DateWiseEventCollaborators is null
            || DateWiseEventCollaborators.Count is 0)
            return null;

        List<EventCollaborator>? eventCollaborators = DateWiseEventCollaborators.First()
                                                     .EventCollaborators;

        EventCollaborator? eventCollaborator = eventCollaborators
                                              ?.FirstOrDefault(eventCollaborator => eventCollaborator.IsOrganizer());

        return eventCollaborator?.User;
    }

    public List<EventCollaborator> GetEventInvitees()
    {
        if (DateWiseEventCollaborators is null
            || DateWiseEventCollaborators.Count is 0)
            return [];

        if (DateWiseEventCollaborators[0].EventCollaborators is null)
            return [];

        return [.. DateWiseEventCollaborators[0].EventCollaborators
                                                .Where(eventCollaborator => eventCollaborator.IsParticipant())];
    }

    public bool HasPendingResponseFromUser(int userId)
    {
        return GetEventInvitees()
               .Exists(eventCollaborator => eventCollaborator.User.Id == userId
                                         && eventCollaborator.IsStatusPending());
    }

    public bool IsProposedEvent()
    {
        return GetEventInvitees()
               .Exists(eventCollaborator => eventCollaborator.IsStatusPending()
                                            || eventCollaborator.IsStatusProposed());
    }

    public bool IsEventOverlappingWith(Event eventObj, DateOnly? matchedDate)
    {
        if (matchedDate is null || eventObj is null) return false;

        return Duration.IsOverlappingWith(eventObj.Duration);
    }

    public DateOnly? GetOverlapDate(Event currentEvent)
    {
        if (currentEvent is null)
            return null;

        List<DateOnly> eventDates = DateWiseEventCollaborators
                                    .Select(eventCollaboratorByDate => eventCollaboratorByDate.EventDate)
                                    .ToList();

        List<DateOnly> currentEventDates = currentEvent.DateWiseEventCollaborators
                                                       .Select(eventCollaboratorByDate => eventCollaboratorByDate.EventDate)
                                                       .ToList();

        DateOnly matchedDate = eventDates.Intersect(currentEventDates).FirstOrDefault();

        return matchedDate == default
               ? null
               : matchedDate;
    }

    public void CreateDateWiseEventCollaboratorList(List<DateOnly> occurrences)
    {
        EventCollaboratorsByDate? eventCollaboratorsByDate = DateWiseEventCollaborators.FirstOrDefault();

        if (eventCollaboratorsByDate is null) return;

        List<EventCollaborator> eventCollaborators = eventCollaboratorsByDate.EventCollaborators;

        List<EventCollaboratorsByDate> eventCollaboratorsByDates = [];

        foreach (DateOnly occurrence in occurrences)
        {
            eventCollaboratorsByDates.Add(new EventCollaboratorsByDate()
            {
                EventDate = occurrence,
                EventCollaborators = [..eventCollaborators.Select(eventCollaborator => new EventCollaborator()
                    {
                        Id = eventCollaborator.Id,
                        EventCollaboratorRole = eventCollaborator.EventCollaboratorRole,
                        ConfirmationStatus = eventCollaborator.ConfirmationStatus,
                        ProposedDuration = eventCollaborator.ProposedDuration,
                        EventDate = occurrence,
                        EventId = eventCollaborator.EventId,
                        User = eventCollaborator.User
                    })]
            });
        }

        this.DateWiseEventCollaborators = eventCollaboratorsByDates;
    }

    public List<EventCollaborator> GetEventCollaboratorsForGivenDate(DateOnly eventDate)
    {
        if (DateWiseEventCollaborators is null)
            return [];

        EventCollaboratorsByDate? eventCollaboratorsByDate = DateWiseEventCollaborators
                                                             .FirstOrDefault(eventCollaboratorByDate => eventCollaboratorByDate.EventDate
                                                                                               == eventDate);
        return eventCollaboratorsByDate is null
               ? []
               : eventCollaboratorsByDate.EventCollaborators;
    }

    public bool IsUserCollaboratedOnGivenDate(int userId, DateOnly eventDate)
    {
        List<EventCollaborator> eventCollaborators = GetEventCollaboratorsForGivenDate(eventDate);

        return eventCollaborators
               .Exists(eventCollaborator => eventCollaborator.User.Id == userId);
    }
}
