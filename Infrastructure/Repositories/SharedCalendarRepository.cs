using Infrastructure.DataModels;
using Core.Domain;
using Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class SharedCalendarRepository : ISharedCalendarRepository
{
    private readonly DbContextEventCalendar _dbContextEventCalendar;

    private readonly SharedCalendarMapper _sharedCalendarMapper;

    public SharedCalendarRepository(DbContextEventCalendar dbContextEventCalendar, SharedCalendarMapper sharedCalendarMapper)
    {
        _dbContextEventCalendar = dbContextEventCalendar;
        _sharedCalendarMapper = sharedCalendarMapper;
    }

    public async Task<List<SharedCalendarModel>> GetAllSharedCalendars()
    {
        return await _dbContextEventCalendar
                      .SharedCalendars
                      .Include(sharedCalendar => sharedCalendar.SenderUser)
                      .Include(sharedCalendar => sharedCalendar.ReceiverUser)
                      .Select(sharedCalendar => _sharedCalendarMapper.MapSharedCalendarEntityToModel(sharedCalendar))
                      .ToListAsync();
    }

    public async Task<SharedCalendarModel?> GetSharedCalendarById(int sharedCalendarId)
    {
        return await _dbContextEventCalendar
                      .SharedCalendars
                      .Where(sharedCalendar => sharedCalendar.Id == sharedCalendarId)
                      .Include(sharedCalendar => sharedCalendar.SenderUser)
                      .Include(sharedCalendar => sharedCalendar.ReceiverUser)
                      .Select(sharedCalendar => _sharedCalendarMapper.MapSharedCalendarEntityToModel(sharedCalendar))
                      .FirstOrDefaultAsync();


    }

    public async Task<int> AddSharedCalendar(SharedCalendarModel sharedCalendarModel)
    {
        SharedCalendarDataModel sharedCalendar = _sharedCalendarMapper.MapSharedCalendarModelToEntity(sharedCalendarModel);

        await _dbContextEventCalendar.SharedCalendars.AddAsync(sharedCalendar);

        await _dbContextEventCalendar.SaveChangesAsync();

        return sharedCalendar.Id;
    }

    public async Task<int> UpdateSharedCalendar(int sharedCalendarId, SharedCalendarModel sharedCalendarModel)
    {
        SharedCalendarDataModel sharedCalendar = _sharedCalendarMapper.MapSharedCalendarModelToEntity(sharedCalendarModel);

        _dbContextEventCalendar.SharedCalendars.Update(sharedCalendar);

        await _dbContextEventCalendar.SaveChangesAsync();

        return sharedCalendar.Id;
    }

    public async Task DeleteSharedCalendar(int sharedCalendarId)
    {
        SharedCalendarDataModel sharedCalendar = new()
        {
            Id = sharedCalendarId
        };

        _dbContextEventCalendar.SharedCalendars.Remove(sharedCalendar);

        await _dbContextEventCalendar.SaveChangesAsync();
    }
}
