using FluentAssertions;
using WebUI.Shared.Instructors.Queries.GetInstructorsOverview;

namespace WebUI.IntegrationTests
{
    public class InstructorsControllerTests : IntegrationTest
    {
        [Fact]
        public async Task GetAll_WithoutInstructors_ReturnsEmptyResponse()
        {
            var response = await _client.GetAsync("/api/instructors");

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            (await response.Content.ReadAsAsync<InstructorsOverviewVM>()).Instructors.Should().BeEmpty();
        }
    }
}
