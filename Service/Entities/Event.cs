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

    public List<EventCollaborator> EventCollaborators { get; set; }

    public User? GetEventOrganizer()
    {
        return EventCollaborators
               ?.FirstOrDefault(eventCollaborator => eventCollaborator
                                                     .IsOrganizer())
               ?.User;
    }

    public List<EventCollaborator> GetEventInvitees()
    {
        if (EventCollaborators is null)
            return [];

        return [.. EventCollaborators
                  .Where(eventCollaborator => eventCollaborator.IsParticipant())
                  .DistinctBy(eventCollaborator => eventCollaborator.User.Id)];
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

        List<DateOnly> eventDates = EventCollaborators
                                    .Select(eventCollaborator => 
                                            eventCollaborator.EventDate)
                                    .ToList();

        List<DateOnly> currentEventDates = currentEvent
                                           .EventCollaborators
                                           .Select(eventCollaboratorByDate => 
                                                   eventCollaboratorByDate.EventDate)
                                           .ToList();

        DateOnly matchedDate = eventDates
                               .Intersect(currentEventDates)
                               .FirstOrDefault();

        return matchedDate == default
               ? null
               : matchedDate;
    }

    public List<EventCollaborator> GetEventCollaboratorsForGivenDate(DateOnly eventDate)
    {
        if (EventCollaborators is null)
            return [];

        return [.. EventCollaborators
                  .Where(eventCollaborator => eventCollaborator.EventDate
                                              == eventDate)];
    }

    public bool IsUserCollaboratedOnGivenDate(int userId, DateOnly eventDate)
    {
        List<EventCollaborator> eventCollaborators = GetEventCollaboratorsForGivenDate(eventDate);

        return eventCollaborators
               .Exists(eventCollaborator => eventCollaborator.User.Id == userId);
    }

    public void PrepareCollaboratorsFromOccurrences(List<DateOnly> occurrences)
    {
        List<EventCollaborator> eventCollaborators = [];

        foreach (var occurrence in occurrences)
        {
            eventCollaborators.AddRange(EventCollaborators
                              .Select(eventCollaborator => new EventCollaborator()
            {
                Id = eventCollaborator.Id,
                EventId = eventCollaborator.EventId,
                EventDate = occurrence,
                User = eventCollaborator.User,
                EventCollaboratorRole = eventCollaborator.EventCollaboratorRole,
                ConfirmationStatus = eventCollaborator.ConfirmationStatus,
                ProposedDuration = eventCollaborator.ProposedDuration,
            }));
        }

        this.EventCollaborators = eventCollaborators;
    }
}
