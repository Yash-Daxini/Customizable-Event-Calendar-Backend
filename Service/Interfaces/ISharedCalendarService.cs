using Core.Domain;

namespace Core.Interfaces
{
    public interface ISharedCalendarService
    {
        public Task<List<SharedCalendar>> GetAllSharedCalendars();

        public Task<SharedCalendar?> GetSharedCalendarById(int sharedCalendarId);

        public Task<int> AddSharedCalendar(SharedCalendar sharedCalendarModel);
    }
}
