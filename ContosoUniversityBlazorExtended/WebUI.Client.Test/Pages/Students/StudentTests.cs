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
        public void Students_ClickCreateButton_OpensDialog()
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

        [Fact]
        public void Students_ClickSearch_CallsStudentService()
        {
            var fakeStudentService = A.Fake<IStudentService>();
            Context.Services.AddScoped(x => fakeStudentService);

            var comp = Context.RenderComponent<Client.Pages.Students.Students>();
            Assert.NotEmpty(comp.Markup.Trim());

            comp.Find("#SearchButton").Should().NotBeNull();
            comp.Find("#SearchButton").Click();

            A.CallTo(() => fakeStudentService.GetAllAsync(A<string>.Ignored, A<int?>.Ignored, A<string>.Ignored, A<int?>.Ignored)).MustHaveHappened();
        }

        [Fact]
        public void Students_ClickBackToFullList_CallsStudentService()
        {
            var fakeStudentService = A.Fake<IStudentService>();
            Context.Services.AddScoped(x => fakeStudentService);

            var comp = Context.RenderComponent<Client.Pages.Students.Students>();
            Assert.NotEmpty(comp.Markup.Trim());

            comp.Find("#BackToFullListButton").Should().NotBeNull();
            comp.Find("#BackToFullListButton").Click();

            A.CallTo(() => fakeStudentService.GetAllAsync(A<string>.Ignored, A<int?>.Ignored, A<string>.Ignored, A<int?>.Ignored)).MustHaveHappened();
        }
    }
}
