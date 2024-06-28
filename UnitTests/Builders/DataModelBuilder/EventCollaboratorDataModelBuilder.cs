using Infrastructure.DataModels;

namespace UnitTests.Builders.DataModelBuilder;

public class EventCollaboratorDataModelBuilder
{
    private readonly EventCollaboratorDataModel _eventCollaboratorDataModel = new();

    public EventCollaboratorDataModelBuilder WithId(int id)
    {
        _eventCollaboratorDataModel.Id = id;
        return this;
    }

    public EventCollaboratorDataModelBuilder WithEventId(int eventId)
    {
        _eventCollaboratorDataModel.EventId = eventId;
        return this;
    }

    public EventCollaboratorDataModelBuilder WithEventDate(DateOnly eventDate)
    {
        _eventCollaboratorDataModel.EventDate = eventDate;
        return this;
    }

    public EventCollaboratorDataModelBuilder WithUserId(int userId)
    {
        _eventCollaboratorDataModel.UserId = userId;
        return this;
    }

    public EventCollaboratorDataModelBuilder WithEventCollaboratorRole(string eventCollaboratorRole)
    {
        _eventCollaboratorDataModel.EventCollaboratorRole = eventCollaboratorRole;
        return this;
    }

    public EventCollaboratorDataModelBuilder WithConfirmationStatus(string confirmationStatus)
    {
        _eventCollaboratorDataModel.ConfirmationStatus = confirmationStatus;
        return this;
    }

    public EventCollaboratorDataModelBuilder WithProposedStartHour(int? startHour)
    {
        _eventCollaboratorDataModel.ProposedStartHour = startHour;
        return this;
    }

    public EventCollaboratorDataModelBuilder WithProposedEndHour(int? endHour)
    {
        _eventCollaboratorDataModel.ProposedEndHour = endHour;
        return this;
    }

    public EventCollaboratorDataModel Build() => _eventCollaboratorDataModel;
}
