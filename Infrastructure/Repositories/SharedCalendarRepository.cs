using Infrastructure.DataModels;
using Core.Domain;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Core.Services;

namespace Infrastructure.Repositories;

public class SharedCalendarRepository : ISharedCalendarRepository
{
    private readonly DbContextEventCalendar _dbContextEventCalendar;
    private readonly IMapper _mapper;

    public SharedCalendarRepository(DbContextEventCalendar dbContextEventCalendar, IMapper mapper)
    {
        _dbContextEventCalendar = dbContextEventCalendar;
        _mapper = mapper;
    }

    public async Task<List<SharedCalendar>> GetAllSharedCalendars()
    {
        return await _dbContextEventCalendar
                      .SharedCalendars
                      .Include(sharedCalendar => sharedCalendar.SenderUser)
                      .Include(sharedCalendar => sharedCalendar.ReceiverUser)
                      .Select(sharedCalendar => _mapper.Map<SharedCalendar>(sharedCalendar))
                      .ToListAsync();
    }

    public async Task<SharedCalendar?> GetSharedCalendarById(int sharedCalendarId)
    {
        return await _dbContextEventCalendar
                      .SharedCalendars
                      .Where(sharedCalendar => sharedCalendar.Id == sharedCalendarId)
                      .Include(sharedCalendar => sharedCalendar.SenderUser)
                      .Include(sharedCalendar => sharedCalendar.ReceiverUser)
                      .Select(sharedCalendar => _mapper.Map<SharedCalendar>(sharedCalendar))
                      .FirstOrDefaultAsync();


    }

    public async Task<int> AddSharedCalendar(SharedCalendar sharedCalendarModel)
    {
        SharedCalendarDataModel sharedCalendar = _mapper.Map<SharedCalendarDataModel>(sharedCalendarModel);

        _dbContextEventCalendar.Attach(sharedCalendar.SenderUser);
        _dbContextEventCalendar.Attach(sharedCalendar.ReceiverUser);

        await _dbContextEventCalendar.SharedCalendars.AddAsync(sharedCalendar);

        await _dbContextEventCalendar.SaveChangesAsync();

        return sharedCalendar.Id;
    }

    public async Task<int> UpdateSharedCalendar(int sharedCalendarId, SharedCalendar sharedCalendarModel)
    {
        SharedCalendarDataModel sharedCalendar = _mapper.Map<SharedCalendarDataModel>(sharedCalendarModel);

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
