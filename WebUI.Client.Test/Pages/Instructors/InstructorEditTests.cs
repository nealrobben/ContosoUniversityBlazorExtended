using AngleSharp.Html.Dom;
using Bunit;
using FakeItEasy;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor;
using WebUI.Client.Pages.Instructors;
using WebUI.Client.Services;
using WebUI.Client.Test.Extensions;
using WebUI.Shared.Instructors.Commands.UpdateInstructor;
using WebUI.Shared.Instructors.Queries.GetInstructorDetails;
using Xunit;

namespace WebUI.Client.Test.Pages.Instructors;

public class InstructorEditTests : BunitTestBase
{
    [Fact]
    public async Task InstructorEdit_DisplayDialogCorrectly()
    {
        var instructorDetailsVM = new InstructorDetailsVM
        {
            InstructorID = 1,
            LastName = "Lastname",
            FirstName = "Firstname",
            HireDate = new DateTime(2021, 3, 1),
            OfficeLocation = "test"
        };

        var fakeInstructorService = A.Fake<IInstructorService>();
        A.CallTo(() => fakeInstructorService.GetAsync(A<string>.Ignored)).Returns(instructorDetailsVM);
        Context.Services.AddScoped(x => fakeInstructorService);

        var uploadService = A.Fake<IFileuploadService>();
        Context.Services.AddScoped(x => uploadService);

        var comp = Context.RenderComponent<MudDialogProvider>();
        Assert.Empty(comp.Markup.Trim());

        var service = Context.Services.GetService<IDialogService>() as DialogService;
        Assert.NotNull(service);
        IDialogReference? dialogReference = null;

        var parameters = new DialogParameters();
        parameters.Add("InstructorId", 1);

        var title = "Edit Instructor";
        await comp.InvokeAsync(() => dialogReference = service?.Show<InstructorEdit>(title, parameters));
        Assert.NotNull(dialogReference);

        Assert.NotEmpty(comp.Markup.Trim());

        comp.Find("h6").TrimmedText().Should().Be(title);

        ((IHtmlInputElement)comp.FindAll("input")[0]).Value.Should().Be(instructorDetailsVM.LastName);
        ((IHtmlInputElement)comp.FindAll("input")[1]).Value.Should().Be(instructorDetailsVM.FirstName);
        ((IHtmlInputElement)comp.FindAll("input")[2]).Value.Should().Be(instructorDetailsVM.HireDate.ToString("yyyy-MM-dd"));
        ((IHtmlInputElement)comp.FindAll("input")[3]).Value.Should().Be(instructorDetailsVM.OfficeLocation);
    }

    [Fact]
    public async Task InstructorEdit_WhenCancelButtonClicked_PopupCloses()
    {
        var instructorDetailsVM = new InstructorDetailsVM
        {
            InstructorID = 1,
            LastName = "Lastname",
            FirstName = "Firstname",
            HireDate = new DateTime(2021, 3, 1),
            OfficeLocation = "test"
        };

        var fakeInstructorService = A.Fake<IInstructorService>();
        A.CallTo(() => fakeInstructorService.GetAsync(A<string>.Ignored)).Returns(instructorDetailsVM);
        Context.Services.AddScoped(x => fakeInstructorService);

        var uploadService = A.Fake<IFileuploadService>();
        Context.Services.AddScoped(x => uploadService);

        var comp = Context.RenderComponent<MudDialogProvider>();
        Assert.Empty(comp.Markup.Trim());

        var service = Context.Services.GetService<IDialogService>() as DialogService;
        Assert.NotNull(service);
        IDialogReference? dialogReference = null;

        var parameters = new DialogParameters();
        parameters.Add("InstructorId", 1);

        var title = "Edit Instructor";
        await comp.InvokeAsync(() => dialogReference = service?.Show<InstructorEdit>(title, parameters));
        Assert.NotNull(dialogReference);

        Assert.NotEmpty(comp.Markup.Trim());

        comp.Find("button[type='button']").Click();
        comp.Markup.Trim().Should().BeEmpty();
    }

    [Fact]
    public async Task InstructorEdit_WhenEditButtonClicked_PopupCloses()
    {
        var instructorDetailsVM = new InstructorDetailsVM
        {
            InstructorID = 1,
            LastName = "Lastname",
            FirstName = "Firstname",
            HireDate = new DateTime(2021, 3, 1),
            OfficeLocation = "test"
        };

        var fakeInstructorService = A.Fake<IInstructorService>();
        A.CallTo(() => fakeInstructorService.GetAsync(A<string>.Ignored)).Returns(instructorDetailsVM);
        Context.Services.AddScoped(x => fakeInstructorService);

        var uploadService = A.Fake<IFileuploadService>();
        Context.Services.AddScoped(x => uploadService);

        var comp = Context.RenderComponent<MudDialogProvider>();
        Assert.Empty(comp.Markup.Trim());

        var service = Context.Services.GetService<IDialogService>() as DialogService;
        Assert.NotNull(service);
        IDialogReference? dialogReference = null;

        var parameters = new DialogParameters();
        parameters.Add("InstructorId", 1);

        var title = "Edit Instructor";
        await comp.InvokeAsync(() => dialogReference = service?.Show<InstructorEdit>(title, parameters));
        Assert.NotNull(dialogReference);

        Assert.NotEmpty(comp.Markup.Trim());

        comp.Find("#LastName").Change("new lastname");
        comp.Find("#FirstName").Change("new firstname");
        comp.Find("#HireDate").Change("1/3/2021");
        comp.Find("#OfficeLocation").Change("1");

        comp.Find("button[type='submit']").Click();
        comp.Markup.Trim().Should().BeEmpty();
    }

    [Fact]
    public async Task InstructorEdit_WhenEditButtonClicked_InstructorServiceMustBeCalled()
    {
        var instructorDetailsVM = new InstructorDetailsVM
        {
            InstructorID = 1,
            LastName = "Lastname",
            FirstName = "Firstname",
            HireDate = new DateTime(2021, 3, 1),
            OfficeLocation = "test"
        };

        var fakeInstructorService = A.Fake<IInstructorService>();
        A.CallTo(() => fakeInstructorService.GetAsync(A<string>.Ignored)).Returns(instructorDetailsVM);
        Context.Services.AddScoped(x => fakeInstructorService);

        var uploadService = A.Fake<IFileuploadService>();
        Context.Services.AddScoped(x => uploadService);

        var comp = Context.RenderComponent<MudDialogProvider>();
        Assert.Empty(comp.Markup.Trim());

        var service = Context.Services.GetService<IDialogService>() as DialogService;
        Assert.NotNull(service);
        IDialogReference? dialogReference = null;

        var parameters = new DialogParameters();
        parameters.Add("InstructorId", 1);

        var title = "Edit Instructor";
        await comp.InvokeAsync(() => dialogReference = service?.Show<InstructorEdit>(title, parameters));
        Assert.NotNull(dialogReference);

        Assert.NotEmpty(comp.Markup.Trim());

        comp.Find("#LastName").Change("new lastname");
        comp.Find("#FirstName").Change("new firstname");
        comp.Find("#HireDate").Change("1/3/2021");
        comp.Find("#OfficeLocation").Change("1");

        comp.Find("button[type='submit']").Click();
        comp.Markup.Trim().Should().BeEmpty();

        A.CallTo(() => fakeInstructorService.UpdateAsync(A<UpdateInstructorCommand>.That.IsInstanceOf(typeof(UpdateInstructorCommand)))).MustHaveHappened();
    }

    [Fact]
    public async Task InstructorEdit_WhenExceptionCaughtAfterSave_ShowErrorMessage()
    {
        var instructorDetailsVM = new InstructorDetailsVM
        {
            InstructorID = 1,
            LastName = "Lastname",
            FirstName = "Firstname",
            HireDate = new DateTime(2021, 3, 1),
            OfficeLocation = "test"
        };

        var fakeInstructorService = A.Fake<IInstructorService>();
        A.CallTo(() => fakeInstructorService.GetAsync(A<string>.Ignored)).Returns(instructorDetailsVM);
        A.CallTo(() => fakeInstructorService.UpdateAsync(A<UpdateInstructorCommand>.Ignored)).ThrowsAsync(new Exception("error"));
        Context.Services.AddScoped(x => fakeInstructorService);

        var uploadService = A.Fake<IFileuploadService>();
        Context.Services.AddScoped(x => uploadService);

        var comp = Context.RenderComponent<MudDialogProvider>();
        Assert.Empty(comp.Markup.Trim());

        var service = Context.Services.GetService<IDialogService>() as DialogService;
        Assert.NotNull(service);
        IDialogReference? dialogReference = null;

        var parameters = new DialogParameters();
        parameters.Add("InstructorId", 1);

        var title = "Edit Instructor";
        await comp.InvokeAsync(() => dialogReference = service?.Show<InstructorEdit>(title, parameters));
        Assert.NotNull(dialogReference);

        Assert.NotEmpty(comp.Markup.Trim());

        var dialog = dialogReference?.Dialog as InstructorEdit;

        comp.Find("#LastName").Change("new lastname");
        comp.Find("#FirstName").Change("new firstname");
        comp.Find("#HireDate").Change("1/3/2021");
        comp.Find("#OfficeLocation").Change("1");

        comp.Find("button[type='submit']").Click();

        dialog?.ErrorVisible.Should().Be(true);
        comp.Find("div.mud-alert-message").TrimmedText().Should().Be("An error occured during saving");
    }

    [Fact]
    public async Task InstructorEdit_WhenValidationFails_ShowErrorMessagesForFields()
    {
        var instructorDetailsVM = new InstructorDetailsVM
        {
            InstructorID = 1,
            LastName = "Lastname",
            FirstName = "Firstname",
            HireDate = new DateTime(2021, 3, 1),
            OfficeLocation = "test"
        };

        var fakeInstructorService = A.Fake<IInstructorService>();
        A.CallTo(() => fakeInstructorService.GetAsync(A<string>.Ignored)).Returns(instructorDetailsVM);
        A.CallTo(() => fakeInstructorService.UpdateAsync(A<UpdateInstructorCommand>.Ignored)).ThrowsAsync(new Exception("error"));
        Context.Services.AddScoped(x => fakeInstructorService);

        var uploadService = A.Fake<IFileuploadService>();
        Context.Services.AddScoped(x => uploadService);

        var comp = Context.RenderComponent<MudDialogProvider>();
        Assert.Empty(comp.Markup.Trim());

        var service = Context.Services.GetService<IDialogService>() as DialogService;
        Assert.NotNull(service);
        IDialogReference? dialogReference = null;

        var parameters = new DialogParameters();
        parameters.Add("InstructorId", 1);

        var title = "Edit Instructor";
        await comp.InvokeAsync(() => dialogReference = service?.Show<InstructorEdit>(title, parameters));
        Assert.NotNull(dialogReference);

        Assert.NotEmpty(comp.Markup.Trim());

        var dialog = dialogReference?.Dialog as InstructorEdit;

        comp.Find("#LastName").Change("");
        comp.Find("#FirstName").Change("");
        comp.Find("#HireDate").Change("");

        comp.Find("button[type='submit']").Click();

        comp.FindAll("div.validation-message")[0].TrimmedText().Should().Be("'Last Name' must not be empty.");
        comp.FindAll("div.validation-message")[1].TrimmedText().Should().Be("'First Name' must not be empty.");
        comp.FindAll("div.validation-message")[2].TrimmedText().Should().Be("The HireDate field must be a date.");
    }
}
