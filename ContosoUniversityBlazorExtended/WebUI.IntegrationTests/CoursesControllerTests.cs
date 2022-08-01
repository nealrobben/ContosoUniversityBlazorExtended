namespace WebUI.IntegrationTests
{
    public class CoursesControllerTests : IntegrationTest
    {
        [Fact]
        public async Task GetAll_WithoutCourses_ReturnsEmptyResponse()
        {
            var response = await _client.GetAsync("/api/courses");
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }
    }
}
