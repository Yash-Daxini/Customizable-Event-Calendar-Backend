using AutoMapper;
using Core.Entities;
using Infrastructure.Repositories;
using Infrastructure;
using NSubstitute;
using Infrastructure.DataModels;

namespace UnitTests.Infrastructure.Repositories.UserRepositoryTests;

public class AuthenticateUser
{
    private DbContextEventCalendar _dbContext;

    private readonly IMapper _mapper;

    public AuthenticateUser()
    {
        _mapper = Substitute.For<IMapper>();
    }

    [Fact]
    public async Task Should_ReturnUser_When_UserWithValidCredentials()
    {
        _dbContext = await new UserRepositoryDBContext().GetDatabaseContext();

        User user = new()
        {

            Id = 1,
            Name = "a",
            Password = "a",
            Email = "a",
        };

        UserDataModel userDataModel = new()
        {
            Name = "b",
            Password = "b",
            Email = "b",
        };

        _mapper.Map<UserDataModel>(user).ReturnsForAnyArgs(userDataModel);

        _mapper.Map<User>(userDataModel).ReturnsForAnyArgs(user);

        UserRepository userRepository = new(_dbContext, _mapper);

        User? authenticatedUser = await userRepository.AuthenticateUser(user);

        Assert.Equivalent(user, authenticatedUser);
    }
}
