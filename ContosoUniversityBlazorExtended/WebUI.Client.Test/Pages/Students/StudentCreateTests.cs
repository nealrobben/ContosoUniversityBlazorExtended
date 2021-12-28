using Bunit;
using FakeItEasy;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor;
using WebUI.Client.Pages.Students;
using WebUI.Client.Services;
using WebUI.Client.Test.Extensions;
using Xunit;

namespace WebUI.Client.Test.Pages.Students
{
    public class StudentCreateTests : BunitTestBase
    {
        [Fact]
        public async Task StudentCreate_DisplayDialogCorrectly()
        {
            var fakeInstructorService = A.Fake<IStudentService>();
            Context.Services.AddScoped(x => fakeInstructorService);

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
    }
}
