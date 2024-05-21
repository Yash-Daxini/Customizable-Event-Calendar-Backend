using Infrastructure.DataModels;
using Core.Domain;

namespace Infrastructure.Mappers;

public class SharedCalendarMapper
{
    private readonly UserMapper _userMapper;

    public SharedCalendarMapper(UserMapper userMapper)
    {
        _userMapper = userMapper;
    }

    public SharedCalendarModel MapSharedCalendarEntityToModel(SharedCalendarDataModel sharedCalendar)
    {
        return new SharedCalendarModel()
        {
            Id = sharedCalendar.Id,
            SenderUser = _userMapper.MapUserEntityToModel(sharedCalendar.SenderUser),
            ReceiverUser = _userMapper.MapUserEntityToModel(sharedCalendar.ReceiverUser),
            FromDate = sharedCalendar.FromDate,
            ToDate = sharedCalendar.ToDate
        };
    }

    public SharedCalendarDataModel MapSharedCalendarModelToEntity(SharedCalendarModel sharedCalendarModel)
    {
        return new SharedCalendarDataModel
        {
            Id = sharedCalendarModel.Id,
            ReceiverUserId = sharedCalendarModel.ReceiverUser.Id,
            SenderUserId = sharedCalendarModel.SenderUser.Id,
            FromDate = sharedCalendarModel.FromDate,
            ToDate = sharedCalendarModel.ToDate,
        };
    }
}
