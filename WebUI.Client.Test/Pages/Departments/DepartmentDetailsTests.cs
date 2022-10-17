using Bunit;
using WebUI.Client.Pages.Departments;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using WebUI.Client.Services;
using FakeItEasy;
using MudBlazor;
using WebUI.Shared.Departments.Queries.GetDepartmentDetails;
using FluentAssertions;
using WebUI.Client.Test.Extensions;

namespace WebUI.Client.Test.Pages.Departments;

public class DepartmentDetailsTests : BunitTestBase
{
    [Fact]
    public async Task DepartmentDetails_DisplayDetailsCorrectly()
    {
        var departmentDetailVM = new DepartmentDetailVM
        {
            Name = "TestDepartment",
            Budget = 123,
            StartDate = new DateTime(2021, 3, 1),
            AdministratorName = "Admin"
        };
        
        var fakeDepartmentService = A.Fake<IDepartmentService>();
        A.CallTo(() => fakeDepartmentService.GetAsync(A<string>.Ignored)).Returns(departmentDetailVM);
        Context.Services.AddScoped(x => fakeDepartmentService);

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

        comp.FindAll("dt")[0].TrimmedText().Should().Be("Name");
        comp.FindAll("dt")[1].TrimmedText().Should().Be("Budget");
        comp.FindAll("dt")[2].TrimmedText().Should().Be("Start date");
        comp.FindAll("dt")[3].TrimmedText().Should().Be("Administrator");

        comp.FindAll("dd")[0].TrimmedText().Should().Be(departmentDetailVM.Name);
        comp.FindAll("dd")[1].TrimmedText().Should().Be(departmentDetailVM.Budget.ToString("F"));
        comp.FindAll("dd")[2].TrimmedText().Should().Be(departmentDetailVM.StartDate.ToShortDateString());
        comp.FindAll("dd")[3].TrimmedText().Should().Be(departmentDetailVM.AdministratorName);
    }

    [Fact]
    public async Task DepartmentDetails_WhenOkButtonClicked_PopupCloses()
    {
        var departmentDetailVM = new DepartmentDetailVM
        {
            Name = "TestDepartment",
            Budget = 123,
            StartDate = new DateTime(2021, 3, 1),
            AdministratorName = "Admin"
        };

        var fakeDepartmentService = A.Fake<IDepartmentService>();
        A.CallTo(() => fakeDepartmentService.GetAsync(A<string>.Ignored)).Returns(departmentDetailVM);
        Context.Services.AddScoped(x => fakeDepartmentService);

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

        comp.Find("button").Click();
        comp.Markup.Trim().Should().BeEmpty();
    }
}