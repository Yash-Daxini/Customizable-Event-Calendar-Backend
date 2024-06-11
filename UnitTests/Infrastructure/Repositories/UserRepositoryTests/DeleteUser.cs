﻿using AutoMapper;
using Core.Entities;
using Infrastructure.Repositories;
using Infrastructure;
using Microsoft.Extensions.Configuration;
using NSubstitute;

namespace UnitTests.Infrastructure.Repositories.UserRepositoryTests;

public class DeleteUser : IClassFixture<AutoMapperFixture>
{
    private DbContextEventCalendar _dbContext;

    private readonly IMapper _mapper;

    private readonly IConfiguration _configuration;

    public DeleteUser(AutoMapperFixture autoMapperFixture)
    {
        _mapper = autoMapperFixture.Mapper;
        _configuration = Substitute.For<IConfiguration>();
    }

    [Fact]
    public async Task Should_DeleteUser_When_UserWithIdAvailable()
    {
        _dbContext = await new UserRepositoryDBContext().GetDatabaseContext();

        User user = new()
        {
            Id = 1,
            Name = "a",
            Password = "a",
            Email = "a",
        };

        _dbContext.ChangeTracker.Clear();

        UserRepository userRepository = new(_dbContext, _mapper, _configuration);

        await userRepository.Delete(user);

        User? deletedUser = await userRepository.GetUserById(1);

        Assert.Null(deletedUser);
    }
}
