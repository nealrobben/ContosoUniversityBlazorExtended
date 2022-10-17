using Bunit;
using FakeItEasy;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor;
using WebUI.Client.Pages.Courses;
using WebUI.Client.Services;
using WebUI.Client.Test.Extensions;
using WebUI.Shared.Courses.Commands.CreateCourse;
using WebUI.Shared.Departments.Queries.GetDepartmentsLookup;
using Xunit;

namespace WebUI.Client.Test.Pages.Courses;

public class CourseCreateTests : BunitTestBase
{
    [Fact]
    public async Task CourseCreate_DisplayDialogCorrectly()
    {
        var fakeDepartmentService = A.Fake<IDepartmentService>();
        A.CallTo(() => fakeDepartmentService.GetLookupAsync()).Returns(GetDepartmentsLookupVMWithTestData());
        Context.Services.AddScoped(x => fakeDepartmentService);

        var fakeCourseService = A.Fake<ICourseService>();
        Context.Services.AddScoped(x => fakeCourseService);

        var comp = Context.RenderComponent<MudDialogProvider>();
        Assert.Empty(comp.Markup.Trim());

        var service = Context.Services.GetService<IDialogService>() as DialogService;
        Assert.NotNull(service);
        IDialogReference? dialogReference = null;

        var title = "Create Course";
        await comp.InvokeAsync(() => dialogReference = service?.Show<CourseCreate>(title));
        Assert.NotNull(dialogReference);

        Assert.NotEmpty(comp.Markup.Trim());

        comp.Find("h6").TrimmedText().Should().Be(title);

        comp.FindAll("input")[0].Id.Should().Be("CourseID");
        comp.FindAll("input")[1].Id.Should().Be("Title");
        comp.FindAll("input")[2].Id.Should().Be("Credits");
        comp.FindAll("input")[3].Id.Should().Be("Department");
    }

    [Fact]
    public async Task CourseCreate_WhenCancelButtonClicked_PopupCloses()
    {
        var fakeDepartmentService = A.Fake<IDepartmentService>();
        A.CallTo(() => fakeDepartmentService.GetLookupAsync()).Returns(GetDepartmentsLookupVMWithTestData());
        Context.Services.AddScoped(x => fakeDepartmentService);

        var fakeCourseService = A.Fake<ICourseService>();
        Context.Services.AddScoped(x => fakeCourseService);

        var comp = Context.RenderComponent<MudDialogProvider>();
        Assert.Empty(comp.Markup.Trim());

        var service = Context.Services.GetService<IDialogService>() as DialogService;
        Assert.NotNull(service);
        IDialogReference? dialogReference = null;

        var title = "Create Course";
        await comp.InvokeAsync(() => dialogReference = service?.Show<CourseCreate>(title));
        Assert.NotNull(dialogReference);

        Assert.NotEmpty(comp.Markup.Trim());

        comp.Find("button[type='button']").Click();
        comp.Markup.Trim().Should().BeEmpty();
    }

    [Fact]
    public async Task CourseCreate_WhenCreateButtonClicked_PopupCloses()
    {
        var fakeDepartmentService = A.Fake<IDepartmentService>();
        A.CallTo(() => fakeDepartmentService.GetLookupAsync()).Returns(GetDepartmentsLookupVMWithTestData());
        Context.Services.AddScoped(x => fakeDepartmentService);

        var fakeCourseService = A.Fake<ICourseService>();
        Context.Services.AddScoped(x => fakeCourseService);

        var comp = Context.RenderComponent<MudDialogProvider>();
        Assert.Empty(comp.Markup.Trim());

        var service = Context.Services.GetService<IDialogService>() as DialogService;
        Assert.NotNull(service);
        IDialogReference? dialogReference = null;

        var title = "Create Course";
        await comp.InvokeAsync(() => dialogReference = service?.Show<CourseCreate>(title));
        Assert.NotNull(dialogReference);

        Assert.NotEmpty(comp.Markup.Trim());

        comp.Find("#CourseID").Change("1");
        comp.Find("#Title").Change("My title");
        comp.Find("#Credits").Change("2");
        comp.Find("#Department").Change("3");

        comp.Find("button[type='submit']").Click();
        comp.Markup.Trim().Should().BeEmpty();
    }

    [Fact]
    public async Task CourseCreate_WhenCreateButtonClicked_CourseServiceMustBeCalled()
    {
        var fakeDepartmentService = A.Fake<IDepartmentService>();
        A.CallTo(() => fakeDepartmentService.GetLookupAsync()).Returns(GetDepartmentsLookupVMWithTestData());
        Context.Services.AddScoped(x => fakeDepartmentService);

        var fakeCourseService = A.Fake<ICourseService>();
        Context.Services.AddScoped(x => fakeCourseService);

        var comp = Context.RenderComponent<MudDialogProvider>();
        Assert.Empty(comp.Markup.Trim());

        var service = Context.Services.GetService<IDialogService>() as DialogService;
        Assert.NotNull(service);
        IDialogReference? dialogReference = null;

        var title = "Create Course";
        await comp.InvokeAsync(() => dialogReference = service?.Show<CourseCreate>(title));
        Assert.NotNull(dialogReference);

        Assert.NotEmpty(comp.Markup.Trim());

        comp.Find("#CourseID").Change("1");
        comp.Find("#Title").Change("My title");
        comp.Find("#Credits").Change("2");
        comp.Find("#Department").Change("3");

        comp.Find("button[type='submit']").Click();

        A.CallTo(() => fakeCourseService.CreateAsync(A<CreateCourseCommand>.That.IsInstanceOf(typeof(CreateCourseCommand)))).MustHaveHappened();
    }

    [Fact]
    public async Task CourseCreate_WhenExceptionCaughtAfterSave_ShowErrorMessage()
    {
        var fakeDepartmentService = A.Fake<IDepartmentService>();
        A.CallTo(() => fakeDepartmentService.GetLookupAsync()).Returns(GetDepartmentsLookupVMWithTestData());
        Context.Services.AddScoped(x => fakeDepartmentService);

        var fakeCourseService = A.Fake<ICourseService>();
        A.CallTo(() => fakeCourseService.CreateAsync(A<CreateCourseCommand>.Ignored)).ThrowsAsync(new Exception("error"));
        Context.Services.AddScoped(x => fakeCourseService);

        var comp = Context.RenderComponent<MudDialogProvider>();
        Assert.Empty(comp.Markup.Trim());

        var service = Context.Services.GetService<IDialogService>() as DialogService;
        Assert.NotNull(service);
        IDialogReference? dialogReference = null;

        var title = "Create Course";
        await comp.InvokeAsync(() => dialogReference = service?.Show<CourseCreate>(title));
        Assert.NotNull(dialogReference);

        Assert.NotEmpty(comp.Markup.Trim());

        var dialog = dialogReference?.Dialog as CourseCreate;

        comp.Find("#CourseID").Change("1");
        comp.Find("#Title").Change("My title");
        comp.Find("#Credits").Change("2");
        comp.Find("#Department").Change("3");

        comp.Find("button[type='submit']").Click();

        dialog?.ErrorVisible.Should().Be(true);
        comp.Find("div.mud-alert-message").TrimmedText().Should().Be("An error occured during saving");
    }

    [Fact]
    public async Task CourseCreate_WhenValidationFails_ShowErrorMessagesForFields()
    {
        var fakeDepartmentService = A.Fake<IDepartmentService>();
        A.CallTo(() => fakeDepartmentService.GetLookupAsync()).Returns(GetDepartmentsLookupVMWithTestData());
        Context.Services.AddScoped(x => fakeDepartmentService);

        var fakeCourseService = A.Fake<ICourseService>();
        Context.Services.AddScoped(x => fakeCourseService);

        var comp = Context.RenderComponent<MudDialogProvider>();
        Assert.Empty(comp.Markup.Trim());

        var service = Context.Services.GetService<IDialogService>() as DialogService;
        Assert.NotNull(service);
        IDialogReference? dialogReference = null;

        var title = "Create Course";
        await comp.InvokeAsync(() => dialogReference = service?.Show<CourseCreate>(title));
        Assert.NotNull(dialogReference);

        Assert.NotEmpty(comp.Markup.Trim());

        comp.Find("button[type='submit']").Click();

        comp.FindAll("div.validation-message")[0].TrimmedText().Should().Be("'Course ID' must not be empty.");
        comp.FindAll("div.validation-message")[1].TrimmedText().Should().Be("'Title' must not be empty.");
        comp.FindAll("div.validation-message")[2].TrimmedText().Should().Be("'Credits' must not be empty.");
        comp.FindAll("div.validation-message")[3].TrimmedText().Should().Be("'Credits' must be greater than '0'.");
    }

    private DepartmentsLookupVM GetDepartmentsLookupVMWithTestData()
    {
        return new DepartmentsLookupVM(new List<DepartmentLookupVM>
        {
            new DepartmentLookupVM
            {
                DepartmentID = 1,
                Name = "Department One"
            },
            new DepartmentLookupVM
            {
                DepartmentID = 2,
                Name = "Department Two"
            },
            new DepartmentLookupVM
            {
                DepartmentID = 3,
                Name = "Department Three"
            }
        });
    }
}
