using AutoMapper;
using Core.Entities;
using Infrastructure.Repositories;
using Infrastructure.DataModels;
using FluentAssertions;
using Infrastructure;
using UnitTests.Builders.EntityBuilder;
using UnitTests.Builders.DataModelBuilder;

namespace UnitTests.Infrastructure.Repositories.UserRepositoryTests;

public class UpdateUser : UserRepositorySetup, IClassFixture<AutoMapperFixture>
{
    private readonly IMapper _mapper;

    private DbContextEventCalendar _dbContext;

    public UpdateUser(AutoMapperFixture autoMapperFixture)
    {
        _mapper = autoMapperFixture.Mapper;
    }

    [Fact]
    public async Task Should_Return_Success_When_UserAvailableWithId()
    {
        UserDataModel userDataModel = new UserDataModelBuilder()
                             .WithId(1)
                             .WithUserName("a")
                             .WithEmail("abc@gmail.com")
                             .WithSecurityStamp(Guid.NewGuid().ToString())
                             .Build();

        _dbContext = new DatabaseBuilder().WithUser(userDataModel).Build();

        SetUpIndentityObjects(_dbContext);

        User expectedResult = new UserBuilder(1)
                              .WithName("a")
                              .WithEmail("abc@gmail.com")
                              .Build();

        UserRepository userRepository = new(_mapper, _userManager, _signInManager);

        await userRepository.Update(expectedResult);

        User? updatedUser = await userRepository.GetUserById(1);

        updatedUser.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public async Task Should_Return_Failed_When_UserWithIdNotAvailable()
    {
        _dbContext = new DatabaseBuilder().Build();

        SetUpIndentityObjects(_dbContext);

        User user = new UserBuilder(2)
                    .WithName("b")
                    .WithEmail("b")
                    .Build();

        UserRepository userRepository = new(_mapper, _userManager, _signInManager);

        await userRepository.Update(user);

        User? updatedUser = await userRepository.GetUserById(2);

        updatedUser.Should().BeNull();
    }
}
