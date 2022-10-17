using ContosoUniversityBlazor.Application.Common.Interfaces;
using ContosoUniversityBlazor.Domain.Entities;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using WebUI.Shared.Departments.Commands.CreateDepartment;
using WebUI.Shared.Departments.Commands.UpdateDepartment;
using WebUI.Shared.Departments.Queries.GetDepartmentDetails;
using WebUI.Shared.Departments.Queries.GetDepartmentsLookup;
using WebUI.Shared.Departments.Queries.GetDepartmentsOverview;
using WebUI.Shared.Common;

namespace WebUI.IntegrationTests;

public class DepartmentsControllerTests : IntegrationTest
{
    [Fact]
    public async Task GetAll_WithoutDepartments_ReturnsEmptyResponse()
    {
        var response = await _client.GetAsync("/api/departments");

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        (await response.Content.ReadAsAsync<OverviewVM<DepartmentVM>>()).Records.Should().BeEmpty();
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
        var result = (await response.Content.ReadAsAsync<OverviewVM<DepartmentVM>>());

        result.Records.Should().ContainSingle();
        result.Records.First().DepartmentID.Should().Be(department.DepartmentID);
        result.Records.First().Name.Should().Be(department.Name);
        result.Records.First().Budget.Should().Be(department.Budget);
        result.Records.First().StartDate.Should().Be(department.StartDate);
    }

    [Fact]
    public async Task GetAll_WithSearchString_ReturnsDepartmentsWithNameMatchingSearchString()
    {
        var department1 = new Department
        {
            DepartmentID = 1,
            Name = "abc",
            Budget = 123,
            StartDate = DateTime.UtcNow
        };

        var department2 = new Department
        {
            DepartmentID = 2,
            Name = "def",
            Budget = 123,
            StartDate = DateTime.UtcNow
        };

        using (var scope = _appFactory.Services.CreateScope())
        {
            var schoolContext = scope.ServiceProvider.GetRequiredService<ISchoolContext>();

            schoolContext.Departments.Add(department1);
            schoolContext.Departments.Add(department2);
            await schoolContext.SaveChangesAsync();
        }

        var response = await _client.GetAsync("/api/departments?searchString=ef");

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        var result = (await response.Content.ReadAsAsync<OverviewVM<DepartmentVM>>());

        result.Records.Should().ContainSingle();
        result.Records.First().DepartmentID.Should().Be(department2.DepartmentID);
        result.Records.First().Name.Should().Be(department2.Name);
        result.Records.First().Budget.Should().Be(department2.Budget);
        result.Records.First().StartDate.Should().Be(department2.StartDate);
    }

    [Fact]
    public async Task GetAll_WithOrder_ReturnsDepartmentsInCorrectOrder()
    {
        var department1 = new Department
        {
            DepartmentID = 1,
            Name = "abc",
            Budget = 123,
            StartDate = DateTime.UtcNow
        };

        var department2 = new Department
        {
            DepartmentID = 2,
            Name = "def",
            Budget = 123,
            StartDate = DateTime.UtcNow
        };

        using (var scope = _appFactory.Services.CreateScope())
        {
            var schoolContext = scope.ServiceProvider.GetRequiredService<ISchoolContext>();

            schoolContext.Departments.Add(department1);
            schoolContext.Departments.Add(department2);
            await schoolContext.SaveChangesAsync();
        }

        var response = await _client.GetAsync("/api/departments?sortOrder=name_desc");

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        var result = (await response.Content.ReadAsAsync<OverviewVM<DepartmentVM>>());

        result.Records.Count().Should().Be(2);
        result.Records.First().DepartmentID.Should().Be(department2.DepartmentID);
        result.Records.First().Name.Should().Be(department2.Name);
        result.Records.First().Budget.Should().Be(department2.Budget);
        result.Records.First().StartDate.Should().Be(department2.StartDate);
    }

    [Fact]
    public async Task GetAll_WithPageSize_ReturnsOnlyDepartmentsOnFirstPage()
    {
        var department1 = new Department
        {
            DepartmentID = 1,
            Name = "abc",
            Budget = 123,
            StartDate = DateTime.UtcNow
        };

        var department2 = new Department
        {
            DepartmentID = 2,
            Name = "def",
            Budget = 123,
            StartDate = DateTime.UtcNow
        };

        var department3 = new Department
        {
            DepartmentID = 3,
            Name = "ghi",
            Budget = 123,
            StartDate = DateTime.UtcNow
        };

        var department4 = new Department
        {
            DepartmentID = 4,
            Name = "jkl",
            Budget = 123,
            StartDate = DateTime.UtcNow
        };

        using (var scope = _appFactory.Services.CreateScope())
        {
            var schoolContext = scope.ServiceProvider.GetRequiredService<ISchoolContext>();

            schoolContext.Departments.Add(department1);
            schoolContext.Departments.Add(department2);
            schoolContext.Departments.Add(department3);
            schoolContext.Departments.Add(department4);
            await schoolContext.SaveChangesAsync();
        }

        var response = await _client.GetAsync("/api/departments?pageSize=2");

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        var result = (await response.Content.ReadAsAsync<OverviewVM<DepartmentVM>>());

        result.Records.Count.Should().Be(2);

        result.Records[0].DepartmentID.Should().Be(department1.DepartmentID);
        result.Records[0].Name.Should().Be(department1.Name);
        result.Records[0].Budget.Should().Be(department1.Budget);
        result.Records[0].StartDate.Should().Be(department1.StartDate);

        result.Records[1].DepartmentID.Should().Be(department2.DepartmentID);
        result.Records[1].Name.Should().Be(department2.Name);
        result.Records[1].Budget.Should().Be(department2.Budget);
        result.Records[1].StartDate.Should().Be(department2.StartDate);
    }

    [Fact]
    public async Task GetAll_WithPageSizeAndPageNumber_ReturnsOnlyDepartmentsOnSecondPage()
    {
        var department1 = new Department
        {
            DepartmentID = 1,
            Name = "abc",
            Budget = 123,
            StartDate = DateTime.UtcNow
        };

        var department2 = new Department
        {
            DepartmentID = 2,
            Name = "def",
            Budget = 123,
            StartDate = DateTime.UtcNow
        };

        var department3 = new Department
        {
            DepartmentID = 3,
            Name = "ghi",
            Budget = 123,
            StartDate = DateTime.UtcNow
        };

        var department4 = new Department
        {
            DepartmentID = 4,
            Name = "jkl",
            Budget = 123,
            StartDate = DateTime.UtcNow
        };

        using (var scope = _appFactory.Services.CreateScope())
        {
            var schoolContext = scope.ServiceProvider.GetRequiredService<ISchoolContext>();

            schoolContext.Departments.Add(department1);
            schoolContext.Departments.Add(department2);
            schoolContext.Departments.Add(department3);
            schoolContext.Departments.Add(department4);
            await schoolContext.SaveChangesAsync();
        }

        var response = await _client.GetAsync("/api/departments?pageNumber=1&pageSize=2");

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        var result = (await response.Content.ReadAsAsync<OverviewVM<DepartmentVM>>());

        result.Records.Count.Should().Be(2);

        result.Records[0].DepartmentID.Should().Be(department3.DepartmentID);
        result.Records[0].Name.Should().Be(department3.Name);
        result.Records[0].Budget.Should().Be(department3.Budget);
        result.Records[0].StartDate.Should().Be(department3.StartDate);

        result.Records[1].DepartmentID.Should().Be(department4.DepartmentID);
        result.Records[1].Name.Should().Be(department4.Name);
        result.Records[1].Budget.Should().Be(department4.Budget);
        result.Records[1].StartDate.Should().Be(department4.StartDate);
    }

    [Fact]
    public async Task GetSingle_WithNonExistingId_ReturnsNotFound()
    {
        var response = await _client.GetAsync("/api/departments/1");
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetSingle_WithExistingId_ReturnsDepartment()
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

        var response = await _client.GetAsync("/api/departments/1");

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        var result = (await response.Content.ReadAsAsync<DepartmentDetailVM>());
        result.DepartmentID.Should().Be(department.DepartmentID);
        result.Name.Should().Be(department.Name);
        result.Budget.Should().Be(department.Budget);
        result.StartDate.Should().Be(department.StartDate);
    }

    [Fact]
    public async Task Create_CreatesDepartment()
    {
        var department = new CreateDepartmentCommand
        {
            Name = "Test 1",
            Budget = 123,
            StartDate = DateTime.UtcNow,
            InstructorID = 1
        };

        var response = await _client.PostAsJsonAsync("/api/departments", department);

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

        using (var scope = _appFactory.Services.CreateScope())
        {
            var schoolContext = scope.ServiceProvider.GetRequiredService<ISchoolContext>();

            schoolContext.Departments.Should().ContainSingle();
            schoolContext.Departments.First().Name.Should().Be(department.Name);
            schoolContext.Departments.First().Budget.Should().Be(department.Budget);
            schoolContext.Departments.First().StartDate.Should().Be(department.StartDate);
            schoolContext.Departments.First().InstructorID.Should().Be(department.InstructorID);
        }
    }

    [Fact]
    public async Task Delete_WithNonExistingId_ReturnsNotFound()
    {
        var response = await _client.DeleteAsync("/api/departments/1");
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Delete_WithExistingId_DeletesDepartment()
    {
        using (var scope = _appFactory.Services.CreateScope())
        {
            var department = new Department
            {
                Name = "Test 1",
                Budget = 123,
                StartDate = DateTime.UtcNow
            };

            var schoolContext = scope.ServiceProvider.GetRequiredService<ISchoolContext>();
            schoolContext.Departments.Add(department);
            await schoolContext.SaveChangesAsync();

            var response = await _client.DeleteAsync("/api/departments/1");
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);

            schoolContext.Departments.Should().BeEmpty();
        }
    }

    [Fact]
    public async Task Update_UpdatesDepartment()
    {
        using (var scope = _appFactory.Services.CreateScope())
        {
            var department = new Department
            {
                DepartmentID = 1,
                Name = "Test 1",
                Budget = 123,
                StartDate = DateTime.UtcNow,
                InstructorID = 1
            };

            var schoolContext = scope.ServiceProvider.GetRequiredService<ISchoolContext>();
            schoolContext.Departments.Add(department);
            await schoolContext.SaveChangesAsync();
        }
        
        var updateDepartmentCommand = new UpdateDepartmentCommand
        {
            DepartmentID = 1,
            Name = "Test 2",
            Budget = 456,
            StartDate = DateTime.UtcNow.AddDays(1),
            InstructorID = 2
        };
        
        var response = await _client.PutAsJsonAsync("/api/departments", updateDepartmentCommand);
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);

        using (var scope = _appFactory.Services.CreateScope())
        {
            var schoolContext = scope.ServiceProvider.GetRequiredService<ISchoolContext>();

            schoolContext.Departments.Should().ContainSingle();
            schoolContext.Departments.First().Name.Should().Be(updateDepartmentCommand.Name);
            schoolContext.Departments.First().Budget.Should().Be(updateDepartmentCommand.Budget);
            schoolContext.Departments.First().StartDate.Should().Be(updateDepartmentCommand.StartDate);
            schoolContext.Departments.First().InstructorID.Should().Be(updateDepartmentCommand.InstructorID);
        }
    }

    [Fact]
    public async Task GetLookup_WithoutDepartments_ReturnsEmptyResponse()
    {
        var response = await _client.GetAsync("/api/departments/lookup");

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        (await response.Content.ReadAsAsync<DepartmentsLookupVM>()).Departments.Should().BeEmpty();
    }

    [Fact]
    public async Task GetLookup_WithDepartments_ReturnsDepartments()
    {
        var department = new Department
        {
            DepartmentID = 1,
            Name = "Test 1"
        };

        using (var scope = _appFactory.Services.CreateScope())
        {
            var schoolContext = scope.ServiceProvider.GetRequiredService<ISchoolContext>();

            schoolContext.Departments.Add(department);
            await schoolContext.SaveChangesAsync();
        }

        var response = await _client.GetAsync("/api/departments/lookup");

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        var result = (await response.Content.ReadAsAsync<DepartmentsLookupVM>());
        result.Departments.Should().ContainSingle();
        result.Departments.First().DepartmentID.Should().Be(department.DepartmentID);
        result.Departments.First().Name.Should().Be(department.Name);
    }
}