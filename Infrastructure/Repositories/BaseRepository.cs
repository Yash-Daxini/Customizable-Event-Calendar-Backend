using System.Data.Common;
using AutoMapper;
using Core.Interfaces;
using Core.Interfaces.IRepositories;

namespace Infrastructure.Repositories;

public class BaseRepository<TEntity, TModel> : IRepository<TEntity>
    where TEntity : class, IEntity
    where TModel : class, IModel
{
    private readonly DbContextEventCalendar _dbContext;
    private readonly IMapper _mapper;

    public BaseRepository(DbContextEventCalendar dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<int> Add(TEntity entity)
    {
        TModel model = MapEntityToModel(entity);

        await _dbContext.Set<TModel>().AddAsync(model);

        await SaveChanges();

        return model.Id;
    }

    public async Task Delete(TEntity entity)
    {
        TModel model = MapEntityToModel(entity);

        _dbContext.Set<TModel>().Remove(model);

        await SaveChanges();
    }

    public async Task Update(TEntity entity)
    {
        TModel model = MapEntityToModel(entity);

        _dbContext.Set<TModel>().Update(model);

        await SaveChanges();
    }

    private TModel MapEntityToModel(TEntity entity)
    {
        return _mapper.Map<TModel>(entity);
    }

    private async Task SaveChanges()
    {
        await _dbContext.SaveChangesAsync();
    }
}
