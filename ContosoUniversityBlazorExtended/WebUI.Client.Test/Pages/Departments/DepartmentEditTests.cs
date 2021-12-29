using AngleSharp.Html.Dom;
using Bunit;
using FakeItEasy;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor;
using WebUI.Client.Pages.Departments;
using WebUI.Client.Services;
using WebUI.Client.Test.Extensions;
using WebUI.Shared.Departments.Commands.UpdateDepartment;
using WebUI.Shared.Departments.Queries.GetDepartmentDetails;
using Xunit;

namespace WebUI.Client.Test.Pages.Departments
{
    public class DepartmentEditTests : BunitTestBase
    {
        [Fact]
        public async Task DepartmentEdit_DisplayDialogCorrectly()
        {
            var departmentDetailVM = new DepartmentDetailVM
            {
                Name = "TestDepartment",
                Budget = 123,
                StartDate = new DateTime(2021, 3, 1),
                InstructorID = 1
            };

            var fakeDepartmentService = A.Fake<IDepartmentService>();
            A.CallTo(() => fakeDepartmentService.GetAsync(A<string>.Ignored)).Returns(departmentDetailVM);
            Context.Services.AddScoped(x => fakeDepartmentService);

            var fakeInstructorService = A.Fake<IInstructorService>();
            Context.Services.AddScoped(x => fakeInstructorService);

            var comp = Context.RenderComponent<MudDialogProvider>();
            Assert.Empty(comp.Markup.Trim());

            var service = Context.Services.GetService<IDialogService>() as DialogService;
            Assert.NotNull(service);
            IDialogReference? dialogReference = null;

            var parameters = new DialogParameters();
            parameters.Add("DepartmentId", 1);

            var title = "Edit Department";
            await comp.InvokeAsync(() => dialogReference = service?.Show<DepartmentEdit>(title, parameters));
            Assert.NotNull(dialogReference);

            Assert.NotEmpty(comp.Markup.Trim());

            comp.Find("h6").TrimmedText().Should().Be(title);

            ((IHtmlInputElement)comp.FindAll("input")[0]).Value.Should().Be(departmentDetailVM.Name);
            ((IHtmlInputElement)comp.FindAll("input")[1]).Value.Should().Be(departmentDetailVM.Budget.ToString());
            //((IHtmlInputElement)comp.FindAll("input")[2]).Value.Should().Be(departmentDetailVM.StartDate.ToShortDateString());
            //comp.FindAll("select")[0].Id.Should().Be("InstructorID");

            var select = ((IHtmlSelectElement)comp.FindAll("select")[0]);

            ((IHtmlSelectElement)comp.FindAll("select")[0]).Value.Should().Be(departmentDetailVM.InstructorID.ToString());
            throw new Exception("test");
        }

        [Fact]
        public async Task DepartmentEdit_WhenCancelButtonClicked_PopupCloses()
        {
            var departmentDetailVM = new DepartmentDetailVM
            {
                Name = "TestDepartment",
                Budget = 123,
                StartDate = new DateTime(2021, 3, 1),
                InstructorID = 1
            };

            var fakeDepartmentService = A.Fake<IDepartmentService>();
            A.CallTo(() => fakeDepartmentService.GetAsync(A<string>.Ignored)).Returns(departmentDetailVM);
            Context.Services.AddScoped(x => fakeDepartmentService);

            var fakeInstructorService = A.Fake<IInstructorService>();
            Context.Services.AddScoped(x => fakeInstructorService);

            var comp = Context.RenderComponent<MudDialogProvider>();
            Assert.Empty(comp.Markup.Trim());

            var service = Context.Services.GetService<IDialogService>() as DialogService;
            Assert.NotNull(service);
            IDialogReference? dialogReference = null;

            var parameters = new DialogParameters();
            parameters.Add("DepartmentId", 1);

            var title = "Edit Department";
            await comp.InvokeAsync(() => dialogReference = service?.Show<DepartmentEdit>(title, parameters));
            Assert.NotNull(dialogReference);

            Assert.NotEmpty(comp.Markup.Trim());

            comp.Find("button[type='button']").Click();
            comp.Markup.Trim().Should().BeEmpty();
        }

        [Fact]
        public async Task DepartmentEdit_WhenEditButtonClicked_PopupCloses()
        {
            var departmentDetailVM = new DepartmentDetailVM
            {
                Name = "TestDepartment",
                Budget = 123,
                StartDate = new DateTime(2021, 3, 1),
                InstructorID = 1
            };

            var fakeDepartmentService = A.Fake<IDepartmentService>();
            A.CallTo(() => fakeDepartmentService.GetAsync(A<string>.Ignored)).Returns(departmentDetailVM);
            Context.Services.AddScoped(x => fakeDepartmentService);

            var fakeInstructorService = A.Fake<IInstructorService>();
            Context.Services.AddScoped(x => fakeInstructorService);

            var comp = Context.RenderComponent<MudDialogProvider>();
            Assert.Empty(comp.Markup.Trim());

            var service = Context.Services.GetService<IDialogService>() as DialogService;
            Assert.NotNull(service);
            IDialogReference? dialogReference = null;

            var parameters = new DialogParameters();
            parameters.Add("DepartmentId", 1);

            var title = "Edit Department";
            await comp.InvokeAsync(() => dialogReference = service?.Show<DepartmentEdit>(title, parameters));
            Assert.NotNull(dialogReference);

            Assert.NotEmpty(comp.Markup.Trim());

            comp.Find("#Name").Change("My name");
            comp.Find("#Budget").Change("123");
            comp.Find("#StartDate").Change("1/3/2021");
            comp.Find("#InstructorID").Change("1");

            comp.Find("button[type='submit']").Click();
            comp.Markup.Trim().Should().BeEmpty();
        }

        [Fact]
        public async Task DepartmentEdit_WhenEditButtonClicked_DepartmentServiceMustBeCalled()
        {
            var departmentDetailVM = new DepartmentDetailVM
            {
                Name = "TestDepartment",
                Budget = 123,
                StartDate = new DateTime(2021, 3, 1),
                InstructorID = 1
            };

            var fakeDepartmentService = A.Fake<IDepartmentService>();
            A.CallTo(() => fakeDepartmentService.GetAsync(A<string>.Ignored)).Returns(departmentDetailVM);
            Context.Services.AddScoped(x => fakeDepartmentService);

            var fakeInstructorService = A.Fake<IInstructorService>();
            Context.Services.AddScoped(x => fakeInstructorService);

            var comp = Context.RenderComponent<MudDialogProvider>();
            Assert.Empty(comp.Markup.Trim());

            var service = Context.Services.GetService<IDialogService>() as DialogService;
            Assert.NotNull(service);
            IDialogReference? dialogReference = null;

            var parameters = new DialogParameters();
            parameters.Add("DepartmentId", 1);

            var title = "Edit Department";
            await comp.InvokeAsync(() => dialogReference = service?.Show<DepartmentEdit>(title, parameters));
            Assert.NotNull(dialogReference);

            Assert.NotEmpty(comp.Markup.Trim());

            comp.Find("#Name").Change("My name");
            comp.Find("#Budget").Change("123");
            comp.Find("#StartDate").Change("1/3/2021");
            comp.Find("#InstructorID").Change("1");

            comp.Find("button[type='submit']").Click();

            A.CallTo(() => fakeDepartmentService.UpdateAsync(A<UpdateDepartmentCommand>.That.IsInstanceOf(typeof(UpdateDepartmentCommand)))).MustHaveHappened();
        }

        [Fact]
        public async Task DepartmentEdit_WhenExceptionCaughtAfterSave_ShowErrorMessage()
        {
            var departmentDetailVM = new DepartmentDetailVM
            {
                Name = "TestDepartment",
                Budget = 123,
                StartDate = new DateTime(2021, 3, 1),
                InstructorID = 1
            };

            var fakeDepartmentService = A.Fake<IDepartmentService>();
            A.CallTo(() => fakeDepartmentService.GetAsync(A<string>.Ignored)).Returns(departmentDetailVM);
            A.CallTo(() => fakeDepartmentService.UpdateAsync(A<UpdateDepartmentCommand>.Ignored)).ThrowsAsync(new Exception("error"));
            Context.Services.AddScoped(x => fakeDepartmentService);

            var fakeInstructorService = A.Fake<IInstructorService>();
            Context.Services.AddScoped(x => fakeInstructorService);

            var comp = Context.RenderComponent<MudDialogProvider>();
            Assert.Empty(comp.Markup.Trim());

            var service = Context.Services.GetService<IDialogService>() as DialogService;
            Assert.NotNull(service);
            IDialogReference? dialogReference = null;

            var parameters = new DialogParameters();
            parameters.Add("DepartmentId", 1);

            var title = "Edit Department";
            await comp.InvokeAsync(() => dialogReference = service?.Show<DepartmentEdit>(title, parameters));
            Assert.NotNull(dialogReference);

            Assert.NotEmpty(comp.Markup.Trim());

            var dialog = dialogReference?.Dialog as DepartmentEdit;

            comp.Find("#Name").Change("My name");
            comp.Find("#Budget").Change("123");
            comp.Find("#StartDate").Change("1/3/2021");
            comp.Find("#InstructorID").Change("1");

            comp.Find("button[type='submit']").Click();

            dialog.ErrorVisible.Should().Be(true);
            comp.Find("div.mud-alert-message").TrimmedText().Should().Be("An error occured during saving");
        }

        [Fact]
        public async Task DepartmentEdit_WhenValidationFails_ShowErrorMessagesForFields()
        {
            var departmentDetailVM = new DepartmentDetailVM
            {
                Name = "TestDepartment",
                Budget = 123,
                StartDate = new DateTime(2021, 3, 1),
                InstructorID = 1
            };

            var fakeDepartmentService = A.Fake<IDepartmentService>();
            A.CallTo(() => fakeDepartmentService.GetAsync(A<string>.Ignored)).Returns(departmentDetailVM);
            Context.Services.AddScoped(x => fakeDepartmentService);

            var fakeInstructorService = A.Fake<IInstructorService>();
            Context.Services.AddScoped(x => fakeInstructorService);

            var comp = Context.RenderComponent<MudDialogProvider>();
            Assert.Empty(comp.Markup.Trim());

            var service = Context.Services.GetService<IDialogService>() as DialogService;
            Assert.NotNull(service);
            IDialogReference? dialogReference = null;

            var parameters = new DialogParameters();
            parameters.Add("DepartmentId", 1);

            var title = "Edit Department";
            await comp.InvokeAsync(() => dialogReference = service?.Show<DepartmentEdit>(title, parameters));
            Assert.NotNull(dialogReference);

            Assert.NotEmpty(comp.Markup.Trim());

            comp.Find("#Name").Change("");
            comp.Find("#Budget").Change("");
            comp.Find("#StartDate").Change("");
            comp.Find("#InstructorID").Change("");

            comp.Find("button[type='submit']").Click();

            comp.FindAll("div.validation-message")[0].TrimmedText().Should().Be("'Name' must not be empty.");
            comp.FindAll("div.validation-message")[1].TrimmedText().Should().Be("The Budget field must be a number.");
            comp.FindAll("div.validation-message")[2].TrimmedText().Should().Be("The StartDate field must be a date.");
            comp.FindAll("div.validation-message")[3].TrimmedText().Should().Be("The selected value  is not a valid number.");
        }
    }
}
