using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using WebUI.Client.Services;
using WebUI.Client.ViewModels.Courses;
using WebUI.Client.ViewModels.Departments;
using WebUI.Client.ViewModels.Instructors;
using WebUI.Client.ViewModels.Students;
using System.Globalization;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components.Web;

namespace WebUI.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddMudServices();

            builder.Services.AddScoped<IDepartmentService,DepartmentService>();
            builder.Services.AddScoped<ICourseService, CourseService>();
            builder.Services.AddScoped<IInstructorService, InstructorService>();
            builder.Services.AddScoped<IStudentService, StudentService>();
            builder.Services.AddScoped<FileuploadService>();

            builder.Services.AddTransient<DepartmentCreateViewModel>();
            builder.Services.AddTransient<DepartmentDetailsViewModel>();
            builder.Services.AddTransient<DepartmentsViewModel>();
            builder.Services.AddTransient<DepartmentEditViewModel>();

            builder.Services.AddTransient<CourseCreateViewModel>();
            builder.Services.AddTransient<CourseDetailsViewModel>();
            builder.Services.AddTransient<CoursesViewModel>();
            builder.Services.AddTransient<CourseEditViewModel>();

            builder.Services.AddTransient<InstructorCreateViewModel>();
            builder.Services.AddTransient<InstructorDetailsViewModel>();
            builder.Services.AddTransient<InstructorsViewModel>();
            builder.Services.AddTransient<InstructorEditViewModel>();

            builder.Services.AddTransient<StudentCreateViewModel>();
            builder.Services.AddTransient<StudentDetailsViewModel>();
            builder.Services.AddTransient<StudentsViewModel>();
            builder.Services.AddTransient<StudentEditViewModel>();

            builder.Services.AddLocalization(opts => { opts.ResourcesPath = "Localization"; });

            var host = builder.Build();

            CultureInfo culture;
            var js = host.Services.GetRequiredService<IJSRuntime>();
            var result = await js.InvokeAsync<string>("blazorCulture.get");

            if (result != null)
            {
                culture = new CultureInfo(result);
            }
            else
            {
                culture = new CultureInfo("en-US");
                await js.InvokeVoidAsync("blazorCulture.set", "en-US");
            }

            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;

            await host.RunAsync();
        }
    }
}
