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

        foreach (var eventCollaboratorByDate in source.DateWiseEventCollaborators)
        {
            foreach (var eventCollaborator in eventCollaboratorByDate.EventCollaborators)
            {
                EventCollaboratorDataModel eventCollaboratorDataModel = _mapper.Map<EventCollaboratorDataModel>(eventCollaborator);
                eventCollaboratorDataModel.EventDate = eventCollaboratorByDate.EventDate;
                eventCollaboratorDataModels.Add(eventCollaboratorDataModel);
            }
        }

        return eventCollaboratorDataModels;
    }
}
