using Core.Domain;

namespace Infrastructure.Repositories;

public interface IEventCollaboratorRepository
{
    public Task<List<Participant>> GetAllParticipants();

    public Task<Participant?> GetParticipantById(int bookId);

    public Task<int> AddParticipant(Participant participantModel, int eventId);

    public Task AddParticipants(List<Participant> participants, int eventId);

    public Task<int> UpdateParticipant(int participantId, Participant participantModel, int eventId);

    public Task DeleteParticipant(int participantId);

    public Task DeleteParticipantsByEventId(int eventId);
}
