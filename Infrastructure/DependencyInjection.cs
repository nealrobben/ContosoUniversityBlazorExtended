using Application.Common.Interfaces;
using ContosoUniversityBlazor.Application.Common.Interfaces;
using ContosoUniversityBlazor.Infrastructure.Services;
using ContosoUniversityBlazor.Persistence;
using Infrastructure.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ContosoUniversityBlazor.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
    {
        services.AddTransient<IDateTime, DateTimeService>();

        var useAzureStorageForProfilePictures = configuration.GetValue<bool>("UseAzureStorageForProfilePictures");

        if(useAzureStorageForProfilePictures)
        {
            services.AddSingleton<IProfilePictureService, AzureProfilePictureService>();
        }
        else
        {
            services.AddSingleton<IProfilePictureService, LocalProfilePictureService>();
        }

        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            //Use a random name since otherwise all integration tests running concurrently will use the same in-memory database (based on name)
            var databaseName = $"ContosoUniversityBlazorDb_{Guid.NewGuid()}";

            services.AddDbContext<SchoolContext>(options =>
                options.UseInMemoryDatabase(databaseName));
        }
        else
        {
            services.AddDbContext<SchoolContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(SchoolContext).Assembly.FullName)));
        }

        services.AddScoped<ISchoolContext>(provider => provider.GetService<SchoolContext>());

        return services;
    }
}
