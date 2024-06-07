using AutoMapper;
using Core.Entities;
using Infrastructure.Repositories;
using Infrastructure;
using NSubstitute;
using Infrastructure.DataModels;

namespace UnitTests.Infrastructure.Repositories.UserRepositoryTests;

public class DeleteUser
{
    private DbContextEventCalendar _dbContext;

    private readonly IMapper _mapper;

    public DeleteUser()
    {
        _mapper = Substitute.For<IMapper>();
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

        UserDataModel userDataModel = new()
        {
            Id = 1,
            Name = "b",
            Password = "b",
            Email = "b",
        };

        _mapper.Map<UserDataModel>(user).ReturnsForAnyArgs(userDataModel);

        _dbContext.ChangeTracker.Clear();

        UserRepository userRepository = new(_dbContext, _mapper);

        await userRepository.Delete(user);

        User? deletedUser = await userRepository.GetUserById(1);

        Assert.Null(deletedUser);
    }
}
