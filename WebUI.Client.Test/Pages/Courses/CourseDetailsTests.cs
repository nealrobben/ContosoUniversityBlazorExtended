using Bunit;
using FakeItEasy;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor;
using WebUI.Client.Pages.Courses;
using WebUI.Client.Services;
using WebUI.Client.Test.Extensions;
using WebUI.Shared.Courses.Queries.GetCourseDetails;
using Xunit;

namespace WebUI.Client.Test.Pages.Courses;

public class CourseDetailsTests : BunitTestBase
{
    [Fact]
    public async Task CourseDetails_DisplayDetailsCorrectly()
    {
        var courseDetailsVM = new CourseDetailVM
        {
            CourseID = 1,
            Title = "My course",
            Credits = 2,
            DepartmentID = 3
        };

        var fakeCourseService = A.Fake<ICourseService>();
        A.CallTo(() => fakeCourseService.GetAsync(A<string>.Ignored)).Returns(courseDetailsVM);
        Context.Services.AddScoped(x => fakeCourseService);

        var comp = Context.RenderComponent<MudDialogProvider>();
        Assert.Empty(comp.Markup.Trim());

        var service = Context.Services.GetService<IDialogService>() as DialogService;
        Assert.NotNull(service);
        IDialogReference? dialogReference = null;

        var parameters = new DialogParameters();
        parameters.Add("CourseId", 1);

        var title = "Course Details";
        await comp.InvokeAsync(() => dialogReference = service?.Show<CourseDetails>(title, parameters));
        Assert.NotNull(dialogReference);

        Assert.NotEmpty(comp.Markup.Trim());

        comp.Find("h6").TrimmedText().Should().Be(title);

        comp.FindAll("dt")[0].TrimmedText().Should().Be("Title");
        comp.FindAll("dt")[1].TrimmedText().Should().Be("Credits");
        comp.FindAll("dt")[2].TrimmedText().Should().Be("Department");

        comp.FindAll("dd")[0].TrimmedText().Should().Be(courseDetailsVM.Title);
        comp.FindAll("dd")[1].TrimmedText().Should().Be(courseDetailsVM.Credits.ToString());
        comp.FindAll("dd")[2].TrimmedText().Should().Be(courseDetailsVM.DepartmentID.ToString());
    }

    [Fact]
    public async Task CourseDetails_WhenOkButtonClicked_PopupCloses()
    {
        var courseDetailsVM = new CourseDetailVM
        {
            CourseID = 1,
            Title = "My course",
            Credits = 2,
            DepartmentID = 3
        };

        var fakeCourseService = A.Fake<ICourseService>();
        A.CallTo(() => fakeCourseService.GetAsync(A<string>.Ignored)).Returns(courseDetailsVM);
        Context.Services.AddScoped(x => fakeCourseService);

        var comp = Context.RenderComponent<MudDialogProvider>();
        Assert.Empty(comp.Markup.Trim());

        var service = Context.Services.GetService<IDialogService>() as DialogService;
        Assert.NotNull(service);
        IDialogReference? dialogReference = null;

        var parameters = new DialogParameters();
        parameters.Add("CourseId", 1);

        var title = "Course Details";
        await comp.InvokeAsync(() => dialogReference = service?.Show<CourseDetails>(title, parameters));
        Assert.NotNull(dialogReference);

        Assert.NotEmpty(comp.Markup.Trim());

        comp.Find("button").Click();
        comp.Markup.Trim().Should().BeEmpty();
    }
}
