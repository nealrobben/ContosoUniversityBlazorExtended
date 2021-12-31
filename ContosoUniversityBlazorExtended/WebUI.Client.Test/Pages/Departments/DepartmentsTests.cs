using Bunit;
using FakeItEasy;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor;
using WebUI.Client.Services;
using WebUI.Client.Test.Extensions;
using WebUI.Shared.Instructors.Queries.GetInstructorsLookup;
using Xunit;

namespace WebUI.Client.Test.Pages.Departments
{
    public class DepartmentsTests : BunitTestBase
    {
        [Fact]
        public async Task Departments_ClickCreateButton_OpensDialog()
        {
            var fakeDepartmentService = A.Fake<IDepartmentService>();
            Context.Services.AddScoped(x => fakeDepartmentService);

            var instructorsLookupVM = new InstructorsLookupVM
            {
                Instructors =
                {
                    new InstructorLookupVM
                    {
                        ID = 1,
                        FullName = "Instructor x"
                    }
                }
            };

            var fakeInstructorService = A.Fake<IInstructorService>();
            A.CallTo(() => fakeInstructorService.GetLookupAsync()).Returns(instructorsLookupVM);
            Context.Services.AddScoped(x => fakeInstructorService);

            var dialog = Context.RenderComponent<MudDialogProvider>();
            Assert.Empty(dialog.Markup.Trim());

            var comp = Context.RenderComponent<Client.Pages.Departments.Departments>();
            Assert.NotEmpty(comp.Markup.Trim());

            comp.Find("h2").TrimmedText().Should().Be("Departments");
            comp.Find("#CreateButton").Should().NotBeNull();

            comp.Find("#CreateButton").Click();

            Assert.NotEmpty(dialog.Markup.Trim());
        }
    }
}
