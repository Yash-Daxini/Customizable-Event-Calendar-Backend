using Core.Interfaces;
using Core.Services;
using Infrastructure;
using Infrastructure.Mappers;
using Infrastructure.Profiles;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(Program), typeof(EventProfile),typeof(ParticipantProfile));
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IEventRepository, EventRepository>();
builder.Services.AddTransient<IEventCollaboratorRepository, EventCollaboratorRepository>();
builder.Services.AddTransient<ISharedCalendarRepository, SharedCalendarRepository>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IEventService, EventService>();
builder.Services.AddTransient<IParticipantService, ParticipantService>();
builder.Services.AddTransient<ISharedCalendarService, SharedCalendarService>();
builder.Services.AddTransient<RecurrenceService>();
builder.Services.AddTransient<SharedCalendarMapper>();
builder.Services.AddTransient<EventMapper>();
builder.Services.AddTransient<ParticipantMapper>();
builder.Services.AddTransient<UserMapper>();
builder.Services.AddDbContext<DbContextEventCalendar>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("connectionString")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
