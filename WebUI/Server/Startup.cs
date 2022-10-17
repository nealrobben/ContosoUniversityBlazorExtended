using ContosoUniversityBlazor.Application;
using ContosoUniversityBlazor.Application.Common.Interfaces;
using ContosoUniversityBlazor.Infrastructure;
using ContosoUniversityBlazor.Persistence;
using ContosoUniversityBlazor.WebUI.Filters;
using ContosoUniversityBlazor.WebUI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebUI.Shared;

namespace WebUI.Server;

public class Startup
{
    public Startup(IConfiguration configuration, IWebHostEnvironment env)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }
    public IWebHostEnvironment Env { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApplication();
        services.AddInfrastructure(Configuration, Env);
        services.AddShared();

        services.AddScoped<ICurrentUserService, CurrentUserService>();

        services.AddHttpContextAccessor();

        services.AddHealthChecks()
            .AddDbContextCheck<SchoolContext>();

        services.AddControllersWithViews(options =>
            options.Filters.Add(new ApiExceptionFilter()));

        services.AddRazorPages();

        // Customise default API behaviour
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });

        services.AddSwaggerDocument();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseWebAssemblyDebugging();

            //Do NOT use this method with .Net 5, it will give an exception when processing a request
            //app.UseDatabaseErrorPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHealthChecks("/health");

        app.UseBlazorFrameworkFiles();
        app.UseStaticFiles();

        //http://localhost:<port>/swagger to view the Swagger UI.
        //http://localhost:<port>/swagger/v1/swagger.json to view the Swagger specification.

        app.UseOpenApi();
        app.UseSwaggerUi3(settings =>
        {
        });

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapRazorPages();
            endpoints.MapControllers();
            endpoints.MapFallbackToFile("index.html");
        });
    }
}
