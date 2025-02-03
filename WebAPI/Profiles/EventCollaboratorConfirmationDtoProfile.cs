using AutoMapper;
using Core.Entities.Enums;
using Core.Entities;
using WebAPI.Dtos;
using Infrastructure.Extensions;

namespace WebAPI.Profiles;

public class EventCollaboratorConfirmationDtoProfile : Profile
{
    public EventCollaboratorConfirmationDtoProfile()
    {
        CreateMap<EventCollaboratorConfirmationDto, EventCollaborator>()
            .ForMember(dest => dest.ConfirmationStatus, opt => opt.MapFrom(src => src.ConfirmationStatus.ToEnum<ConfirmationStatus>()));
    }
}
