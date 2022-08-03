using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
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
                    builder.ConfigureAppConfiguration((context, configBuilder) =>
                    {
                        configBuilder.AddInMemoryCollection(
                            new Dictionary<string, string>
                            {
                                ["UseInMemoryDatabase"] = "true"
                            });
                    });
                });

            _client = appFactory.CreateClient();
        }
    }
}
