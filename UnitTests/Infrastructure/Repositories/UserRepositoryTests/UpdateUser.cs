using AutoMapper;
using Core.Entities;
using Infrastructure.Repositories;
using Infrastructure;
using NSubstitute;
using Infrastructure.DataModels;

namespace UnitTests.Infrastructure.Repositories.UserRepositoryTests;

public class UpdateUser
{
    private DbContextEventCalendar _dbContext;

    private readonly IMapper _mapper;

    public UpdateUser()
    {
        _mapper = Substitute.For<IMapper>();
    }

    [Fact]
    public async Task Should_UpdateUser_When_UserWithIdAvailable()
    {
        _dbContext = await new UserRepositoryDBContext().GetDatabaseContext();

        User user = new()
        {
            Id = 1,
            Name = "b",
            Password = "b",
            Email = "b",
        };

        UserDataModel userDataModel = new()
        {
            Id = 1,
            Name = "b",
            Password = "b",
            Email = "b",
        };

        _mapper.Map<UserDataModel>(user).ReturnsForAnyArgs(userDataModel);

        _mapper.Map<User>(userDataModel).ReturnsForAnyArgs(user);

        _dbContext.ChangeTracker.Clear();

        UserRepository userRepository = new(_dbContext, _mapper);

        await userRepository.Update(user);

        User? updatedUser = await userRepository.GetUserById(1);

        Assert.Equivalent(user, updatedUser);
    }
}
