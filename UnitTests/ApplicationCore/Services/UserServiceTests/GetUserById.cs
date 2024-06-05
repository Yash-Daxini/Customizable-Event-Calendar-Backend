using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.IRepositories;
using Core.Interfaces.IServices;
using Core.Services;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace UnitTests.ApplicationCore.Services.UserServiceTests;

public class GetUserById
{
    private readonly IUserRepository _userRepository;
    private readonly IUserService _userService;

    public GetUserById()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _userService = new UserService(_userRepository);
    }

    [Fact]
    public async Task Should_ReturnUserWithGivenId_When_UserWithGivenUserIdAvailable()
    {
        User user = new()
        {
            Id = 1,
            Name = "Test",
            Email = "Test@gmail.com",
            Password = "password",
        };

        _userRepository.GetUserById(1).Returns(user);

        User? userById = await _userService.GetUserById(1);

        Assert.Equal(user, userById);

        await _userRepository.Received().GetUserById(1);
    }

    [Fact]
    public async Task Should_ThrowException_When_UserWithGivenUserIdNotAvailable()
    {
        _userRepository.GetUserById(1).ReturnsNull();

        await Assert.ThrowsAsync<NotFoundException>(async () => await _userService.GetUserById(1));

        await _userRepository.Received().GetUserById(1);
    }

    [Fact]
    public async Task Should_ThrowException_When_UserWithGivenUserIdNotValid()
    {
        _userRepository.GetUserById(-11).ReturnsNull();

        await Assert.ThrowsAsync<ArgumentException>(async () => await _userService.GetUserById(-11));

        await _userRepository.DidNotReceive().GetUserById(-1);
    }
}
