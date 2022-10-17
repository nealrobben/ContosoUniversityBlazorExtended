using Bunit;
using FakeItEasy;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor;
using WebUI.Client.Pages.Students;
using WebUI.Client.Services;
using WebUI.Client.Test.Extensions;
using WebUI.Shared.Students.Queries.GetStudentDetails;
using Xunit;

namespace WebUI.Client.Test.Pages.Students;

public class StudentDetailsTests : BunitTestBase
{
    [Fact]
    public async Task StudentDetails_DisplayDetailsCorrectly()
    {
        var studentDetailsVM = new StudentDetailsVM
        {
            StudentID = 1,
            LastName = "Lastname",
            FirstName = "Firstname",
            EnrollmentDate = new DateTime(2021,3,1)
        };

        var enrollment = new StudentDetailsEnrollmentVM
        {
            CourseTitle = "My title",
            Grade = ContosoUniversityBlazor.Domain.Enums.Grade.A
        };
        studentDetailsVM.Enrollments.Add(enrollment);

        var fakeStudentService = A.Fake<IStudentService>();
        A.CallTo(() => fakeStudentService.GetAsync(A<string>.Ignored)).Returns(studentDetailsVM);
        Context.Services.AddScoped(x => fakeStudentService);

        var comp = Context.RenderComponent<MudDialogProvider>();
        Assert.Empty(comp.Markup.Trim());

        var service = Context.Services.GetService<IDialogService>() as DialogService;
        Assert.NotNull(service);
        IDialogReference? dialogReference = null;

        var parameters = new DialogParameters();
        parameters.Add("StudentId", 1);

        var title = "Student Details";
        await comp.InvokeAsync(() => dialogReference = service?.Show<StudentDetails>(title, parameters));
        Assert.NotNull(dialogReference);

        Assert.NotEmpty(comp.Markup.Trim());

        comp.Find("h6").TrimmedText().Should().Be(title);

        comp.FindAll("dt")[0].TrimmedText().Should().Be("Last name");
        comp.FindAll("dt")[1].TrimmedText().Should().Be("First name");
        comp.FindAll("dt")[2].TrimmedText().Should().Be("Enrollment date");

        comp.FindAll("dd")[0].TrimmedText().Should().Be(studentDetailsVM.LastName);
        comp.FindAll("dd")[1].TrimmedText().Should().Be(studentDetailsVM.FirstName);
        comp.FindAll("dd")[2].TrimmedText().Should().Be(studentDetailsVM.EnrollmentDate.ToShortDateString());

        var enrollmentsTable = comp.Find("table");
        var row = ((AngleSharp.Html.Dom.IHtmlTableSectionElement)enrollmentsTable.Children[0]).Rows[1];
        row.Cells[0].TrimmedText().Should().Be(enrollment.CourseTitle);
        row.Cells[1].TrimmedText().Should().Be(enrollment.Grade.ToString());
    }

    [Fact]
    public async Task StudentDetails_WhenOkButtonClicked_PopupCloses()
    {
        var studentDetailsVM = new StudentDetailsVM
        {
            StudentID = 1,
            LastName = "Lastname",
            FirstName = "Firstname",
            EnrollmentDate = new DateTime(2021, 3, 1)
        };

        var enrollment = new StudentDetailsEnrollmentVM
        {
            CourseTitle = "My title",
            Grade = ContosoUniversityBlazor.Domain.Enums.Grade.A
        };
        studentDetailsVM.Enrollments.Add(enrollment);

        var fakeStudentService = A.Fake<IStudentService>();
        A.CallTo(() => fakeStudentService.GetAsync(A<string>.Ignored)).Returns(studentDetailsVM);
        Context.Services.AddScoped(x => fakeStudentService);

        var comp = Context.RenderComponent<MudDialogProvider>();
        Assert.Empty(comp.Markup.Trim());

        var service = Context.Services.GetService<IDialogService>() as DialogService;
        Assert.NotNull(service);
        IDialogReference? dialogReference = null;

        var parameters = new DialogParameters();
        parameters.Add("StudentId", 1);

        var title = "Student Details";
        await comp.InvokeAsync(() => dialogReference = service?.Show<StudentDetails>(title, parameters));
        Assert.NotNull(dialogReference);

        Assert.NotEmpty(comp.Markup.Trim());

        comp.Find("button").Click();
        comp.Markup.Trim().Should().BeEmpty();
    }
}
