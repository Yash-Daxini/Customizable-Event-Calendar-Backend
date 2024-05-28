namespace Core.Interfaces.IServices;

public interface IMultipleInviteesEventService
{
    public Task StartSchedulingProcessOfProposedEvent(int userId);
}
