using AutoMapper;
using Core.Entities;
using Infrastructure.Repositories;
using NSubstitute;
using Infrastructure.DataModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace UnitTests.Infrastructure.Repositories.UserRepositoryTests;

public class AddUser : IClassFixture<AutoMapperFixture>
{
    private readonly IMapper _mapper;

    private readonly UserManager<UserDataModel> _userManager;
    private readonly SignInManager<UserDataModel> _signInManager;

    public AddUser(AutoMapperFixture autoMapperFixture)
    {
        _mapper = autoMapperFixture.Mapper;

        var userStoreSubstitute = Substitute.For<IUserStore<UserDataModel>>();

        _userManager = Substitute.For<UserManager<UserDataModel>>(
            userStoreSubstitute, null, null, null, null, null, null, null, null);

        var contextAccessorSubstitute = Substitute.For<IHttpContextAccessor>();
        var userClaimsPrincipalFactorySubstitute = Substitute.For<IUserClaimsPrincipalFactory<UserDataModel>>();

        _signInManager= Substitute.For<SignInManager<UserDataModel>>(
            _userManager, contextAccessorSubstitute, userClaimsPrincipalFactorySubstitute, null, null, null, null);
    }

    [Fact]
    public async Task Should_AddUserAndReturnUserId_When_CallsTheRepositoryMethod()
    {
        User user = new()
        {
            Name = "b",
            Password = "b",
            Email = "b",
        };

        UserDataModel userDataModel = new()
        {
            UserName = "b",
            Email = "b",
        };

        UserRepository userRepository = new(_mapper, _userManager, _signInManager);

        _userManager.CreateAsync(userDataModel,"b").ReturnsForAnyArgs(IdentityResult.Success);

        var result = await userRepository.SignUp(user);

        Assert.True(result.Succeeded);
    }
}
