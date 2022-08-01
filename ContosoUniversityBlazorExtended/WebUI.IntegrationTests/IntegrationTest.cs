using ContosoUniversityBlazor.Persistence;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using WebUI.Server;

namespace WebUI.IntegrationTests
{
    public class IntegrationTest
    {
        protected readonly HttpClient _client;

        protected IntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        services.RemoveAll(typeof(SchoolContext));
                        services.AddDbContext<SchoolContext>(options =>
                        {
                            options.UseInMemoryDatabase("TestDB");
                        });
                    });
                });

            _client = appFactory.CreateClient();
        }
    }
}
