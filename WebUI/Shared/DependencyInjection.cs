using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace WebUI.Shared;

public static class DependencyInjection
{
    public static IServiceCollection AddShared(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
}
