namespace WebUI.IntegrationTests
{
    public class InstructorsControllerTests : IntegrationTest
    {
        [Fact]
        public async Task GetAll_WithoutInstructors_ReturnsEmptyResponse()
        {
            var response = await _client.GetAsync("/api/instructors");
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }
    }
}
