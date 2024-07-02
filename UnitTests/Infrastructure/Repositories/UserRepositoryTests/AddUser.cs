using AutoMapper;
using Core.Entities;
using Infrastructure.Repositories;
using FluentAssertions;
using Infrastructure;
using UnitTests.Builders.EntityBuilder;

namespace UnitTests.Infrastructure.Repositories.UserRepositoryTests;

public class AddUser : UserRepositorySetup, IClassFixture<AutoMapperFixture>
{
    private readonly IMapper _mapper;

    private DbContextEventCalendar _dbContext;

    public AddUser(AutoMapperFixture autoMapperFixture)
    {
        _mapper = autoMapperFixture.Mapper;
    }

    [Fact]
    public async Task Should_Return_AddedUserId_When_UserIsValid()
    {
        _dbContext = new DatabaseBuilder().Build();

        SetUpIndentityObjects(_dbContext);

        User user = new UserBuilder(2)
                    .WithName("b")
                    .WithPassword("bbBB@1")
                    .WithEmail("b@gmail.com")
                    .Build();

        UserRepository userRepository = new(_mapper, _userManager, _signInManager);

        var result = await userRepository.SignUp(user);

        result.Succeeded.Should().BeTrue();
    }
}
