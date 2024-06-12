using AutoMapper;
using Core.Entities;
using Core.Entities.Enums;
using Infrastructure.DataModels;
using Infrastructure.Extensions;

namespace Infrastructure.Profiles;

public class EventCollaboratorProfile : Profile
{

    public EventCollaboratorProfile()
    {
        CreateMap<EventCollaboratorDataModel, EventCollaborator>()
            .ForMember(dest => dest.EventCollaboratorRole, opt => opt.MapFrom(src => src.EventCollaboratorRole.ToEnum<EventCollaboratorRole>()))
            .ForMember(dest => dest.ConfirmationStatus, opt => opt.MapFrom(src => src.ConfirmationStatus.ToEnum<ConfirmationStatus>()))
            .ForMember(dest => dest.ProposedDuration, opt => opt.MapFrom(src => MapDuration(src.ProposedStartHour, src.ProposedEndHour)))
            .ForMember(dest => dest.EventDate, opt => opt.MapFrom(src => src.EventDate));

        CreateMap<EventCollaborator, EventCollaboratorDataModel>()
            .ForMember(dest => dest.EventCollaboratorRole, opt => opt.MapFrom(src => src.EventCollaboratorRole))
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

        return new Duration((int)startHour,(int)endHour);
    }
}
