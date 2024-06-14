using System.Text;
using Core.Interfaces.IRepositories;
using Core.Interfaces.IServices;
using Core.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure;
using Infrastructure.Profiles;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Dtos;
using WebAPI.Profiles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddAutoMapper(typeof(Program),
                               typeof(EventProfile),
                               typeof(EventCollaboratorProfile),
                               typeof(SharedCalendarProfile),
                               typeof(UserProfile),
                               typeof(UserDtoProfile),
                               typeof(RecurringEventRequestDtoProfile),
                               typeof(NonRecurringEventRequestDtoProfile),
                               typeof(EventResponseDtoProfile),
                               typeof(EventCollaboratorRequestDtoProfile),
                               typeof(EventCollaboratorConfirmationDto),
                               typeof(DurationDtoProfile),
                               typeof(SharedCalendarDtoProfile));

builder.Services.AddFluentValidationAutoValidation()
    .AddValidatorsFromAssemblyContaining<Program>();

//Repositories
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IEventRepository, EventRepository>();
builder.Services.AddTransient<IEventCollaboratorRepository, EventCollaboratorRepository>();
builder.Services.AddTransient<ISharedCalendarRepository, SharedCalendarRepository>();

//Services
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IEventService, EventService>();
builder.Services.AddTransient<IEventCollaboratorService, EventCollaboratorService>();
builder.Services.AddTransient<ISharedCalendarService, SharedCalendarService>();
builder.Services.AddTransient<IOverlappingEventService, OverlapEventService>();
builder.Services.AddTransient<IMultipleInviteesEventService, MultipleInviteesEventService>();
builder.Services.AddTransient<IUserAuthenticationService, UserAuthenticationService>();
builder.Services.AddTransient<ISharedEventCollaborationService, SharedEventCollaborationService>();

builder.Services.AddDbContext<DbContextEventCalendar>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("connectionString")));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidAudience = builder.Configuration["JWT:ValidAudience"],
            ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"])),
        };
    });

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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
