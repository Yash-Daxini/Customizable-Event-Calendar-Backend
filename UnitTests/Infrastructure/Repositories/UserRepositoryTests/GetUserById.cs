using AutoMapper;
using Core.Entities;
using Infrastructure.Repositories;
using NSubstitute;
using Infrastructure.DataModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using FluentAssertions;

namespace UnitTests.Infrastructure.Repositories.UserRepositoryTests;

public class GetUserById : IClassFixture<AutoMapperFixture>
{
    private readonly IMapper _mapper;

    private readonly UserManager<UserDataModel> _userManager;
    private readonly SignInManager<UserDataModel> _signInManager;

    public GetUserById(AutoMapperFixture autoMapperFixture)
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
    public async Task Should_ReturnUser_When_UserWithIdAvailable()
    {
        User expectedResult = new()
        {

            Id = 1,
            Name = "a",
            Email = "a",
        };

        UserDataModel userDataModel = new()
        {

            Id = 1,
            UserName = "a",
            Email = "a",
        };

        UserRepository userRepository = new(_mapper, _userManager, _signInManager);

        _userManager.FindByIdAsync("1").Returns(userDataModel);

        User? actualResult = await userRepository.GetUserById(1);

        actualResult.Should().BeEquivalentTo(expectedResult);
    }
}
