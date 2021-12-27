using Bunit;
using FakeItEasy;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor;
using WebUI.Client.Pages.Departments;
using WebUI.Client.Services;
using WebUI.Client.Test.Extensions;
using WebUI.Shared.Departments.Commands.CreateDepartment;
using Xunit;

namespace WebUI.Client.Test.Pages.Departments
{
    public class DepartmentCreateTests : BunitTestBase
    {
        [Fact]
        public async Task DepartmentCreate_DisplayDialogCorrectly()
        {
            var fakeDepartmentService = A.Fake<IDepartmentService>();
            Context.Services.AddScoped(x => fakeDepartmentService);

            var fakeInstructorService = A.Fake<IInstructorService>();
            Context.Services.AddScoped(x => fakeInstructorService);

            var comp = Context.RenderComponent<MudDialogProvider>();
            Assert.Empty(comp.Markup.Trim());

            var service = Context.Services.GetService<IDialogService>() as DialogService;
            Assert.NotNull(service);
            IDialogReference? dialogReference = null;

            var title = "Create Department";
            await comp.InvokeAsync(() => dialogReference = service?.Show<DepartmentCreate>(title));
            Assert.NotNull(dialogReference);

            Assert.NotEmpty(comp.Markup.Trim());

            comp.Find("h6").TrimmedText().Should().Be(title);

            comp.FindAll("input")[0].Id.Should().Be("Name");
            comp.FindAll("input")[1].Id.Should().Be("Budget");
            comp.FindAll("input")[2].Id.Should().Be("StartDate");
        }

        [Fact]
        public async Task DepartmentCreate_WhenCancelButtonClicked_PopupCloses()
        {
            var fakeDepartmentService = A.Fake<IDepartmentService>();
            Context.Services.AddScoped(x => fakeDepartmentService);

            var fakeInstructorService = A.Fake<IInstructorService>();
            Context.Services.AddScoped(x => fakeInstructorService);

            var comp = Context.RenderComponent<MudDialogProvider>();
            Assert.Empty(comp.Markup.Trim());

            var service = Context.Services.GetService<IDialogService>() as DialogService;
            Assert.NotNull(service);
            IDialogReference? dialogReference = null;

            var title = "Create Department";
            await comp.InvokeAsync(() => dialogReference = service?.Show<DepartmentCreate>(title));
            Assert.NotNull(dialogReference);

            Assert.NotEmpty(comp.Markup.Trim());

            comp.Find("button[type='button']").Click();
            comp.Markup.Trim().Should().BeEmpty();
        }

        [Fact]
        public async Task DepartmentCreate_WhenCreateButtonClicked_PopupCloses()
        {
            var fakeDepartmentService = A.Fake<IDepartmentService>();
            Context.Services.AddScoped(x => fakeDepartmentService);

            var fakeInstructorService = A.Fake<IInstructorService>();
            Context.Services.AddScoped(x => fakeInstructorService);

            var comp = Context.RenderComponent<MudDialogProvider>();
            Assert.Empty(comp.Markup.Trim());

            var service = Context.Services.GetService<IDialogService>() as DialogService;
            Assert.NotNull(service);
            IDialogReference? dialogReference = null;

            var title = "Create Department";
            await comp.InvokeAsync(() => dialogReference = service?.Show<DepartmentCreate>(title));
            Assert.NotNull(dialogReference);

            Assert.NotEmpty(comp.Markup.Trim());

            var dialog = dialogReference?.Dialog as DepartmentCreate;

            dialog.Should().NotBeNull();
            dialog.CreateDepartmentCommand.Name = "My name";
            dialog.CreateDepartmentCommand.Budget = 123;
            dialog.CreateDepartmentCommand.StartDate = new DateTime(2021, 3, 1);
            dialog.CreateDepartmentCommand.InstructorID = 1;

            comp.Find("button[type='submit']").Click();
            comp.Markup.Trim().Should().BeEmpty();
        }

        [Fact]
        public async Task DepartmentCreate_WhenCreateButtonClicked_DepartmentServiceMustBeCalled()
        {
            var fakeDepartmentService = A.Fake<IDepartmentService>();
            Context.Services.AddScoped(x => fakeDepartmentService);

            var fakeInstructorService = A.Fake<IInstructorService>();
            Context.Services.AddScoped(x => fakeInstructorService);

            var comp = Context.RenderComponent<MudDialogProvider>();
            Assert.Empty(comp.Markup.Trim());

            var service = Context.Services.GetService<IDialogService>() as DialogService;
            Assert.NotNull(service);
            IDialogReference? dialogReference = null;

            var title = "Create Department";
            await comp.InvokeAsync(() => dialogReference = service?.Show<DepartmentCreate>(title));
            Assert.NotNull(dialogReference);

            Assert.NotEmpty(comp.Markup.Trim());

            var dialog = dialogReference?.Dialog as DepartmentCreate;

            dialog.Should().NotBeNull();
            dialog.CreateDepartmentCommand.Name = "My name";
            dialog.CreateDepartmentCommand.Budget = 123;
            dialog.CreateDepartmentCommand.StartDate = new DateTime(2021, 3, 1);
            dialog.CreateDepartmentCommand.InstructorID = 1;

            comp.Find("button[type='submit']").Click();

            A.CallTo(() => fakeDepartmentService.CreateAsync(A<CreateDepartmentCommand>.That.IsInstanceOf(typeof(CreateDepartmentCommand)))).MustHaveHappened();
        }

        [Fact]
        public async Task DepartmentCreate_WhenExceptionCaughtAfterSave_ShowErrorMessage()
        {
            var fakeDepartmentService = A.Fake<IDepartmentService>();
            A.CallTo(() => fakeDepartmentService.CreateAsync(A<CreateDepartmentCommand>.Ignored)).ThrowsAsync(new Exception("error"));
            Context.Services.AddScoped(x => fakeDepartmentService);

            var fakeInstructorService = A.Fake<IInstructorService>();
            Context.Services.AddScoped(x => fakeInstructorService);

            var comp = Context.RenderComponent<MudDialogProvider>();
            Assert.Empty(comp.Markup.Trim());

            var service = Context.Services.GetService<IDialogService>() as DialogService;
            Assert.NotNull(service);
            IDialogReference? dialogReference = null;

            var title = "Create Department";
            await comp.InvokeAsync(() => dialogReference = service?.Show<DepartmentCreate>(title));
            Assert.NotNull(dialogReference);

            Assert.NotEmpty(comp.Markup.Trim());

            var dialog = dialogReference?.Dialog as DepartmentCreate;

            dialog.Should().NotBeNull();
            dialog.CreateDepartmentCommand.Name = "My name";
            dialog.CreateDepartmentCommand.Budget = 123;
            dialog.CreateDepartmentCommand.StartDate = new DateTime(2021, 3, 1);
            dialog.CreateDepartmentCommand.InstructorID = 1;

            comp.Find("button[type='submit']").Click();

            dialog.ErrorVisible.Should().Be(true);
            comp.Find("div.mud-alert-message").TrimmedText().Should().Be("An error occured during saving");
        }

        //TODO: add unit test for failed validation
    }
}
