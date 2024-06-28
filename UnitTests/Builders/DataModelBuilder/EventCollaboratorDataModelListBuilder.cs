using Infrastructure.DataModels;

namespace UnitTests.Builders.DataModelBuilder;

public class EventCollaboratorDataModelListBuilder
{
    private readonly List<EventCollaboratorDataModel> _eventCollaboratorDataModels = [];

    private readonly int EventId;

    public EventCollaboratorDataModelListBuilder(int eventId)
    {
        EventId = eventId;        
    }

    public EventCollaboratorDataModelListBuilder WithOrganizer(int userId,DateOnly eventDate)
    {
        _eventCollaboratorDataModels.Add(new EventCollaboratorDataModelBuilder()
                                         .WithEventCollaboratorRole("Organizer")
                                         .WithConfirmationStatus("Accept")
                                         .WithEventId(EventId)
                                         .WithUserId(userId)
                                         .WithEventDate(eventDate)
                                         .Build());

        return this;
    }

    public EventCollaboratorDataModelListBuilder WithParticipant(int userId, 
                                                                 string confirmationStatus ,
                                                                 DateOnly eventDate,
                                                                 int? proposedStartHour,
                                                                 int? proposedEndHour)
    {
        _eventCollaboratorDataModels.Add(new EventCollaboratorDataModelBuilder()
                                         .WithEventCollaboratorRole("Participant")
                                         .WithConfirmationStatus(confirmationStatus)
                                         .WithEventId(EventId)
                                         .WithUserId(userId)
                                         .WithEventDate(eventDate)
                                         .WithProposedStartHour(proposedStartHour)
                                         .WithProposedEndHour(proposedEndHour)
                                         .Build());

        return this;
    }

    public List<EventCollaboratorDataModel> Build() => _eventCollaboratorDataModels;
}
