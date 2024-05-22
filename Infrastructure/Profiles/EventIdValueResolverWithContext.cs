using AutoMapper;
using Core.Domain;
using Infrastructure.DataModels;

namespace Infrastructure.Profiles;

public class EventIdValueResolverWithContext : IValueResolver<Participant,EventCollaboratorDataModel,int>
{
    public int Resolve(Participant source,EventCollaboratorDataModel destination, int eventId, ResolutionContext context)
    {
        eventId = (int)context.Items["EventId"];
        return eventId;
    }
}
