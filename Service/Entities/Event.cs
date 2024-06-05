using Core.Entities.Enums;
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

        if (eventCollaborators is null)
            return null;

        EventCollaborator? eventCollaborator = eventCollaborators
                                              .FirstOrDefault(eventCollaborator => eventCollaborator.IsEventOrganizer());

        if (eventCollaborator is null)
            return null;

        return eventCollaborator.User;
    }

    public List<EventCollaborator> GetEventInvitees()
    {
        if (DateWiseEventCollaborators is null
            || DateWiseEventCollaborators.Count is 0)
            return [];

        if (DateWiseEventCollaborators[0].EventCollaborators is null)
            return [];

        return [.. DateWiseEventCollaborators[0].EventCollaborators
                                                .Where(eventCollaborator => eventCollaborator.IsEventParticipant())];
    }

    public bool HasPendingResponseFromUser(int userId)
    {
        return GetEventInvitees()
               .Exists(eventCollaborator => eventCollaborator.User.Id == userId
                                         && eventCollaborator.IsPendingStatus());
    }

    public bool IsProposedEvent()
    {
        return GetEventInvitees()
               .Exists(eventCollaborator => eventCollaborator.IsPendingStatus()
                                            || eventCollaborator.IsProposedStatus());
    }

    public bool IsEventOverlappingWith(Event eventObj, DateOnly? matchedDate)
    {
        if (matchedDate is null || eventObj is null) return false;

        if (this.Duration.IsOverlappingWith(eventObj.Duration))
            return true;

        return false;
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

    public void CreateDateWiseEventCollaboratorsList(List<DateOnly> occurrences)
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
                EventCollaborators = [..eventCollaborators.Select(eventCollaborator =>
                {
                    EventCollaborator newEventCollaborator = new(eventCollaborator) { EventDate = occurrence };
                    return newEventCollaborator;
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

    public void MakeNonRecurringEvent()
    {
        RecurrencePattern.Frequency = Frequency.None;
        RecurrencePattern.Interval = 1;
        RecurrencePattern.ByWeekDay = null;
        RecurrencePattern.ByMonthDay = null;
        RecurrencePattern.ByMonth = null;
        RecurrencePattern.WeekOrder = null;
    }
}
