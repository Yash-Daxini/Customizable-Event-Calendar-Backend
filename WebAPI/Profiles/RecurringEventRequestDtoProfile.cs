using AutoMapper;
using Core.Entities;
using WebAPI.Dtos;

namespace WebAPI.Profiles;

public class RecurringEventRequestDtoProfile : Profile
{
    public RecurringEventRequestDtoProfile()
    {
        CreateMap<RecurringEventRequestDto, Event>()
            .ForMember(dest => dest.DateWiseEventCollaborators, opt => opt.MapFrom<RecurringEventDateWiseEventCollaboratorsResolver>());
    }
}
