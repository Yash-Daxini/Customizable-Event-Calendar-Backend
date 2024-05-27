using Infrastructure.DataModels;
using Core.Domain;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Core.Interfaces.IRepositories;
using AutoMapper.QueryableExtensions;

namespace Infrastructure.Repositories;

public class SharedCalendarRepository : ISharedCalendarRepository
{
    private readonly DbContextEventCalendar _dbContext;
    private readonly IMapper _mapper;

    public SharedCalendarRepository(DbContextEventCalendar dbContextEventCalendar, IMapper mapper)
    {
        _dbContext = dbContextEventCalendar;
        _mapper = mapper;
    }

    public async Task<List<SharedCalendar>> GetAllSharedCalendars()
    {
        return await _dbContext
                      .SharedCalendars
                      .Include(sharedCalendar => sharedCalendar.SenderUser)
                      .Include(sharedCalendar => sharedCalendar.ReceiverUser)
                      .ProjectTo<SharedCalendar>(_mapper.ConfigurationProvider)
                      .ToListAsync();
    }

    public async Task<SharedCalendar?> GetSharedCalendarById(int sharedCalendarId)
    {
        return await _dbContext
                      .SharedCalendars
                      .Where(sharedCalendar => sharedCalendar.Id == sharedCalendarId)
                      .Include(sharedCalendar => sharedCalendar.SenderUser)
                      .Include(sharedCalendar => sharedCalendar.ReceiverUser)
                      .ProjectTo<SharedCalendar>(_mapper.ConfigurationProvider)
                      .FirstOrDefaultAsync();


    }

    public async Task<int> AddSharedCalendar(SharedCalendar sharedCalendarModel)
    {
        SharedCalendarDataModel sharedCalendar = _mapper.Map<SharedCalendarDataModel>(sharedCalendarModel);

        _dbContext.Attach(sharedCalendar.SenderUser);
        _dbContext.Attach(sharedCalendar.ReceiverUser);

        await _dbContext.SharedCalendars.AddAsync(sharedCalendar);

        await _dbContext.SaveChangesAsync();

        return sharedCalendar.Id;
    }

    public async Task<int> UpdateSharedCalendar(SharedCalendar sharedCalendarModel)
    {
        SharedCalendarDataModel sharedCalendar = _mapper.Map<SharedCalendarDataModel>(sharedCalendarModel);

        _dbContext.SharedCalendars.Update(sharedCalendar);

        await _dbContext.SaveChangesAsync();

        return sharedCalendar.Id;
    }

    public async Task DeleteSharedCalendar(int sharedCalendarId)
    {
        SharedCalendarDataModel sharedCalendar = new()
        {
            Id = sharedCalendarId
        };

        _dbContext.SharedCalendars.Remove(sharedCalendar);

        await _dbContext.SaveChangesAsync();
    }
}
