using Bunit;
using FakeItEasy;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor;
using WebUI.Client.Pages.Students;
using WebUI.Client.Services;
using WebUI.Client.Test.Extensions;
using WebUI.Shared.Students.Commands.CreateStudent;
using Xunit;

namespace WebUI.Client.Test.Pages.Students;

public class StudentCreateTests : BunitTestBase
{
    [Fact]
    public async Task StudentCreate_DisplayDialogCorrectly()
    {
        var fakeStudentService = A.Fake<IStudentService>();
        Context.Services.AddScoped(x => fakeStudentService);

        var fakeUploadService = A.Fake<IFileuploadService>();
        Context.Services.AddScoped(x => fakeUploadService);

        var comp = Context.RenderComponent<MudDialogProvider>();
        Assert.Empty(comp.Markup.Trim());

        var service = Context.Services.GetService<IDialogService>() as DialogService;
        Assert.NotNull(service);
        IDialogReference? dialogReference = null;

        var title = "Create Student";
        await comp.InvokeAsync(() => dialogReference = service?.Show<StudentCreate>(title));
        Assert.NotNull(dialogReference);

        Assert.NotEmpty(comp.Markup.Trim());

        comp.Find("h6").TrimmedText().Should().Be(title);

        comp.FindAll("input")[0].Id.Should().Be("LastName");
        comp.FindAll("input")[1].Id.Should().Be("FirstName");
        comp.FindAll("input")[2].Id.Should().Be("EnrollmentDate");
    }

    [Fact]
    public async Task StudentCreate_WhenCancelButtonClicked_PopupCloses()
    {
        var fakeStudentService = A.Fake<IStudentService>();
        Context.Services.AddScoped(x => fakeStudentService);

        var fakeUploadService = A.Fake<IFileuploadService>();
        Context.Services.AddScoped(x => fakeUploadService);

        var comp = Context.RenderComponent<MudDialogProvider>();
        Assert.Empty(comp.Markup.Trim());

        var service = Context.Services.GetService<IDialogService>() as DialogService;
        Assert.NotNull(service);
        IDialogReference? dialogReference = null;

        var title = "Create Student";
        await comp.InvokeAsync(() => dialogReference = service?.Show<StudentCreate>(title));
        Assert.NotNull(dialogReference);

        Assert.NotEmpty(comp.Markup.Trim());

        comp.Find("button[type='button']").Click();
        comp.Markup.Trim().Should().BeEmpty();
    }

    [Fact]
    public async Task StudentCreate_WhenCreateButtonClicked_PopupCloses()
    {
        var fakeStudentService = A.Fake<IStudentService>();
        Context.Services.AddScoped(x => fakeStudentService);

        var fakeUploadService = A.Fake<IFileuploadService>();
        Context.Services.AddScoped(x => fakeUploadService);

        var comp = Context.RenderComponent<MudDialogProvider>();
        Assert.Empty(comp.Markup.Trim());

        var service = Context.Services.GetService<IDialogService>() as DialogService;
        Assert.NotNull(service);
        IDialogReference? dialogReference = null;

        var title = "Create Student";
        await comp.InvokeAsync(() => dialogReference = service?.Show<StudentCreate>(title));
        Assert.NotNull(dialogReference);

        Assert.NotEmpty(comp.Markup.Trim());

        comp.Find("#LastName").Change("lastname");
        comp.Find("#FirstName").Change("Firstname");
        comp.Find("#EnrollmentDate").Change("01/03/2021");

        comp.Find("button[type='submit']").Click();
        comp.Markup.Trim().Should().BeEmpty();
    }

    [Fact]
    public async Task StudentCreate_WhenCreateButtonClicked_StudentServiceMustBeCalled()
    {
        var fakeStudentService = A.Fake<IStudentService>();
        Context.Services.AddScoped(x => fakeStudentService);

        var fakeUploadService = A.Fake<IFileuploadService>();
        Context.Services.AddScoped(x => fakeUploadService);

        var comp = Context.RenderComponent<MudDialogProvider>();
        Assert.Empty(comp.Markup.Trim());

        var service = Context.Services.GetService<IDialogService>() as DialogService;
        Assert.NotNull(service);
        IDialogReference? dialogReference = null;

        var title = "Create Student";
        await comp.InvokeAsync(() => dialogReference = service?.Show<StudentCreate>(title));
        Assert.NotNull(dialogReference);

        Assert.NotEmpty(comp.Markup.Trim());

        comp.Find("#LastName").Change("lastname");
        comp.Find("#FirstName").Change("Firstname");
        comp.Find("#EnrollmentDate").Change("01/03/2021");

        comp.Find("button[type='submit']").Click();

        A.CallTo(() => fakeStudentService.CreateAsync(A<CreateStudentCommand>.That.IsInstanceOf(typeof(CreateStudentCommand)))).MustHaveHappened();
    }

    [Fact]
    public async Task StudentCreate_WhenExceptionCaughtAfterSave_ShowErrorMessage()
    {
        var fakeStudentService = A.Fake<IStudentService>();
        A.CallTo(() => fakeStudentService.CreateAsync(A<CreateStudentCommand>.Ignored)).ThrowsAsync(new Exception("error"));
        Context.Services.AddScoped(x => fakeStudentService);

        var fakeUploadService = A.Fake<IFileuploadService>();
        Context.Services.AddScoped(x => fakeUploadService);

        var comp = Context.RenderComponent<MudDialogProvider>();
        Assert.Empty(comp.Markup.Trim());

        var service = Context.Services.GetService<IDialogService>() as DialogService;
        Assert.NotNull(service);
        IDialogReference? dialogReference = null;

        var title = "Create Student";
        await comp.InvokeAsync(() => dialogReference = service?.Show<StudentCreate>(title));
        Assert.NotNull(dialogReference);

        Assert.NotEmpty(comp.Markup.Trim());

        var dialog = dialogReference?.Dialog as StudentCreate;

        comp.Find("#LastName").Change("lastname");
        comp.Find("#FirstName").Change("Firstname");
        comp.Find("#EnrollmentDate").Change("01/03/2021");

        comp.Find("button[type='submit']").Click();

        dialog?.ErrorVisible.Should().Be(true);
        comp.Find("div.mud-alert-message").TrimmedText().Should().Be("An error occured during saving");
    }

    [Fact]
    public async Task StudentCreate_WhenValidationFails_ShowErrorMessagesForFields()
    {
        var fakeStudentService = A.Fake<IStudentService>();
        Context.Services.AddScoped(x => fakeStudentService);

        var fakeUploadService = A.Fake<IFileuploadService>();
        Context.Services.AddScoped(x => fakeUploadService);

        var comp = Context.RenderComponent<MudDialogProvider>();
        Assert.Empty(comp.Markup.Trim());

        var service = Context.Services.GetService<IDialogService>() as DialogService;
        Assert.NotNull(service);
        IDialogReference? dialogReference = null;

        var title = "Create Student";
        await comp.InvokeAsync(() => dialogReference = service?.Show<StudentCreate>(title));
        Assert.NotNull(dialogReference);

        Assert.NotEmpty(comp.Markup.Trim());

        comp.Find("#EnrollmentDate").Change("");

        comp.Find("button[type='submit']").Click();

        comp.FindAll("div.validation-message")[0].TrimmedText().Should().Be("'Last Name' must not be empty.");
        comp.FindAll("div.validation-message")[1].TrimmedText().Should().Be("'First Name' must not be empty.");
        comp.FindAll("div.validation-message")[2].TrimmedText().Should().Be("The EnrollmentDate field must be a date.");
    }
}
