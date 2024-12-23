using Infrastructure.DataModels;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Core.Interfaces.IRepositories;
using AutoMapper.QueryableExtensions;
using Core.Entities;

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

    public async Task<List<SharedCalendar>> GetAllSharedCalendars(int userId)
    {
        return _mapper.Map<List<SharedCalendar>>(await _dbContext.SharedCalendars
                               .Where(sharedCalendar => sharedCalendar.ReceiverId == userId || sharedCalendar.SenderId == userId)
                               .Include(sharedCalendar => sharedCalendar.Sender)
                               .Include(sharedCalendar => sharedCalendar.Receiver)
                               .ToListAsync());
    }

    public async Task<SharedCalendar?> GetSharedCalendarById(int sharedCalendarId)
    {
        return _mapper.Map<SharedCalendar>(await _dbContext.SharedCalendars
                               .Include(sharedCalendar => sharedCalendar.Sender)
                               .Include(sharedCalendar => sharedCalendar.Receiver)
                               .FirstOrDefaultAsync(sharedCalendar => sharedCalendar.Id
                                                                    == sharedCalendarId));


    }
}
