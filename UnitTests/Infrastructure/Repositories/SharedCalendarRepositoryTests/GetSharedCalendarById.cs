using AutoMapper;
using Core.Entities;
using Infrastructure.Repositories;
using Infrastructure;
using Infrastructure.Profiles;

namespace UnitTests.Infrastructure.Repositories.SharedCalendarRepositoryTests;

public class GetSharedCalendarById
{
    private DbContextEventCalendar _dbContext;
    private readonly IMapper _mapper;
    private readonly List<SharedCalendar> _sharedCalendars;

    public GetSharedCalendarById()
    {
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new EventProfile());
            mc.AddProfile(new SharedCalendarProfile());
            mc.AddProfile(new EventCollaboratorProfile());
            mc.AddProfile(new UserProfile());
        });
        IMapper mapper = mappingConfig.CreateMapper();
        _mapper = mapper;
        _sharedCalendars = [new() {
            Id = 1,
            Sender = new(){
                Id = 1,
                Name = "a",
                Email = "a",
                Password = "a",
            },
            Receiver = new(){
                Id = 2,
                Name = "b",
                Email = "b",
                Password = "b",
            },
            FromDate = new DateOnly(2024,6,7),
            ToDate = new DateOnly(2024,6,7)
        }];
    }

    [Fact]
    public async Task Should_ReturnSharedCalendar_When_SharedCalendarAvailableWithGivenId()
    {
        _dbContext = await new SharedCalendarRepositoryDBContext().GetDatabaseContext();

        SharedCalendarRepository sharedCalendarRepository = new(_dbContext, _mapper);

        SharedCalendar? sharedCalendar = await sharedCalendarRepository.GetSharedCalendarById(1);

        Assert.Equivalent(_sharedCalendars[0], sharedCalendar);
    }

    [Fact]
    public async Task Should_ReturnNull_When_SharedCalendarNotAvailableWithGivenId()
    {
        _dbContext = await new SharedCalendarRepositoryDBContext().GetDatabaseContext();

        SharedCalendarRepository sharedCalendarRepository = new(_dbContext, _mapper);

        SharedCalendar? sharedCalendar = await sharedCalendarRepository.GetSharedCalendarById(2);

        Assert.Null(sharedCalendar);
    }
}
