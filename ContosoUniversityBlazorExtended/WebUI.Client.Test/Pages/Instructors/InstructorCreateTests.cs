using Bunit;
using FakeItEasy;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor;
using WebUI.Client.Pages.Instructors;
using WebUI.Client.Services;
using WebUI.Client.Test.Extensions;
using Xunit;

namespace WebUI.Client.Test.Pages.Instructors
{
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
    }
}
