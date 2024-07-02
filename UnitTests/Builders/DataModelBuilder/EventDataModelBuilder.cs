using Infrastructure.DataModels;

namespace UnitTests.Builders.DataModelBuilder;

public class EventDataModelBuilder
{
    private readonly EventDataModel _eventDataModel = new ();

    public EventDataModelBuilder WithTitle(string title)
    {
        _eventDataModel.Title = title;  
        return this;
    }

    public EventDataModelBuilder WithDescription(string description)
    {
        _eventDataModel.Description = description;
        return this;
    }

    public EventDataModelBuilder WithLocation(string location)
    {
        _eventDataModel.Location = location;
        return this;
    }

    public EventDataModelBuilder WithStartHour(int startHour)
    {
        _eventDataModel.StartHour = startHour;
        return this;
    }

    public EventDataModelBuilder WithEndHour(int endHour)
    {
        _eventDataModel.EndHour = endHour;
        return this;
    }

    public EventDataModelBuilder WithStartDate(DateOnly startDate)
    {
        _eventDataModel.StartDate = startDate;
        return this;
    }

    public EventDataModelBuilder WithEndDate(DateOnly endDate)
    {
        _eventDataModel.EndDate = endDate;
        return this;
    }

    public EventDataModelBuilder WithFrequency(string? frequency)
    {
        _eventDataModel.Frequency = frequency;
        return this;
    }

    public EventDataModelBuilder WithInterval(int interval)
    {
        _eventDataModel.Interval = interval;
        return this;
    }

    public EventDataModelBuilder WithWeekOrder(int? weekOrder)
    {
        _eventDataModel.WeekOrder = weekOrder;
        return this;
    }

    public EventDataModelBuilder WithByMonthDay(int? byMonthDay)
    {
        _eventDataModel.ByMonthDay = byMonthDay;
        return this;
    }

    public EventDataModelBuilder WithByMonth(int? byMonth)
    {
        _eventDataModel.ByMonth = byMonth;
        return this;
    }

    public EventDataModelBuilder WithUserId(int userId)
    {
        _eventDataModel.UserId = userId;
        return this;
    }

    public EventDataModelBuilder WithEventCollaborators(List<EventCollaboratorDataModel> eventCollaborators)
    {
        _eventDataModel.EventCollaborators = eventCollaborators;
        return this;
    }

    public EventDataModel Build() => _eventDataModel;
}
