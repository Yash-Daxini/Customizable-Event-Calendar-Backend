using AutoMapper;
using Core.Domain;
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
            foreach (var participant in participantByDate.Participants)
            {
                EventCollaboratorDataModel eventCollaboratorDataModel = _mapper.Map<EventCollaboratorDataModel>(participant);
                eventCollaboratorDataModel.EventDate = participantByDate.EventDate;
                eventCollaboratorDataModels.Add(eventCollaboratorDataModel);
            }
        }

        return eventCollaboratorDataModels;
    }
}
