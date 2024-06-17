using AutoMapper;
using Core.Entities;
using Infrastructure.Repositories;
using Infrastructure;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using Infrastructure.DataModels;
using Microsoft.AspNetCore.Identity;

namespace UnitTests.Infrastructure.Repositories.UserRepositoryTests;

public class DeleteUser : IClassFixture<AutoMapperFixture>
{
    private DbContextEventCalendar _dbContext;

    private readonly IMapper _mapper;

    private readonly IConfiguration _configuration;

    private readonly UserManager<UserDataModel> _userManager;

    private readonly SignInManager<UserDataModel> _signInManager;

    public DeleteUser(AutoMapperFixture autoMapperFixture, UserManager<UserDataModel> userManager, SignInManager<UserDataModel> signInManager)
    {
        _mapper = autoMapperFixture.Mapper;
        _configuration = Substitute.For<IConfiguration>();
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [Fact]
    public async Task Should_DeleteUser_When_UserWithIdAvailable()
    {
        _dbContext = await new UserRepositoryDBContext().GetDatabaseContext();

        User user = new()
        {
            Id = 1,
            Name = "a",
            Password = "a",
            Email = "a",
        };

        UserRepository userRepository = new(_dbContext, _mapper, _configuration, _userManager, _signInManager);

        await userRepository.Delete(user);

        User? deletedUser = await userRepository.GetUserById(1);

        Assert.Null(deletedUser);
    }
}
