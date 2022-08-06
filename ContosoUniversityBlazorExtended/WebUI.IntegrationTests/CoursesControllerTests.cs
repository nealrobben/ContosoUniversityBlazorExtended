using ContosoUniversityBlazor.Application.Common.Interfaces;
using ContosoUniversityBlazor.Domain.Entities;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using WebUI.Shared.Courses.Queries.GetCoursesOverview;

namespace WebUI.IntegrationTests
{
    public class CoursesControllerTests : IntegrationTest
    {
        [Fact]
        public async Task GetAll_WithoutCourses_ReturnsEmptyResponse()
        {
            var response = await _client.GetAsync("/api/courses");

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            (await response.Content.ReadAsAsync<CoursesOverviewVM>()).Courses.Should().BeEmpty();
        }

        [Fact]
        public async Task GetAll_WithCourses_ReturnsCourses()
        {
            var department = new Department
            {
                DepartmentID = 1,
                Name = "Name 1"
            };

            var course = new Course
            {
                CourseID = 1,
                Title = "Title 1",
                Credits = 2,
                DepartmentID = department.DepartmentID
            };

            using (var scope = _appFactory.Services.CreateScope())
            {
                var schoolContext = scope.ServiceProvider.GetRequiredService<ISchoolContext>();

                schoolContext.Departments.Add(department);
                schoolContext.Courses.Add(course);
                await schoolContext.SaveChangesAsync();
            }

            var response = await _client.GetAsync("/api/courses");

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            var result = (await response.Content.ReadAsAsync<CoursesOverviewVM>());
            result.Courses.Should().ContainSingle();
            result.Courses.First().CourseID.Should().Be(course.CourseID);
            result.Courses.First().Title.Should().Be(course.Title);
            result.Courses.First().Credits.Should().Be(course.Credits);
            result.Courses.First().DepartmentName.Should().Be(department.Name);
        }
    }
}
