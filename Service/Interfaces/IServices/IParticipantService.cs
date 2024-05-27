using Core.Domain;

namespace Core.Interfaces.IServices;

public interface IParticipantService
{
    public Task<int> AddParticipant(Participant participantModel);

    public Task AddParticipants(List<Participant> participants);

    public Task<int> UpdateParticipant(int participantId, Participant participantModel);

    public Task DeleteParticipant(int participantId);

    public Task DeleteParticipantsByEventId(int eventId);
}
