using Core.Domain;

namespace Core.Interfaces
{
    public interface ISharedCalendarService
    {
        public Task<List<SharedCalendarModel>> GetAllSharedCalendars();

        public Task<SharedCalendarModel?> GetSharedCalendarById(int sharedCalendarId);

        public Task<int> AddSharedCalendar(SharedCalendarModel sharedCalendarModel);

        public Task<EventModel> GetSharedEventsFromSharedCalendarId(int sharedCalendarId);
    }
}
