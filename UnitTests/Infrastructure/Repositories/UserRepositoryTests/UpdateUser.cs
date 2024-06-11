using AutoMapper;
using Core.Entities;
using Infrastructure.Repositories;
using Infrastructure;
using Microsoft.Extensions.Configuration;
using NSubstitute;

namespace UnitTests.Infrastructure.Repositories.UserRepositoryTests;

public class UpdateUser : IClassFixture<AutoMapperFixture>
{
    private DbContextEventCalendar _dbContext;

    private readonly IMapper _mapper;

    private readonly IConfiguration _configuration;

    public UpdateUser(AutoMapperFixture autoMapperFixture)
    {
        _mapper = autoMapperFixture.Mapper;
        _configuration = Substitute.For<IConfiguration>();
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

        UserRepository userRepository = new(_dbContext, _mapper, _configuration);

        await userRepository.Update(user);

        User? updatedUser = await userRepository.GetUserById(1);

        Assert.Equivalent(user, updatedUser);
    }
}
