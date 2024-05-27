using Core.Domain;

namespace Core.Interfaces.IRepositories;

public interface IEventCollaboratorRepository
{
    public Task<List<Participant>> GetAllParticipants();

    public Task<Participant?> GetParticipantById(int participantId);

    public Task<int> AddParticipant(Participant participantModel);

    public Task AddParticipants(List<Participant> participants);

    public Task<int> UpdateParticipant(int participantId, Participant participantModel);

    public Task DeleteParticipant(int participantId);

    public Task DeleteParticipantsByEventId(int eventId);
}
