﻿using Core.Entities;

namespace Core.Interfaces.IRepositories
{
    public interface IUserRepository : IRepository<User>
    {
        public Task<User?> GetUserById(int userId);

        public Task<AuthenticateResponse?> AuthenticateUser(User user);
    }
}
