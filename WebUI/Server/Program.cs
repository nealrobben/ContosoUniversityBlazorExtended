using ContosoUniversityBlazor.Application.System.Commands.SeedData;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebUI.Server.Extensions;

namespace WebUI.Server;

public class Program
{
    public async static Task Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        await MigrateDatabase(host);

        try
        {
            Log.Information("Application Starting.");
            await host.RunAsync();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "The Application failed to start.");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    private static async Task MigrateDatabase(IHost host)
    {
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;

            try
            {
                var context = services.GetRequiredService<ContosoUniversityBlazor.Persistence.SchoolContext>();

                context.Database.EnsureCreated();

                var mediator = services.GetRequiredService<IMediator>();
                await mediator.Send(new SeedDataCommand(), CancellationToken.None);
            }
            catch (Exception ex)
            {
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred while migrating or seeding the database.");

            }
        }
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .UseSerilog()
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}
