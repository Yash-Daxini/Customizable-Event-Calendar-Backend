using AutoMapper;
using Core.Entities;
using Infrastructure.Repositories;
using Infrastructure;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using Microsoft.AspNetCore.Identity;
using Infrastructure.DataModels;
using Microsoft.AspNetCore.Http;
using NSubstitute.ReturnsExtensions;

namespace UnitTests.Infrastructure.Repositories.UserRepositoryTests;

public class UpdateUser : IClassFixture<AutoMapperFixture>
{
    private readonly IMapper _mapper;

    private readonly UserManager<UserDataModel> _userManager;
    private readonly SignInManager<UserDataModel> _signInManager;

    public UpdateUser(AutoMapperFixture autoMapperFixture)
    {
        _mapper = autoMapperFixture.Mapper;
        var userStoreSubstitute = Substitute.For<IUserStore<UserDataModel>>();

        _userManager = Substitute.For<UserManager<UserDataModel>>(
            userStoreSubstitute, null, null, null, null, null, null, null, null);

        var contextAccessorSubstitute = Substitute.For<IHttpContextAccessor>();
        var userClaimsPrincipalFactorySubstitute = Substitute.For<IUserClaimsPrincipalFactory<UserDataModel>>();

        _signInManager = Substitute.For<SignInManager<UserDataModel>>(
            _userManager, contextAccessorSubstitute, userClaimsPrincipalFactorySubstitute, null, null, null, null);
    }

    [Fact]
    public async Task Should_UpdateUserAndReturnSuccess_When_UserWithIdAvailable()
    {
        User expectedResult = new()
        {
            Id = 1,
            Name = "b",
            Email = "b",
        };

        UserDataModel userDataModel = new()
        {
            Id = 1,
            UserName = "b",
            Email = "b",
        };

        UserRepository userRepository = new(_mapper, _userManager, _signInManager);

        _userManager.UpdateAsync(userDataModel).Returns(IdentityResult.Success);

        _userManager.FindByIdAsync("1").Returns(userDataModel);

        await userRepository.Update(expectedResult);

        User? updatedUser = await userRepository.GetUserById(1);

        Assert.Equivalent(expectedResult, updatedUser);
    }

    [Fact]
    public async Task Should_ReturnFailed_When_UserWithIdNotAvailable()
    {
        User user = new()
        {
            Id = 1,
            Name = "b",
            Email = "b",
        };

        UserDataModel userDataModel = null;

        UserRepository userRepository = new(_mapper, _userManager, _signInManager);

        _userManager.UpdateAsync(userDataModel).Returns(IdentityResult.Failed());

        _userManager.FindByIdAsync("1").ReturnsNull();

        await userRepository.Update(user);

        User? updatedUser = await userRepository.GetUserById(1);

        Assert.Null(updatedUser);
    }
}
