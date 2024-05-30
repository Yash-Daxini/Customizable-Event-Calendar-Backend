using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.IRepositories;
using Core.Interfaces.IServices;

namespace Core.Services;

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
        SharedCalendar? sharedCalendar = await _sharedCalendarRepository.GetSharedCalendarById(sharedCalendarId);

        return sharedCalendar is null
               ? throw new NotFoundException($"SharedCalendar with id {sharedCalendarId} not found.")
               : sharedCalendar;
    }

    public async Task<int> AddSharedCalendar(SharedCalendar sharedCalendarModel)
    {
        return await _sharedCalendarRepository.Add(sharedCalendarModel);
    }
}
