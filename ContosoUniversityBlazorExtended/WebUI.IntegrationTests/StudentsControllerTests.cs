namespace WebUI.IntegrationTests
{
    public class StudentsControllerTests : IntegrationTest
    {
        [Fact]
        public async Task GetAll_WithoutStudents_ReturnsEmptyResponse()
        {
            var response = await _client.GetAsync("/api/students");
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }
    }
}
