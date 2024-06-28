using Infrastructure.DataModels;

namespace UnitTests.Builders.DataModelBuilder;

public class SharedCalendarDataModelBuilder
{
    private readonly SharedCalendarDataModel _sharedCalendarDataModel = new();

    public SharedCalendarDataModelBuilder WithId(int id)
    {
        _sharedCalendarDataModel.Id = id;
        return this;
    }

    public SharedCalendarDataModelBuilder WithSenderId(int senderId)
    {
        _sharedCalendarDataModel.SenderId = senderId;
        return this;
    }

    public SharedCalendarDataModelBuilder WithReceiverId(int receiverId)
    {
        _sharedCalendarDataModel.ReceiverId = receiverId;
        return this;
    }

    public SharedCalendarDataModelBuilder WithFromDate(DateOnly fromDate)
    {
        _sharedCalendarDataModel.FromDate = fromDate;
        return this;
    }

    public SharedCalendarDataModelBuilder WithToDate(DateOnly toDate)
    {
        _sharedCalendarDataModel.ToDate = toDate;
        return this;
    }

    public SharedCalendarDataModel Build() => _sharedCalendarDataModel;
}
