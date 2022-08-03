using FluentAssertions;
using WebUI.Shared.Departments.Queries.GetDepartmentsOverview;

namespace WebUI.IntegrationTests
{
    public class DepartmentsControllerTests : IntegrationTest
    {
        [Fact]
        public async Task GetAll_WithoutDepartments_ReturnsEmptyResponse()
        {
            var response = await _client.GetAsync("/api/departments");

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            (await response.Content.ReadAsAsync<DepartmentsOverviewVM>()).Departments.Should().BeEmpty();
        }
    }
}