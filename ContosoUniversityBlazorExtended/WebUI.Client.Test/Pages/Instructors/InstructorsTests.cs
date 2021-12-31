using Bunit;
using FakeItEasy;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor;
using WebUI.Client.Services;
using WebUI.Client.Test.Extensions;
using Xunit;

namespace WebUI.Client.Test.Pages.Instructors
{
    public class InstructorsTests : BunitTestBase
    {
        [Fact]
        public async Task Instructors_ClickCreateButton_OpensDialog()
        {
            var fakeInstructorService = A.Fake<IInstructorService>();
            Context.Services.AddScoped(x => fakeInstructorService);

            var fakeCourseService = A.Fake<ICourseService>();
            Context.Services.AddScoped(x => fakeCourseService);

            var fakeStudentService = A.Fake<IStudentService>();
            Context.Services.AddScoped(x => fakeStudentService);

            var fakeUploadService = A.Fake<IFileuploadService>();
            Context.Services.AddScoped(x => fakeUploadService);

            var dialog = Context.RenderComponent<MudDialogProvider>();
            Assert.Empty(dialog.Markup.Trim());

            var comp = Context.RenderComponent<Client.Pages.Instructors.Instructors>();
            Assert.NotEmpty(comp.Markup.Trim());

            comp.Find("h2").TrimmedText().Should().Be("Instructors");
            comp.Find("#CreateButton").Should().NotBeNull();

            comp.Find("#CreateButton").Click();

            Assert.NotEmpty(dialog.Markup.Trim());
        }
    }
}
