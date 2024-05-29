using Core.Domain.Models;

namespace Core.Interfaces.IServices
{
    public interface ISharedCalendarService
    {
        public Task<List<SharedCalendar>> GetAllSharedCalendars();

        public Task<SharedCalendar?> GetSharedCalendarById(int sharedCalendarId);

        public Task<int> AddSharedCalendar(SharedCalendar sharedCalendarModel);
    }
}
