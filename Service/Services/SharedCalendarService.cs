using Core.Domain;
using Core.Interfaces.IRepositories;
using Core.Interfaces.IServices;

namespace Core.Services
{
    public class SharedCalendarService : ISharedCalendarService
    {
        private readonly ISharedCalendarRepository _sharedCalendarRepository;

        public SharedCalendarService(ISharedCalendarRepository sharedCalendarRepository)
        {
            _sharedCalendarRepository = sharedCalendarRepository;
        }

        public async Task<List<SharedCalendar>> GetAllSharedCalendars()
        {
            return await _sharedCalendarRepository.GetAllSharedCalendars();
        }

        public async Task<SharedCalendar?> GetSharedCalendarById(int sharedCalendarId)
        {
            return await _sharedCalendarRepository.GetSharedCalendarById(sharedCalendarId);
        }

        public async Task<int> AddSharedCalendar(SharedCalendar sharedCalendarModel)
        {
            return await _sharedCalendarRepository.AddSharedCalendar(sharedCalendarModel);
        }
    }
}
