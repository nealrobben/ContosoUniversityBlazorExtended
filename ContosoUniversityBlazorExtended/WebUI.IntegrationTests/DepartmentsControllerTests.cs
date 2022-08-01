namespace WebUI.IntegrationTests
{
    public class DepartmentsControllerTests : IntegrationTest
    {
        [Fact]
        public async Task GetAll_WithoutDepartments_ReturnsEmptyResponse()
        {
            var response = await _client.GetAsync("/departments");
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }
    }
}