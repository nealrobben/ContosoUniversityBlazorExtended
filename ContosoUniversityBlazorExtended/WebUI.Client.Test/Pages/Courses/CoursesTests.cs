using Bunit;
using FakeItEasy;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor;
using WebUI.Client.Services;
using WebUI.Client.Test.Extensions;
using WebUI.Shared.Departments.Queries.GetDepartmentsLookup;
using Xunit;

namespace WebUI.Client.Test.Pages.Courses
{
    public class CoursesTests : BunitTestBase
    {
        [Fact]
        public async Task Courses_ClickCreateButton_OpensDialog()
        {
            var fakeCourseService = A.Fake<ICourseService>();
            Context.Services.AddScoped(x => fakeCourseService);

            var departmentsLookupVM = new DepartmentsLookupVM
            {
                Departments =
                {
                    new DepartmentLookupVM
                    {
                        DepartmentID = 1,
                        Name = "Department x"
                    }
                }
            };

            var fakeDepartmentService = A.Fake<IDepartmentService>();
            A.CallTo(() => fakeDepartmentService.GetLookupAsync()).Returns(departmentsLookupVM);
            Context.Services.AddScoped(x => fakeDepartmentService);

            var dialog = Context.RenderComponent<MudDialogProvider>();
            Assert.Empty(dialog.Markup.Trim());

            var comp = Context.RenderComponent<Client.Pages.Courses.Courses>();
            Assert.NotEmpty(comp.Markup.Trim());

            comp.Find("h2").TrimmedText().Should().Be("Courses");
            comp.Find("#CreateButton").Should().NotBeNull();

            comp.Find("#CreateButton").Click();

            Assert.NotEmpty(dialog.Markup.Trim());
        }
    }
}
