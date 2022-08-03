using FluentAssertions;

namespace WebUI.IntegrationTests
{
    public class DepartmentsControllerTests : IntegrationTest
    {
        [Fact]
        public async Task GetAll_WithoutDepartments_ReturnsEmptyResponse()
        {
            var response = await _client.GetAsync("/api/departments");

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }
    }
}