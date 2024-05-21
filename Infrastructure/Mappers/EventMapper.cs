using Infrastructure.DataModels;
using Core.Domain;

namespace Infrastructure.Mappers;

public class EventMapper
{
    private readonly ParticipantMapper _participantMapper;

    public EventMapper(ParticipantMapper participantMapper)
    {
        _participantMapper = participantMapper;
    }

    public EventModel MapEventEntityToModel(Event eventObj, List<EventCollaborator> eventCollaborators)
    {
        return new EventModel
        {
            Id = eventObj.Id,
            Title = eventObj.Title,
            Description = eventObj.Description,
            Location = eventObj.Location,
            Duration = new DurationMapper().MapDurationModel(eventObj.EventStartHour, eventObj.EventEndHour),
            RecurrencePattern = new RecurrencePatternMapper().MapEventEntityToRecurrencePatternModel(eventObj),
            DateWiseParticipants = GetEventParticipantsByDate(eventCollaborators)
        };
    }

    private List<ParticipantsByDate> GetEventParticipantsByDate(List<EventCollaborator> eventCollaborators)
    {
        return eventCollaborators.GroupBy(eventCollaborator => eventCollaborator.EventDate).Select(eventCollaborator =>
                    new ParticipantsByDate
                    {
                        EventDate = eventCollaborator.Key,
                        Participants = eventCollaborator.Select(eventCollaborator => _participantMapper.MapEventCollaboratorToParticipantModel(eventCollaborator))
                                        .ToList()
                    })
                    .ToList();
    }

    public Event MapEventModelToEntity(EventModel eventModel)
    {
        Event eventObj = new();

        new RecurrencePatternMapper().MapRecurrencePatternModelToEventEntity(eventModel.RecurrencePattern, eventObj);

        eventObj.Id = eventModel.Id;
        eventObj.Title = eventModel.Title;
        eventObj.Description = eventModel.Description;
        eventObj.Location = eventModel.Location;
        eventObj.EventStartHour = eventModel.Duration.StartHour;
        eventObj.EventEndHour = eventModel.Duration.EndHour;
        eventObj.UserId = eventModel.GetEventOrganizer().Id;
        return eventObj;

    }
}
