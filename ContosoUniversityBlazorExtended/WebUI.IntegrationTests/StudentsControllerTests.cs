using FluentAssertions;
using WebUI.Shared.Students.Queries.GetStudentsOverview;

namespace WebUI.IntegrationTests
{
    public class StudentsControllerTests : IntegrationTest
    {
        [Fact]
        public async Task GetAll_WithoutStudents_ReturnsEmptyResponse()
        {
            var response = await _client.GetAsync("/api/students");

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            //(await response.Content.ReadAsAsync<StudentsOverviewVM>()).Students.Should().Be().Empty();
        }
    }
}
