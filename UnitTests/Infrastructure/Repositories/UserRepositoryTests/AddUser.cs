using AutoMapper;
using Core.Entities;
using Infrastructure.Repositories;
using Infrastructure;
using Microsoft.Extensions.Configuration;
using NSubstitute;

namespace UnitTests.Infrastructure.Repositories.UserRepositoryTests;

public class AddUser : IClassFixture<AutoMapperFixture>
{
    private DbContextEventCalendar _dbContext;

    private readonly IMapper _mapper;

    private readonly IConfiguration _configuration;

    public AddUser(AutoMapperFixture autoMapperFixture)
    {
        _mapper = autoMapperFixture.Mapper;
        _configuration = Substitute.For<IConfiguration>();
    }

    [Fact]
    public async Task Should_AddUserAndReturnUserId_When_CallsTheRepositoryMethod()
    {
        _dbContext = await new UserRepositoryDBContext().GetDatabaseContext();

        User user = new()
        {
            Name = "b",
            Password = "b",
            Email = "b",
        };

        UserRepository userRepository = new(_dbContext, _mapper, _configuration);

        int id = await userRepository.Add(user);

        user.Id = id;

        User? addedUser = await userRepository.GetUserById(id);

        Assert.Equivalent(user, addedUser);
    }
}
