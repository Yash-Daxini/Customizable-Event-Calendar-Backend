using Core.Entities.Enums;

namespace WebAPI.Dtos;

public class EventCollaboratorResponseDto
{
    public int Id { get; set; }

    public UserResponseDto User { get; set; }

    public string EventCollaboratorRole { get; set; }

    public string ConfirmationStatus { get; set; }

    public DurationDto? ProposedDuration { get; set; }
}
