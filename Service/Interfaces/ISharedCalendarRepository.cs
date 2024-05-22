using Core.Domain;

namespace Infrastructure.Repositories;

public interface ISharedCalendarRepository
{
    public Task<List<SharedCalendar>> GetAllSharedCalendars();

    public Task<SharedCalendar?> GetSharedCalendarById(int sharedCalendarId);

    public Task<int> AddSharedCalendar(SharedCalendar sharedCalendarModel);

    public Task<int> UpdateSharedCalendar(int sharedCalendarId, SharedCalendar sharedCalendarModel);

    public Task DeleteSharedCalendar(int sharedCalendarId);
}
