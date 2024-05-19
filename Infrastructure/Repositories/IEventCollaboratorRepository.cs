using Infrastructure.DataModels;
using Infrastructure.DomainEntities;

namespace Infrastructure.Repositories;

public interface IEventCollaboratorRepository
{
    public Task<List<ParticipantModel>> GetAllParticipants();

    public Task<List<EventCollaborator>> GetAllEventCollaboratorsByEventId(int eventId);

    public Task<ParticipantModel?> GetParticipantById(int bookId);

    public Task<int> AddParticipant(ParticipantModel participantModel, int eventId);

    public Task<int> UpdateParticipant(ParticipantModel participantModel, int eventId);

    public Task DeleteParticipant(int participantId);
}
