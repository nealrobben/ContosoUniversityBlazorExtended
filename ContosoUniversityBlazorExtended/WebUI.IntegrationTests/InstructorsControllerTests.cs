using FluentAssertions;

namespace WebUI.IntegrationTests
{
    public class InstructorsControllerTests : IntegrationTest
    {
        [Fact]
        public async Task GetAll_WithoutInstructors_ReturnsEmptyResponse()
        {
            var response = await _client.GetAsync("/api/instructors");

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }
    }
}
