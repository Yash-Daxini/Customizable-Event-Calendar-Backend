using Core.Domain;

namespace Core.Interfaces;

public interface IParticipantService
{
    public Task<int> AddParticipant(ParticipantModel participantModel, int eventId);

    public Task<int> UpdateParticipant(int participantId, ParticipantModel participantModel, int eventId);

    public Task DeleteParticipant(int participantId);
}
