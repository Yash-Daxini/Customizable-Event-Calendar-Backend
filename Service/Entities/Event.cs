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

        return DateWiseEventCollaborators.First()
                                         .EventCollaborators
                                         .FirstOrDefault(eventCollaborator => eventCollaborator.IsEventOrganizer())
                                         .User;
    }

    public List<EventCollaborator> GetEventInvitees()
    {
        if (DateWiseEventCollaborators is null
            || DateWiseEventCollaborators.Count is 0)
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

    public bool IsEventOverlappingWith(Event eventObj, DateOnly matchedDate)
    {
        if (matchedDate == default) return false;

        if (this.Duration.IsOverlappingWith(eventObj.Duration))
            return true;

        return false;
    }

    public DateOnly GetOverlapDate(Event currentEvent)
    {
        List<DateOnly> eventDates = DateWiseEventCollaborators
                                    .Select(eventCollaboratorByDate => eventCollaboratorByDate.EventDate)
                                    .ToList();

        List<DateOnly> currentEventDates = currentEvent.DateWiseEventCollaborators
                                                       .Select(eventCollaboratorByDate => eventCollaboratorByDate.EventDate)
                                                       .ToList();

        DateOnly matchedDate = eventDates.Intersect(currentEventDates).FirstOrDefault();

        return matchedDate;
    }

    public void CreateDateWiseEventCollaboratorsList(List<DateOnly> occurrences)
    {
        List<EventCollaborator> eventCollaborators = DateWiseEventCollaborators.First().EventCollaborators;

        List<EventCollaboratorsByDate> eventCollaboratorsByDates = [];

        foreach (DateOnly occurrence in occurrences)
        {
            List<EventCollaborator> updatedCollaborators = [];

            foreach (EventCollaborator eventCollaborator in eventCollaborators)
            {
                EventCollaborator newEventCollaborator = new(eventCollaborator)
                {
                    EventDate = occurrence
                };
                updatedCollaborators.Add(newEventCollaborator);
            }

            eventCollaboratorsByDates.Add(new EventCollaboratorsByDate()
            {
                EventDate = occurrence,
                EventCollaborators = updatedCollaborators
            });
        }

        this.DateWiseEventCollaborators = eventCollaboratorsByDates;
    }

    public List<EventCollaborator> GetEventCollaboratorsForGivenDate(DateOnly eventDate)
    {
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

    public override bool Equals(object? obj)
    {
        if (obj is Event eventObj)
        {
            bool result = this.Id == eventObj.Id
                && this.Title.Equals(eventObj.Title)
                && this.Location.Equals(eventObj.Location)
                && this.Description.Equals(eventObj.Description)
                && this.Duration.Equals(eventObj.Duration)
                && this.RecurrencePattern.Equals(eventObj.RecurrencePattern)
                && this.DateWiseEventCollaborators.Count == eventObj.DateWiseEventCollaborators.Count;

            foreach (var dateWiseEventCollaborators in this.DateWiseEventCollaborators)
            {
                result &= eventObj.DateWiseEventCollaborators.Contains(dateWiseEventCollaborators);
            }

            return result;
        }

        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(this.Id,
                                this.Title,
                                this.Location,
                                this.Description,
                                this.Description,
                                this.RecurrencePattern,
                                this.DateWiseEventCollaborators);
    }
}
