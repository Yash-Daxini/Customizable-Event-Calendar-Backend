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

    public async Task<List<SharedCalendar>> GetAllSharedCalendars(int userId)
    {
        return await _sharedCalendarRepository.GetAllSharedCalendars(userId);
    }

    public async Task<List<SharedCalendar>> GetSentSharedCalendars(int userId)
    {
        return await _sharedCalendarRepository.GetSentSharedCalendars(userId);
    }

    public async Task<List<SharedCalendar>> GetReceivedSharedCalendars(int userId)
    {
        return await _sharedCalendarRepository.GetReceivedSharedCalendars(userId);
    }

    public async Task<SharedCalendar?> GetSharedCalendarById(int sharedCalendarId)
    {
        SharedCalendar? sharedCalendar = await _sharedCalendarRepository
                                               .GetSharedCalendarById(sharedCalendarId);

        return sharedCalendar is null
               ? throw new NotFoundException($"SharedCalendar with id" +
                 $" {sharedCalendarId} not found.")
               : sharedCalendar;
    }

    public async Task<int> AddSharedCalendar(SharedCalendar sharedCalendarModel)
    {
        if (sharedCalendarModel is null)
            throw new NullArgumentException($" Shared calendar can't be null");

        return await _sharedCalendarRepository.Add(sharedCalendarModel);
    }
}
