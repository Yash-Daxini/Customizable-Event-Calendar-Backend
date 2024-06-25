using AutoMapper;
using Core.Entities;
using Infrastructure.Repositories;
using NSubstitute;
using Infrastructure.DataModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using NSubstitute.ReturnsExtensions;
using FluentAssertions;
using Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using UnitTests.Builders;

namespace UnitTests.Infrastructure.Repositories.UserRepositoryTests;

public class DeleteUser : IClassFixture<AutoMapperFixture>
{
    private readonly IMapper _mapper;

    private readonly UserManager<UserDataModel> _userManager;

    private readonly SignInManager<UserDataModel> _signInManager;

    public DeleteUser(AutoMapperFixture autoMapperFixture)
    {
        _mapper = autoMapperFixture.Mapper;

        var dbContext = new UserRepositoryDBContext().GetDatabaseContext();

        var services = new ServiceCollection();

        services.AddSingleton(dbContext);

        services.AddIdentity<UserDataModel, IdentityRole<int>>()
            .AddEntityFrameworkStores<DbContextEventCalendar>()
            .AddDefaultTokenProviders();

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        services.AddLogging();

        var serviceProvider = services.BuildServiceProvider();

        _userManager = serviceProvider.GetRequiredService<UserManager<UserDataModel>>();

        _signInManager = serviceProvider.GetRequiredService<SignInManager<UserDataModel>>();
    }

    [Fact]
    public async Task Should_DeleteUser_When_UserWithIdAvailable()
    {
        User user = new UserBuilder(1)
                    .WithName("a")
                    .WithPassword("a")
                    .WithEmail("a")
                    .Build();

        UserRepository userRepository = new(_mapper, _userManager, _signInManager);

        IdentityResult identityResult = await userRepository.Delete(user);

        User? deletedUser = await userRepository.GetUserById(1);

        deletedUser.Should().BeNull();

        identityResult.Should().Be(IdentityResult.Success);
    }
}
