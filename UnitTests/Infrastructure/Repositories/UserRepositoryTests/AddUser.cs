using AutoMapper;
using Core.Entities;
using Infrastructure.Repositories;
using Infrastructure;
using NSubstitute;
using Infrastructure.DataModels;

namespace UnitTests.Infrastructure.Repositories.UserRepositoryTests;

public class AddUser
{
    private DbContextEventCalendar _dbContext;

    private readonly IMapper _mapper;

    public AddUser()
    {
        _mapper = Substitute.For<IMapper>();
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

        UserDataModel userDataModel = new()
        {
            Name = "b",
            Password = "b",
            Email = "b",
        };

        _mapper.Map<UserDataModel>(user).ReturnsForAnyArgs(userDataModel);

        _mapper.Map<User>(userDataModel).ReturnsForAnyArgs(user);

        UserRepository userRepository = new(_dbContext, _mapper);

        int id = await userRepository.Add(user);

        user.Id = id;

        User? addedUser = await userRepository.GetUserById(id);

        Assert.Equivalent(user, addedUser);
    }
}
