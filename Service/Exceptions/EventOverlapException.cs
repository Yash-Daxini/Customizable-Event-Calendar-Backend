using Core.Models;

namespace Core.Exceptions;

public class EventOverlapException: Exception
{
    public OverlapResponseModel OverlapResponseModel { get; }

    public EventOverlapException(OverlapResponseModel overlapResponseModel) => OverlapResponseModel = overlapResponseModel;
}
