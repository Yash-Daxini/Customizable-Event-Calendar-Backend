using AutoMapper;
using Core.Entities;
using Infrastructure.Repositories;
using Infrastructure;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using Microsoft.AspNetCore.Identity;
using Infrastructure.DataModels;

namespace UnitTests.Infrastructure.Repositories.UserRepositoryTests;

public class UpdateUser : IClassFixture<AutoMapperFixture>
{
    private DbContextEventCalendar _dbContext;

    private readonly IMapper _mapper;

    private readonly IConfiguration _configuration;

    private readonly UserManager<UserDataModel> _userManager;
    private readonly SignInManager<UserDataModel> _signInManager;

    public UpdateUser(AutoMapperFixture autoMapperFixture, UserManager<UserDataModel> userManager, SignInManager<UserDataModel> signInManager)
    {
        _mapper = autoMapperFixture.Mapper;
        _configuration = Substitute.For<IConfiguration>();
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [Fact]
    public async Task Should_UpdateUser_When_UserWithIdAvailable()
    {
        _dbContext = await new UserRepositoryDBContext().GetDatabaseContext();

        User user = new()
        {
            Id = 1,
            Name = "b",
            Password = "b",
            Email = "b",
        };

        UserRepository userRepository = new(_dbContext, _mapper, _configuration, _userManager, _signInManager);

        await userRepository.Update(user);

        User? updatedUser = await userRepository.GetUserById(1);

        Assert.Equivalent(user, updatedUser);
    }
}
