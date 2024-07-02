using AutoMapper;
using Core.Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using FluentAssertions;
using Infrastructure;
using UnitTests.Builders.EntityBuilder;
using Infrastructure.DataModels;
using UnitTests.Builders.DataModelBuilder;

namespace UnitTests.Infrastructure.Repositories.UserRepositoryTests;

public class DeleteUser : UserRepositorySetup, IClassFixture<AutoMapperFixture>
{
    private readonly IMapper _mapper;

    private DbContextEventCalendar _dbContext;

    public DeleteUser(AutoMapperFixture autoMapperFixture)
    {
        _mapper = autoMapperFixture.Mapper;
    }

    [Fact]
    public async Task Should_DeleteUser_When_UserWithIdAvailable()
    {
        UserDataModel userDataModel = new UserDataModelBuilder()
                             .WithId(1)
                             .WithUserName("a")
                             .WithEmail("abc@gmail.com")
                             .Build();

        _dbContext = new DatabaseBuilder()
                    .WithUser(userDataModel)
                    .Build();

        SetUpIndentityObjects(_dbContext);

        User user = new UserBuilder(1)
                    .WithName("a")
                    .WithPassword("a")
                    .WithEmail("a@gmail.com")
                    .Build();

        UserRepository userRepository = new(_mapper, _userManager, _signInManager);

        IdentityResult identityResult = await userRepository.Delete(user);

        User? deletedUser = await userRepository.GetUserById(1);

        deletedUser.Should().BeNull();

        identityResult.Should().Be(IdentityResult.Success);
    }
}
