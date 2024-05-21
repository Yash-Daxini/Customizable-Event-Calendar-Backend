using Core.Domain;
using Core.Interfaces;
using Infrastructure.Repositories;

namespace Core.Services
{
    public class SharedCalendarService : ISharedCalendarService
    {
        private readonly ISharedCalendarRepository _sharedCalendarRepository;

        public SharedCalendarService(ISharedCalendarRepository sharedCalendarRepository)
        {
            _sharedCalendarRepository = sharedCalendarRepository;
        }

        public Task<List<SharedCalendarModel>> GetAllSharedCalendars()
        {
            return _sharedCalendarRepository.GetAllSharedCalendars();
        }

        public Task<SharedCalendarModel?> GetSharedCalendarById(int sharedCalendarId)
        {
            return _sharedCalendarRepository.GetSharedCalendarById(sharedCalendarId);
        }

        public Task<int> AddSharedCalendar(SharedCalendarModel sharedCalendarModel)
        {
            return _sharedCalendarRepository.AddSharedCalendar(sharedCalendarModel);
        }

        public Task<EventModel> GetSharedEventsFromSharedCalendarId(int sharedCalendarId)
        {
            throw new NotImplementedException();
        }
    }
}
