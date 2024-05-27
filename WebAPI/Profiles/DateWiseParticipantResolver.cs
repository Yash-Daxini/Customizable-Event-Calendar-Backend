using AutoMapper;
using Core.Domain;
using WebAPI.Dtos;

namespace WebAPI.Profiles;

public class DateWiseParticipantsResolver : IValueResolver<EventRequestDto, Event, List<ParticipantsByDate>>
{
    private readonly IMapper _mapper;

    public DateWiseParticipantsResolver(IMapper mapper)
    {
        _mapper = mapper;
    }

    public List<ParticipantsByDate> Resolve(EventRequestDto source, Event destination, List<ParticipantsByDate> destMember, ResolutionContext context)
    {
        return [new ParticipantsByDate()
                {
                    EventDate = new DateOnly(),
                    Participants = _mapper.Map<List<Participant>>(source.Participants)
                }
               ];
    }
}
