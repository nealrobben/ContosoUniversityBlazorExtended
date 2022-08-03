using FluentAssertions;
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
    }
}
