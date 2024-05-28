using Infrastructure.DataModels;
using Core.Domain;
using AutoMapper;
using Core.Interfaces.IRepositories;

namespace Infrastructure.Repositories
{
    public class EventCollaboratorRepository : IEventCollaboratorRepository
    {
        private readonly DbContextEventCalendar _dbContext;

        private readonly IMapper _mapper;

        public EventCollaboratorRepository(DbContextEventCalendar dbContextEvent, IMapper mapper)
        {
            _dbContext = dbContextEvent;
            _mapper = mapper;
        }

        public async Task<int> AddEventCollaborator(EventCollaborator eventCollaborator)
        {
            EventCollaboratorDataModel eventCollaboratorDataModel = _mapper.Map<EventCollaboratorDataModel>(eventCollaborator);

            _dbContext.EventCollaborators.Add(eventCollaboratorDataModel);

            await _dbContext.SaveChangesAsync();

            return eventCollaborator.Id;
        }

        public async Task<int> UpdateEventCollaborator(EventCollaborator eventCollaborator)
        {
            EventCollaboratorDataModel eventCollaboratorDataModel = _mapper.Map<EventCollaboratorDataModel>(eventCollaborator);

            _dbContext.EventCollaborators.Update(eventCollaboratorDataModel);

            await _dbContext.SaveChangesAsync();

            return eventCollaboratorDataModel.Id;
        }

        public async Task DeleteEventCollaboratorsByEventId(int eventId)
        {
            List<EventCollaboratorDataModel> eventCollaboratorsToDelete = [.._dbContext
                                                                          .EventCollaborators
                                                                          .Where(eventcollaborator => eventcollaborator.EventId == eventId)];

            _dbContext.RemoveRange(eventCollaboratorsToDelete);

            await _dbContext.SaveChangesAsync();
        }
    }
}
