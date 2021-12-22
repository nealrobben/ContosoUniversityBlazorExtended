using Bunit;
using WebUI.Client.Pages.Departments;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using WebUI.Client.Services;
using FakeItEasy;
using Microsoft.Extensions.Localization;
using MudBlazor;
using WebUI.Shared.Departments.Queries.GetDepartmentDetails;

namespace WebUI.Client.Test.Pages.Departments
{
    public class DepartmentDetailsTests
    {
        [Fact]
        public void Test1()
        {
            using var ctx = new TestContext();

            var fakeDepartmentService = A.Fake<IDepartmentService>();
            A.CallTo(() => fakeDepartmentService.GetAsync(A<string>.Ignored)).Returns(new DepartmentDetailVM { Name = "TestDepartment" });

            ctx.Services.AddScoped<IDepartmentService>(x => fakeDepartmentService);
            ctx.Services.AddScoped<IStringLocalizer<DepartmentDetails>>(x => A.Fake<IStringLocalizer<DepartmentDetails>>());
            ctx.Services.AddScoped<IDialogService>(x => A.Fake<IDialogService>());

            var cut = ctx.RenderComponent<DepartmentDetails>(parameters => parameters.Add(p => p.DepartmentId, 1));

            Assert.NotNull(cut);
        }
    }
}