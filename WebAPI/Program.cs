using System.Text;
using Core.Constants;
using Core.Interfaces.IRepositories;
using Core.Interfaces.IServices;
using Core.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure;
using Infrastructure.DataModels;
using Infrastructure.Profiles;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using WebAPI.Dtos;
using WebAPI.Middlewares;
using WebAPI.Profiles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddAutoMapper(typeof(Program),
                               typeof(EventProfile),
                               typeof(EventCollaboratorProfile),
                               typeof(SharedCalendarProfile),
                               typeof(UserProfile),
                               typeof(UserResponseDtoProfile),
                               typeof(RecurringEventRequestDtoProfile),
                               typeof(NonRecurringEventRequestDtoProfile),
                               typeof(EventResponseDtoProfile),
                               typeof(EventCollaboratorRequestDtoProfile),
                               typeof(EventCollaboratorConfirmationDto),
                               typeof(DurationDtoProfile),
                               typeof(SharedCalendarDtoProfile),
                               typeof(UserRequestDtoProfile));

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
builder.Services.AddTransient<ITokenClaimService, TokenClaimService>();

//Identity
builder.Services.AddIdentity<UserDataModel, IdentityRole<int>>()
    .AddEntityFrameworkStores<DbContextEventCalendar>()
    .AddDefaultTokenProviders();

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
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthorizationConstants.JWT_SECRET_KEY)),
        };
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Customizable Event Calendar",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddCors(p => p.AddPolicy("corspolicy", build =>
{
    build.WithOrigins("http://localhost:5173", "http://localhost").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("corspolicy");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<UserContextMiddleware>();

app.MapControllers();

app.Run();
