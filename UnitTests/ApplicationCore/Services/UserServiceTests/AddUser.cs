using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.IRepositories;
using Core.Interfaces.IServices;
using Core.Services;
using NSubstitute;
using NullArgumentException = Core.Exceptions.NullArgumentException;

namespace UnitTests.ApplicationCore.Services.UserServiceTests;

public class AddUser
{

    private readonly IUserRepository _userRepository;
    private readonly IUserService _userService;

    public AddUser()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _userService = new UserService(_userRepository);
    }

    [Fact]
    public async Task Should_ReturnUserIdAfterAddOperation_When_CallsRepositoryMethod()
    {
        User user = new()
        {
            Id = 1,
            Name = "Test",
            Email = "Test@gmail.com",
            Password = "password",
        };

        _userRepository.Add(user).Returns(1);

        int userId = await _userService.AddUser(user);

        Assert.Equal(1, userId);

        await _userRepository.Received().Add(user);
    }

    [Fact]
    public async Task Should_ThrowException_When_UserIsNull()
    {
        User user = null;

        _userRepository.Add(user).Returns(1);

        await Assert.ThrowsAsync<NullArgumentException>(async() => await _userService.AddUser(user));

        await _userRepository.DidNotReceive().Add(user);
    }
}
