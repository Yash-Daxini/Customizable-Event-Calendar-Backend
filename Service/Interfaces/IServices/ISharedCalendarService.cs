using Core.Entities;

namespace Core.Interfaces.IServices
{
    public interface ISharedCalendarService
    {
        public Task<List<SharedCalendar>> GetAllSharedCalendars(int userId);

        public Task<SharedCalendar?> GetSharedCalendarById(int sharedCalendarId);

        public Task<int> AddSharedCalendar(SharedCalendar sharedCalendarModel);
    }
}
