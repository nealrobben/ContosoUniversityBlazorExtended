﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace WebUI.Server.Extensions;

public static class HostBuilderExtensions
{
    public static IHostBuilder UseSerilog(this IHostBuilder builder)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.Development.json")
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();
        Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
        SerilogHostBuilderExtensions.UseSerilog(builder);
        return builder;
    }
}
