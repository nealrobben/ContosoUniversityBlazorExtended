using Bunit;
using WebUI.Client.Pages.Departments;
using WebUI.Client.ViewModels.Departments;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using WebUI.Client.Services;

namespace WebUI.Client.Test
{
    public class Class1
    {
        [Fact]
        public void Test1()
        {
            //Assert.Equal(1, 1);

            using var ctx = new TestContext();
            ctx.Services.AddScoped<DepartmentDetailsViewModel>();
            ctx.Services.AddScoped<DepartmentService>(); //TODO: add interface, import FakeXrmEasy

            var cut = ctx.RenderComponent<DepartmentDetails>();

            Assert.NotNull(cut);
        }
    }
}