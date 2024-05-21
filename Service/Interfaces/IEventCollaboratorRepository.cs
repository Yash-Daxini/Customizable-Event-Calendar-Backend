using Core.Domain;

namespace Infrastructure.Repositories;

public interface IEventCollaboratorRepository
{
    public Task<List<ParticipantModel>> GetAllParticipants();

    public Task<ParticipantModel?> GetParticipantById(int bookId);

    public Task<int> AddParticipant(ParticipantModel participantModel, int eventId);

    public Task<int> UpdateParticipant(int participantId, ParticipantModel participantModel, int eventId);

    public Task DeleteParticipant(int participantId);
}
