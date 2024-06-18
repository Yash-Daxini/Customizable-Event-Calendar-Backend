using AutoMapper;
using Core.Entities;
using Infrastructure.Repositories;
using Infrastructure;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using Infrastructure.DataModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace UnitTests.Infrastructure.Repositories.UserRepositoryTests;

public class LogIn : IClassFixture<AutoMapperFixture>
{
    private readonly IMapper _mapper;

    private readonly UserManager<UserDataModel> _userManager;
    private readonly SignInManager<UserDataModel> _signInManager;

    public LogIn(AutoMapperFixture autoMapperFixture)
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
    public async Task Should_ReturnSuccessResult_When_UserWithValidCredentials()
    {
        User user = new()
        {
            Id = 1,
            Name = "a",
            Password = "a",
            Email = "a",
        };

        UserRepository userRepository = new(_mapper, _userManager, _signInManager);

        _signInManager.PasswordSignInAsync(user.Name,user.Password,false,false).Returns(SignInResult.Success);

        SignInResult authResult = await userRepository.LogIn(user);

        Assert.Equal(SignInResult.Success, authResult);
    }

    [Fact]
    public async Task Should_ReturnFailedResult_When_UserWithInValidCredentials()
    {
        User user = new()
        {
            Id = 5,
            Name = "b",
            Password = "b",
            Email = "b",
        };

        UserRepository userRepository = new(_mapper, _userManager, _signInManager);

        _signInManager.PasswordSignInAsync(user.Name, user.Password, false, false).Returns(SignInResult.Failed);

        SignInResult authResponse = await userRepository.LogIn(user);

        Assert.Equal(SignInResult.Failed, authResponse);
    }
}
