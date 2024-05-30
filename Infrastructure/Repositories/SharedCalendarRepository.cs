using Infrastructure.DataModels;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Core.Interfaces.IRepositories;
using AutoMapper.QueryableExtensions;
using Core.Domain.Models;

namespace Infrastructure.Repositories;

public class SharedCalendarRepository : BaseRepository<SharedCalendar, SharedCalendarDataModel>, ISharedCalendarRepository
{
    private readonly DbContextEventCalendar _dbContext;
    private readonly IMapper _mapper;

    public SharedCalendarRepository(DbContextEventCalendar dbContextEventCalendar, IMapper mapper) 
                                   : base(dbContextEventCalendar, mapper)
    {
        _dbContext = dbContextEventCalendar;
        _mapper = mapper;
    }

    public async Task<List<SharedCalendar>> GetAllSharedCalendars()
    {
        return await _dbContext.SharedCalendars
                               .Include(sharedCalendar => sharedCalendar.Sender)
                               .Include(sharedCalendar => sharedCalendar.Receiver)
                               .ProjectTo<SharedCalendar>(_mapper.ConfigurationProvider)
                               .ToListAsync();
    }

    public async Task<SharedCalendar?> GetSharedCalendarById(int sharedCalendarId)
    {
        return await _dbContext.SharedCalendars
                               .Where(sharedCalendar => sharedCalendar.Id == sharedCalendarId)
                               .Include(sharedCalendar => sharedCalendar.Sender)
                               .Include(sharedCalendar => sharedCalendar.Receiver)
                               .ProjectTo<SharedCalendar>(_mapper.ConfigurationProvider)
                               .FirstOrDefaultAsync();


    }
}
