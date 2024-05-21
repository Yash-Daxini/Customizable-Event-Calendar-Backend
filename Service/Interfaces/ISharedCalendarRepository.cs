using Core.Domain;

namespace Infrastructure.Repositories;

public interface ISharedCalendarRepository
{
    public Task<List<SharedCalendarModel>> GetAllSharedCalendars();

    public Task<SharedCalendarModel?> GetSharedCalendarById(int sharedCalendarId);

    public Task<int> AddSharedCalendar(SharedCalendarModel sharedCalendarModel);

    public Task<int> UpdateSharedCalendar(int sharedCalendarId, SharedCalendarModel sharedCalendarModel);

    public Task DeleteSharedCalendar(int sharedCalendarId);
}
