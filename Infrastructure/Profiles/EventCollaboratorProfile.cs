using AutoMapper;
using Core.Domain.Enums;
using Core.Domain.Models;
using Infrastructure.DataModels;
using Infrastructure.Extensions;

namespace Infrastructure.Profiles;

public class EventCollaboratorProfile : Profile
{

    public EventCollaboratorProfile()
    {
        CreateMap<EventCollaboratorDataModel, EventCollaborator>()
            .ForMember(dest => dest.EventCollaboratorRole, opt => opt.MapFrom(src => src.ParticipantRole.ToEnum<EventCollaboratorRole>()))
            .ForMember(dest => dest.ConfirmationStatus, opt => opt.MapFrom(src => src.ConfirmationStatus.ToEnum<ConfirmationStatus>()))
            .ForMember(dest => dest.ProposedDuration, opt => opt.MapFrom(src => MapDuration(src.ProposedStartHour, src.ProposedEndHour)))
            .ForMember(dest => dest.EventDate, opt => opt.MapFrom(src => src.EventDate));

        CreateMap<EventCollaborator, EventCollaboratorDataModel>()
            .ForMember(dest => dest.ParticipantRole, opt => opt.MapFrom(src => src.EventCollaboratorRole))
            .ForMember(dest => dest.ConfirmationStatus, opt => opt.MapFrom(src => src.ConfirmationStatus))
            .ForMember(dest => dest.ProposedStartHour, opt => opt.MapFrom(src => MapProposedStartHour(src.ProposedDuration)))
            .ForMember(dest => dest.ProposedEndHour, opt => opt.MapFrom(src => MapProposedEndHour(src.ProposedDuration)))
            .ForMember(dest => dest.EventDate, opt => opt.MapFrom(src => src.EventDate))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.Id))
            .ForMember(dest => dest.User, opt => opt.Ignore());
    }

    private static object? MapProposedStartHour(Duration proposedDuration)
    {
        return proposedDuration?.StartHour;
    }

    private static object? MapProposedEndHour(Duration proposedDuration)
    {
        return proposedDuration?.EndHour;
    }

    private static Duration? MapDuration(int? startHour, int? endHour)
    {
        if (startHour is null || endHour is null) return null;

        return new Duration
        {
            StartHour = (int)startHour,
            EndHour = (int)endHour
        };
    }
}
