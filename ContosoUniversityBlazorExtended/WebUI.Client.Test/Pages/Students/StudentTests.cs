using Bunit;
using FakeItEasy;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor;
using WebUI.Client.Services;
using WebUI.Client.Test.Extensions;
using Xunit;

namespace WebUI.Client.Test.Pages.Students
{
    public class StudentTests : BunitTestBase
    {
        [Fact]
        public async Task Students_ClickCreateButton_OpensDialog()
        {
            var fakeStudentService = A.Fake<IStudentService>();
            Context.Services.AddScoped(x => fakeStudentService);

            var fakeUploadService = A.Fake<IFileuploadService>();
            Context.Services.AddScoped(x => fakeUploadService);

            var dialog = Context.RenderComponent<MudDialogProvider>();
            Assert.Empty(dialog.Markup.Trim());

            var comp = Context.RenderComponent<Client.Pages.Students.Students>();
            Assert.NotEmpty(comp.Markup.Trim());

            comp.Find("h2").TrimmedText().Should().Be("Students");
            comp.Find("#CreateButton").Should().NotBeNull();

            comp.Find("#CreateButton").Click();

            Assert.NotEmpty(dialog.Markup.Trim());
        }
    }
}
