﻿using Core.Domain;

namespace Core.Interfaces.IRepositories;

public interface IEventCollaboratorRepository
{
    public Task<List<EventCollaborator>> GetAllEventCollaborators();

    public Task<EventCollaborator?> GetEventCollaboratorById(int eventCollaboratorId);

    public Task<int> AddEventCollaborator(EventCollaborator eventCollaborator);

    public Task<int> UpdateEventCollaborator(EventCollaborator eventCollaborator);

    public Task DeleteEventCollaborator(int eventCollaboratorId);

    public Task DeleteEventCollaboratorsByEventId(int eventId);
}
