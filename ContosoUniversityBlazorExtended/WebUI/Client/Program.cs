using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
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

            builder.Services.AddScoped<DepartmentService>();
            builder.Services.AddScoped<CourseService>();
            builder.Services.AddScoped<InstructorService>();
            builder.Services.AddScoped<StudentService>();

            builder.Services.AddScoped<DepartmentCreateViewModel>();
            builder.Services.AddScoped<DepartmentDetailsViewModel>();
            builder.Services.AddScoped<DepartmentsViewModel>();
            builder.Services.AddScoped<DepartmentEditViewModel>();

            builder.Services.AddScoped<CourseCreateViewModel>();
            builder.Services.AddScoped<CourseDetailsViewModel>();
            builder.Services.AddScoped<CoursesViewModel>();
            builder.Services.AddScoped<CourseEditViewModel>();

            builder.Services.AddScoped<InstructorCreateViewModel>();
            builder.Services.AddScoped<InstructorDetailsViewModel>();
            builder.Services.AddScoped<InstructorsViewModel>();
            builder.Services.AddScoped<InstructorEditViewModel>();

            builder.Services.AddScoped<StudentCreateViewModel>();
            builder.Services.AddScoped<StudentDetailsViewModel>();
            builder.Services.AddScoped<StudentsViewModel>();
            builder.Services.AddScoped<StudentEditViewModel>();

            await builder.Build().RunAsync();
        }
    }
}
