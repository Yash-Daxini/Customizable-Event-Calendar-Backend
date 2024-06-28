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

public class UpdateUser
{

    private readonly IUserRepository _userRepository;
    private readonly IUserService _userService;

    public UpdateUser()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _userService = new UserService(_userRepository);
    }

    [Fact]
    public async Task Should_UpdateUser_When_UserAvailableWithId()
    {
        User user = new UserBuilder(1)
                    .WithName("Test")
                    .WithEmail("Test@gmail.com")
                    .WithPassword("password")
                    .Build();

        _userRepository.GetUserById(1).Returns(user);

        await _userService.UpdateUser(user);

        await _userRepository.Received().Update(user);
    }

    [Fact]
    public async Task Should_Throw_NotFoundException_When_UserNotAvailableWithId()
    {
        User user = new UserBuilder(1)
                    .Build();

        _userRepository.GetUserById(1).ReturnsNull();

        var action = async () => await _userService.UpdateUser(user);

        await action.Should().ThrowAsync<NotFoundException>();

        await _userRepository.DidNotReceive().Update(user);
    }

    [Fact]
    public async Task Should_Throw_NullArgumentException_When_UserIsNull()
    {
        User user = null;

        _userRepository.GetUserById(1).ReturnsNull();

        var action = async () => await _userService.UpdateUser(user);

        await action.Should().ThrowAsync<NullArgumentException>();

        await _userRepository.DidNotReceive().Update(user);
    }
}
