using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.IRepositories;
using Core.Interfaces.IServices;
using Core.Services;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using UnitTests.Builders.EntityBuilder;

namespace UnitTests.ApplicationCore.Services.UserServiceTests;

public class DeleteUser
{

    private readonly IUserRepository _userRepository;
    private readonly IUserService _userService;

    public DeleteUser()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _userService = new UserService(_userRepository);
    }

    [Fact]
    public async Task Should_DeleteUser_When_UserAvailableWithId()
    {
        User user = new UserBuilder(1)
                    .WithName("Test")
                    .WithEmail("Test@gmail.com")
                    .WithPassword("password")
                    .Build();

        _userRepository.GetUserById(1).Returns(user);

        await _userService.DeleteUser(user.Id);

        await _userRepository.Received().Delete(user);
    }

    [Fact]
    public async Task Should_Throw_NotFoundException_When_UserNotAvailableWithId()
    {
        User user = new UserBuilder(1)
                    .Build();

        _userRepository.GetUserById(1).ReturnsNull();

        var action = async () => await _userService.DeleteUser(user.Id);

        await action.Should().ThrowAsync<NotFoundException>();

        await _userRepository.DidNotReceive().Delete(user);
    }

    [Fact]
    public async Task Should_Throw_NotFoundException_When_UserIdIsNotValid()
    {
        User user = null;

        _userRepository.GetUserById(-1).ReturnsNull();

        var action = async () => await _userService.DeleteUser(-1);

        await action.Should().ThrowAsync<NotFoundException>();

        await _userRepository.DidNotReceive().Delete(user);
    }
}
