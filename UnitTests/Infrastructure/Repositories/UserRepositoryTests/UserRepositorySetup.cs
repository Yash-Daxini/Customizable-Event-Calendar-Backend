using Infrastructure;
using Infrastructure.DataModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;

namespace UnitTests.Infrastructure.Repositories.UserRepositoryTests
{
    public class UserRepositorySetup
    {
        protected UserManager<UserDataModel> _userManager;
        protected SignInManager<UserDataModel> _signInManager;

        public void SetUpIndentityObjects(DbContextEventCalendar dbContext)
        {
            var services = new ServiceCollection();

            services.AddSingleton(dbContext);

            services.AddIdentity<UserDataModel, IdentityRole<int>>()
                .AddEntityFrameworkStores<DbContextEventCalendar>()
                .AddDefaultTokenProviders();

            var httpContext = Substitute.For<HttpContext>();

            httpContext.RequestServices.GetService(typeof(IAuthenticationService))
                       .Returns(Substitute.For<IAuthenticationService>());

            var httpContextAccessor = new HttpContextAccessor
            {
                HttpContext = httpContext
            };

            services.AddSingleton<IHttpContextAccessor>(httpContextAccessor);

            services.AddLogging();

            var serviceProvider = services.BuildServiceProvider();

            _userManager = serviceProvider.GetRequiredService<UserManager<UserDataModel>>();

            _signInManager = serviceProvider.GetRequiredService<SignInManager<UserDataModel>>();
        }
    }
}