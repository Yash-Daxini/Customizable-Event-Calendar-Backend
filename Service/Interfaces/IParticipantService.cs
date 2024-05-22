using Core.Domain;

namespace Core.Interfaces;

public interface IParticipantService
{
    public Task<int> AddParticipant(Participant participantModel, int eventId);

    public Task AddParticipants(List<Participant> participants, int eventId);

    public Task<int> UpdateParticipant(int participantId, Participant participantModel, int eventId);

    public Task DeleteParticipant(int participantId);

    public Task DeleteParticipantsByEventId(int eventId);
}
