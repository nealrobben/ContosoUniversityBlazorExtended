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

namespace WebUI.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddMudServices();

            builder.Services.AddScoped<DepartmentService>();
            builder.Services.AddScoped<CourseService>();
            builder.Services.AddScoped<InstructorService>();
            builder.Services.AddScoped<StudentService>();
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

            await builder.Build().RunAsync();
        }
    }
}
