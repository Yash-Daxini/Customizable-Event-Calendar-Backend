using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.IRepositories;
using Core.Interfaces.IServices;
using Core.Services;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

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
    public async Task Should_DeleteUser_When_UserWithIdAvailable()
    {
        User user = new()
        {
            Id = 1,
            Name = "Test",
            Email = "Test@gmail.com",
            Password = "password",
        };

        _userRepository.GetUserById(1).Returns(user);

        await _userService.DeleteUser(user.Id);

        await _userRepository.Received().Delete(user);
    }

    [Fact]
    public async Task Should_Throw_When_UserWithIdNotAvailable()
    {
        User user = new User()
        {
            Id = 1,
        };

        _userRepository.GetUserById(1).ReturnsNull();

        await Assert.ThrowsAsync<NotFoundException>(async () => await _userService.DeleteUser(user.Id));

        await _userRepository.DidNotReceive().Delete(user);
    }

    [Fact]
    public async Task Should_Throw_When_UserIdIsNotValid()
    {
        User user = null;

        _userRepository.GetUserById(-1).ReturnsNull();

        await Assert.ThrowsAsync<ArgumentException>(async () => await _userService.DeleteUser(-1));

        await _userRepository.DidNotReceive().Delete(user);
    }
}
