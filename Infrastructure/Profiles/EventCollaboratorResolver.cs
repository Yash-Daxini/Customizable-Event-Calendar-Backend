using AutoMapper;
using Core.Domain.Models;
using Infrastructure.DataModels;

namespace Infrastructure.Profiles;

public class EventCollaboratorResolver : IValueResolver<Event, EventDataModel, List<EventCollaboratorDataModel>>
{
    private readonly IMapper _mapper;

    public EventCollaboratorResolver(IMapper mapper)
    {
        _mapper = mapper;
    }

    public List<EventCollaboratorDataModel> Resolve(Event source, EventDataModel destination, List<EventCollaboratorDataModel> destMember, ResolutionContext context)
    {
        List<EventCollaboratorDataModel> eventCollaboratorDataModels = [];

        foreach (var participantByDate in source.DateWiseParticipants)
        {
            foreach (var eventCollaborator in participantByDate.EventCollaborators)
            {
                EventCollaboratorDataModel eventCollaboratorDataModel = _mapper.Map<EventCollaboratorDataModel>(eventCollaborator);
                eventCollaboratorDataModel.EventDate = participantByDate.EventDate;
                eventCollaboratorDataModels.Add(eventCollaboratorDataModel);
            }
        }

        return eventCollaboratorDataModels;
    }
}
