using Bunit;
using WebUI.Client.Pages.Departments;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using WebUI.Client.Services;
using FakeItEasy;
using Microsoft.Extensions.Localization;
using MudBlazor;
using WebUI.Shared.Departments.Queries.GetDepartmentDetails;
using FluentAssertions;
using WebUI.Client.Test.Extensions;

namespace WebUI.Client.Test.Pages.Departments
{
    public class DepartmentDetailsTests : BunitTestBase
    {
        [Fact]
        public async Task DepartmentDetails()
        {
            var fakeDepartmentService = A.Fake<IDepartmentService>();
            A.CallTo(() => fakeDepartmentService.GetAsync(A<string>.Ignored)).Returns(new DepartmentDetailVM { Name = "TestDepartment", 
                Budget = 123, StartDate = new DateTime(2021,3,1), AdministratorName = "Admin" });

            Context.Services.AddScoped<IDepartmentService>(x => fakeDepartmentService);
            Context.Services.AddScoped<IStringLocalizer<DepartmentDetails>>(x => A.Fake<IStringLocalizer<DepartmentDetails>>());

            var comp = Context.RenderComponent<MudDialogProvider>();
            Assert.Empty(comp.Markup.Trim());

            var service = Context.Services.GetService<IDialogService>() as DialogService;
            Assert.NotNull(service);
            IDialogReference? dialogReference = null;

            var parameters = new DialogParameters();
            parameters.Add("DepartmentId", 1);

            var title = "Department Details";
            await comp.InvokeAsync(() => dialogReference = service?.Show<DepartmentDetails>(title, parameters));
            Assert.NotNull(dialogReference);

            Assert.NotEmpty(comp.Markup.Trim());

            comp.Find("h6").TrimmedText().Should().Be(title);
        }
    }
}