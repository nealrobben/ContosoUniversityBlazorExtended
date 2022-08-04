using ContosoUniversityBlazor.Application.Common.Interfaces;
using ContosoUniversityBlazor.Domain.Entities;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
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

        [Fact]
        public async Task GetAll_WithDepartments_ReturnsDepartments()
        {
            var department = new Department
            {
                DepartmentID = 1,
                Name = "Test 1",
                Budget = 123,
                StartDate = DateTime.UtcNow
            };

            using (var scope = _appFactory.Services.CreateScope())
            {
                var schoolContext = scope.ServiceProvider.GetRequiredService<ISchoolContext>();

                schoolContext.Departments.Add(department);
                await schoolContext.SaveChangesAsync();
            }

            var response = await _client.GetAsync("/api/departments");

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            var result = (await response.Content.ReadAsAsync<DepartmentsOverviewVM>());
            result.Departments.Should().ContainSingle();
            result.Departments.First().DepartmentID.Should().Be(department.DepartmentID);
            result.Departments.First().Name.Should().Be(department.Name);
            result.Departments.First().Budget.Should().Be(department.Budget);
            result.Departments.First().StartDate.Should().Be(department.StartDate);
        }
    }
}