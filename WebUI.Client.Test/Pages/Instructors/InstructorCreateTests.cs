using Bunit;
using FakeItEasy;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor;
using WebUI.Client.Pages.Instructors;
using WebUI.Client.Services;
using WebUI.Client.Test.Extensions;
using WebUI.Shared.Instructors.Commands.CreateInstructor;
using Xunit;

namespace WebUI.Client.Test.Pages.Instructors;

public class InstructorCreateTests : BunitTestBase
{
    [Fact]
    public async Task InstructorCreate_DisplayDialogCorrectly()
    {
        var fakeInstructorService = A.Fake<IInstructorService>();
        Context.Services.AddScoped(x => fakeInstructorService);

        var uploadService = A.Fake<IFileuploadService>();
        Context.Services.AddScoped(x => uploadService);

        var comp = Context.RenderComponent<MudDialogProvider>();
        Assert.Empty(comp.Markup.Trim());

        var service = Context.Services.GetService<IDialogService>() as DialogService;
        Assert.NotNull(service);
        IDialogReference? dialogReference = null;

        var title = "Create Instructor";
        await comp.InvokeAsync(() => dialogReference = service?.Show<InstructorCreate>(title));
        Assert.NotNull(dialogReference);

        Assert.NotEmpty(comp.Markup.Trim());

        comp.Find("h6").TrimmedText().Should().Be(title);

        comp.FindAll("input")[0].Id.Should().Be("LastName");
        comp.FindAll("input")[1].Id.Should().Be("FirstName");
        comp.FindAll("input")[2].Id.Should().Be("HireDate");
    }

    [Fact]
    public async Task InstructorCreate_WhenCancelButtonClicked_PopupCloses()
    {
        var fakeInstructorService = A.Fake<IInstructorService>();
        Context.Services.AddScoped(x => fakeInstructorService);

        var uploadService = A.Fake<IFileuploadService>();
        Context.Services.AddScoped(x => uploadService);

        var comp = Context.RenderComponent<MudDialogProvider>();
        Assert.Empty(comp.Markup.Trim());

        var service = Context.Services.GetService<IDialogService>() as DialogService;
        Assert.NotNull(service);
        IDialogReference? dialogReference = null;

        var title = "Create Instructor";
        await comp.InvokeAsync(() => dialogReference = service?.Show<InstructorCreate>(title));
        Assert.NotNull(dialogReference);

        Assert.NotEmpty(comp.Markup.Trim());

        comp.Find("button[type='button']").Click();
        comp.Markup.Trim().Should().BeEmpty();
    }

    [Fact]
    public async Task InstructorCreate_WhenCreateButtonClicked_PopupCloses()
    {
        var fakeInstructorService = A.Fake<IInstructorService>();
        Context.Services.AddScoped(x => fakeInstructorService);

        var fakeUploadService = A.Fake<IFileuploadService>();
        Context.Services.AddScoped(x => fakeUploadService);

        var comp = Context.RenderComponent<MudDialogProvider>();
        Assert.Empty(comp.Markup.Trim());

        var service = Context.Services.GetService<IDialogService>() as DialogService;
        Assert.NotNull(service);
        IDialogReference? dialogReference = null;

        var title = "Create Instructor";
        await comp.InvokeAsync(() => dialogReference = service?.Show<InstructorCreate>(title));
        Assert.NotNull(dialogReference);

        Assert.NotEmpty(comp.Markup.Trim());

        comp.Find("#LastName").Change("lastname");
        comp.Find("#FirstName").Change("Firstname");
        comp.Find("#HireDate").Change("01/03/2021");

        comp.Find("button[type='submit']").Click();
        comp.Markup.Trim().Should().BeEmpty();
    }

    [Fact]
    public async Task InstructorCreate_WhenCreateButtonClicked_InstructorServiceMustBeCalled()
    {
        var fakeInstructorService = A.Fake<IInstructorService>();
        Context.Services.AddScoped(x => fakeInstructorService);

        var fakeUploadService = A.Fake<IFileuploadService>();
        Context.Services.AddScoped(x => fakeUploadService);

        var comp = Context.RenderComponent<MudDialogProvider>();
        Assert.Empty(comp.Markup.Trim());

        var service = Context.Services.GetService<IDialogService>() as DialogService;
        Assert.NotNull(service);
        IDialogReference? dialogReference = null;

        var title = "Create Instructor";
        await comp.InvokeAsync(() => dialogReference = service?.Show<InstructorCreate>(title));
        Assert.NotNull(dialogReference);

        Assert.NotEmpty(comp.Markup.Trim());

        comp.Find("#LastName").Change("lastname");
        comp.Find("#FirstName").Change("Firstname");
        comp.Find("#HireDate").Change("01/03/2021");

        comp.Find("button[type='submit']").Click();

        A.CallTo(() => fakeInstructorService.CreateAsync(A<CreateInstructorCommand>.That.IsInstanceOf(typeof(CreateInstructorCommand)))).MustHaveHappened();
    }

    [Fact]
    public async Task InstructorCreate_WhenExceptionCaughtAfterSave_ShowErrorMessage()
    {
        var fakeInstructorService = A.Fake<IInstructorService>();
        A.CallTo(() => fakeInstructorService.CreateAsync(A<CreateInstructorCommand>.Ignored)).ThrowsAsync(new Exception("error"));
        Context.Services.AddScoped(x => fakeInstructorService);

        var fakeUploadService = A.Fake<IFileuploadService>();
        Context.Services.AddScoped(x => fakeUploadService);

        var comp = Context.RenderComponent<MudDialogProvider>();
        Assert.Empty(comp.Markup.Trim());

        var service = Context.Services.GetService<IDialogService>() as DialogService;
        Assert.NotNull(service);
        IDialogReference? dialogReference = null;

        var title = "Create Instructor";
        await comp.InvokeAsync(() => dialogReference = service?.Show<InstructorCreate>(title));
        Assert.NotNull(dialogReference);

        Assert.NotEmpty(comp.Markup.Trim());

        var dialog = dialogReference?.Dialog as InstructorCreate;

        comp.Find("#LastName").Change("lastname");
        comp.Find("#FirstName").Change("Firstname");
        comp.Find("#HireDate").Change("01/03/2021");

        comp.Find("button[type='submit']").Click();

        dialog?.ErrorVisible.Should().Be(true);
        comp.Find("div.mud-alert-message").TrimmedText().Should().Be("An error occured during saving");
    }

    [Fact]
    public async Task InstructorCreate_WhenValidationFails_ShowErrorMessagesForFields()
    {
        var fakeInstructorService = A.Fake<IInstructorService>();
        Context.Services.AddScoped(x => fakeInstructorService);

        var fakeUploadService = A.Fake<IFileuploadService>();
        Context.Services.AddScoped(x => fakeUploadService);

        var comp = Context.RenderComponent<MudDialogProvider>();
        Assert.Empty(comp.Markup.Trim());

        var service = Context.Services.GetService<IDialogService>() as DialogService;
        Assert.NotNull(service);
        IDialogReference? dialogReference = null;

        var title = "Create Instructor";
        await comp.InvokeAsync(() => dialogReference = service?.Show<InstructorCreate>(title));
        Assert.NotNull(dialogReference);

        Assert.NotEmpty(comp.Markup.Trim());

        comp.Find("#HireDate").Change("");

        comp.Find("button[type='submit']").Click();

        comp.FindAll("div.validation-message")[0].TrimmedText().Should().Be("'Last Name' must not be empty.");
        comp.FindAll("div.validation-message")[1].TrimmedText().Should().Be("'First Name' must not be empty.");
        comp.FindAll("div.validation-message")[2].TrimmedText().Should().Be("The HireDate field must be a date.");
    }
}
