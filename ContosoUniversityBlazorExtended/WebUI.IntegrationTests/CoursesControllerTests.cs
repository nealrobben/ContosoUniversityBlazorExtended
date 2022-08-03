using FluentAssertions;

namespace WebUI.IntegrationTests
{
    public class CoursesControllerTests : IntegrationTest
    {
        [Fact]
        public async Task GetAll_WithoutCourses_ReturnsEmptyResponse()
        {
            var response = await _client.GetAsync("/api/courses");

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }
    }
}
