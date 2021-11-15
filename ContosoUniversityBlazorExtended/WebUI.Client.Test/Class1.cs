using Bunit;
using WebUI.Client.Pages.Departments;
using WebUI.Client.ViewModels.Departments;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using WebUI.Client.Services;
using FakeItEasy;
using Microsoft.Extensions.Localization;
using MudBlazor;

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
            ctx.Services.AddScoped<IDepartmentService>(x => A.Fake<IDepartmentService>());
            ctx.Services.AddScoped<IStringLocalizer<DepartmentResources>>(x => A.Fake<IStringLocalizer<DepartmentResources>>());
            ctx.Services.AddScoped<IStringLocalizer<GeneralResources>>(x => A.Fake<IStringLocalizer<GeneralResources>>());
            ctx.Services.AddScoped<IDialogService>(x => A.Fake<IDialogService>());

            var cut = ctx.RenderComponent<DepartmentDetails>();

            Assert.NotNull(cut);
        }
    }
}