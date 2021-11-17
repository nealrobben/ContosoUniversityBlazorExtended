using Application.Common.Interfaces;
using ContosoUniversityBlazor.Application.Common.Interfaces;
using ContosoUniversityBlazor.Infrastructure.Identity;
using ContosoUniversityBlazor.Infrastructure.Persistence;
using ContosoUniversityBlazor.Infrastructure.Services;
using ContosoUniversityBlazor.Persistence;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ContosoUniversityBlazor.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("ContosoUniversityBlazorDb"));
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            }

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

                services.AddDefaultIdentity<ApplicationUser>()
                    .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

            services.AddTransient<IDateTime, DateTimeService>();
            services.AddTransient<IIdentityService, IdentityService>();

            services.AddSingleton<IProfilePictureService, ProfilePictureService>();

            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<SchoolContext>(options =>
                    options.UseInMemoryDatabase("ContosoUniversityBlazorDb"));
            }
            else
            {
                services.AddDbContext<SchoolContext>(options =>
                    options.UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly(typeof(SchoolContext).Assembly.FullName)));
            }

            services.AddScoped<ISchoolContext>(provider => provider.GetService<SchoolContext>());

            services.AddAuthentication()
                .AddIdentityServerJwt();

            return services;
        }
    }
}
