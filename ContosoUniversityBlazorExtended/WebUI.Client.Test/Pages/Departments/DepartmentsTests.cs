using Bunit;
using FakeItEasy;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using MudBlazor;
using WebUI.Client.Services;
using Xunit;

namespace WebUI.Client.Test.Pages.Departments
{
    public class DepartmentsTests
    {
        //[Fact]
        //public void Departments()
        //{
        //    using var ctx = new TestContext();

        //    var fakeDepartmentService = A.Fake<IDepartmentService>();

        //    ctx.Services.AddScoped<IDepartmentService>(x => fakeDepartmentService);
        //    ctx.Services.AddScoped<IStringLocalizer<Client.Pages.Departments.Departments>>(x => A.Fake<IStringLocalizer<Client.Pages.Departments.Departments>>());
        //    ctx.Services.AddScoped<IDialogService>(x => A.Fake<IDialogService>());
        //    ctx.Services.AddScoped<ISnackbar>(x => A.Fake<ISnackbar>());
        //    //ctx.Services.AddScoped<IDialogService>(x => new DialogService());

        //    var cut = ctx.RenderComponent<Client.Pages.Departments.Departments>();

        //    Assert.NotNull(cut);
        //}
    }
}
