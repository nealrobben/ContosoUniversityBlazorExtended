using ContosoUniversityBlazor.Application.Common.Interfaces;
using ContosoUniversityBlazor.Domain.Entities;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using WebUI.Shared.Common;
using WebUI.Shared.Courses.Commands.CreateCourse;
using WebUI.Shared.Courses.Commands.UpdateCourse;
using WebUI.Shared.Courses.Queries.GetCourseDetails;
using WebUI.Shared.Courses.Queries.GetCoursesForInstructor;
using WebUI.Shared.Courses.Queries.GetCoursesOverview;

namespace WebUI.IntegrationTests;

public class CoursesControllerTests : IntegrationTest
{
    [Fact]
    public async Task GetAll_WithoutCourses_ReturnsEmptyResponse()
    {
        var response = await _client.GetAsync("/api/courses");

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        (await response.Content.ReadAsAsync<OverviewVM<CourseVM>>()).Records.Should().BeEmpty();
    }

    [Fact]
    public async Task GetAll_WithCourses_ReturnsCourses()
    {
        var department = new Department
        {
            DepartmentID = 1,
            Name = "Name 1"
        };

        var course = new Course
        {
            CourseID = 1,
            Title = "Title 1",
            Credits = 2,
            DepartmentID = department.DepartmentID
        };

        using (var scope = _appFactory.Services.CreateScope())
        {
            var schoolContext = scope.ServiceProvider.GetRequiredService<ISchoolContext>();

            schoolContext.Departments.Add(department);
            schoolContext.Courses.Add(course);
            await schoolContext.SaveChangesAsync();
        }

        var response = await _client.GetAsync("/api/courses");

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        var result = (await response.Content.ReadAsAsync<OverviewVM<CourseVM>>());
        result.Records.Should().ContainSingle();
        result.Records.First().CourseID.Should().Be(course.CourseID);
        result.Records.First().Title.Should().Be(course.Title);
        result.Records.First().Credits.Should().Be(course.Credits);
        result.Records.First().DepartmentName.Should().Be(department.Name);
    }

    [Fact]
    public async Task GetAll_SearchString_ReturnsCoursesWithTitleMatchingSearchString()
    {
        var department = new Department
        {
            DepartmentID = 1,
            Name = "Name 1"
        };

        var course1 = new Course
        {
            CourseID = 1,
            Title = "Economics",
            Credits = 2,
            DepartmentID = department.DepartmentID
        };

        var course2 = new Course
        {
            CourseID = 2,
            Title = "Math",
            Credits = 2,
            DepartmentID = department.DepartmentID
        };

        using (var scope = _appFactory.Services.CreateScope())
        {
            var schoolContext = scope.ServiceProvider.GetRequiredService<ISchoolContext>();

            schoolContext.Departments.Add(department);
            schoolContext.Courses.Add(course1);
            schoolContext.Courses.Add(course2);
            await schoolContext.SaveChangesAsync();
        }

        var response = await _client.GetAsync("/api/courses?searchString=math");

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        var result = (await response.Content.ReadAsAsync<OverviewVM<CourseVM>>());

        result.Records.Should().ContainSingle();
        result.Records.First().CourseID.Should().Be(course2.CourseID);
        result.Records.First().Title.Should().Be(course2.Title);
        result.Records.First().Credits.Should().Be(course2.Credits);
        result.Records.First().DepartmentName.Should().Be(department.Name);
    }


    [Fact]
    public async Task GetAll_WithOrder_ReturnsCoursesInCorrectOrder()
    {
        var department = new Department
        {
            DepartmentID = 1,
            Name = "Name 1"
        };

        var course1 = new Course
        {
            CourseID = 1,
            Title = "abc",
            Credits = 2,
            DepartmentID = department.DepartmentID
        };

        var course2 = new Course
        {
            CourseID = 2,
            Title = "def",
            Credits = 2,
            DepartmentID = department.DepartmentID
        };

        using (var scope = _appFactory.Services.CreateScope())
        {
            var schoolContext = scope.ServiceProvider.GetRequiredService<ISchoolContext>();

            schoolContext.Departments.Add(department);
            schoolContext.Courses.Add(course1);
            schoolContext.Courses.Add(course2);
            await schoolContext.SaveChangesAsync();
        }

        var response = await _client.GetAsync("/api/courses?sortOrder=title_desc");

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        var result = (await response.Content.ReadAsAsync<OverviewVM<CourseVM>>());

        result.Records.Count.Should().Be(2);
        result.Records.First().CourseID.Should().Be(course2.CourseID);
        result.Records.First().Title.Should().Be(course2.Title);
        result.Records.First().Credits.Should().Be(course2.Credits);
        result.Records.First().DepartmentName.Should().Be(department.Name);
    }

    [Fact]
    public async Task GetAll_WithPageSize_ReturnsOnlyCoursesOnFirstPage()
    {
        var department = new Department
        {
            DepartmentID = 1,
            Name = "Name 1"
        };

        var course1 = new Course
        {
            CourseID = 1,
            Title = "abc",
            Credits = 2,
            DepartmentID = department.DepartmentID
        };

        var course2 = new Course
        {
            CourseID = 2,
            Title = "def",
            Credits = 2,
            DepartmentID = department.DepartmentID
        };

        var course3 = new Course
        {
            CourseID = 3,
            Title = "ghi",
            Credits = 2,
            DepartmentID = department.DepartmentID
        };

        var course4 = new Course
        {
            CourseID = 4,
            Title = "jkl",
            Credits = 2,
            DepartmentID = department.DepartmentID
        };

        using (var scope = _appFactory.Services.CreateScope())
        {
            var schoolContext = scope.ServiceProvider.GetRequiredService<ISchoolContext>();

            schoolContext.Departments.Add(department);
            schoolContext.Courses.Add(course1);
            schoolContext.Courses.Add(course2);
            schoolContext.Courses.Add(course3);
            schoolContext.Courses.Add(course4);
            await schoolContext.SaveChangesAsync();
        }

        var response = await _client.GetAsync("/api/courses?pageSize=2");

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        var result = (await response.Content.ReadAsAsync<OverviewVM<CourseVM>>());

        result.Records.Count.Should().Be(2);

        result.Records[0].CourseID.Should().Be(course1.CourseID);
        result.Records[0].Title.Should().Be(course1.Title);
        result.Records[0].Credits.Should().Be(course1.Credits);
        result.Records[0].DepartmentName.Should().Be(department.Name);

        result.Records[1].CourseID.Should().Be(course2.CourseID);
        result.Records[1].Title.Should().Be(course2.Title);
        result.Records[1].Credits.Should().Be(course2.Credits);
        result.Records[1].DepartmentName.Should().Be(department.Name);
    }

    [Fact]
    public async Task GetAll_WithPageSizeAndNumber_ReturnsOnlyCoursesOnSecondPage()
    {
        var department = new Department
        {
            DepartmentID = 1,
            Name = "Name 1"
        };

        var course1 = new Course
        {
            CourseID = 1,
            Title = "abc",
            Credits = 2,
            DepartmentID = department.DepartmentID
        };

        var course2 = new Course
        {
            CourseID = 2,
            Title = "def",
            Credits = 2,
            DepartmentID = department.DepartmentID
        };

        var course3 = new Course
        {
            CourseID = 3,
            Title = "ghi",
            Credits = 2,
            DepartmentID = department.DepartmentID
        };

        var course4 = new Course
        {
            CourseID = 4,
            Title = "jkl",
            Credits = 2,
            DepartmentID = department.DepartmentID
        };

        using (var scope = _appFactory.Services.CreateScope())
        {
            var schoolContext = scope.ServiceProvider.GetRequiredService<ISchoolContext>();

            schoolContext.Departments.Add(department);
            schoolContext.Courses.Add(course1);
            schoolContext.Courses.Add(course2);
            schoolContext.Courses.Add(course3);
            schoolContext.Courses.Add(course4);
            await schoolContext.SaveChangesAsync();
        }

        var response = await _client.GetAsync("/api/courses?pageNumber=1&pageSize=2");

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        var result = (await response.Content.ReadAsAsync<OverviewVM<CourseVM>>());

        result.Records.Count.Should().Be(2);

        result.Records[0].CourseID.Should().Be(course3.CourseID);
        result.Records[0].Title.Should().Be(course3.Title);
        result.Records[0].Credits.Should().Be(course3.Credits);
        result.Records[0].DepartmentName.Should().Be(department.Name);

        result.Records[1].CourseID.Should().Be(course4.CourseID);
        result.Records[1].Title.Should().Be(course4.Title);
        result.Records[1].Credits.Should().Be(course4.Credits);
        result.Records[1].DepartmentName.Should().Be(department.Name);
    }

    [Fact]
    public async Task GetSingle_WithNonExistingId_ReturnsNotFound()
    {
        var response = await _client.GetAsync("/api/courses/1");
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetSingle_WithExistingId_ReturnsCourse()
    {
        var department = new Department
        {
            DepartmentID = 1,
            Name = "Name 1"
        };

        var course = new Course
        {
            CourseID = 1,
            Title = "Title 1",
            Credits = 2,
            DepartmentID = department.DepartmentID
        };

        using (var scope = _appFactory.Services.CreateScope())
        {
            var schoolContext = scope.ServiceProvider.GetRequiredService<ISchoolContext>();

            schoolContext.Departments.Add(department);
            schoolContext.Courses.Add(course);
            await schoolContext.SaveChangesAsync();
        }

        var response = await _client.GetAsync("/api/courses/1");

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        var result = (await response.Content.ReadAsAsync<CourseDetailVM>());
        result.CourseID.Should().Be(course.CourseID);
        result.Title.Should().Be(course.Title);
        result.Credits.Should().Be(course.Credits);
        result.DepartmentID.Should().Be(department.DepartmentID);
    }

    [Fact]
    public async Task Create_CreatesCourse()
    {
        var course = new CreateCourseCommand
        {
            CourseID = 1,
            Title = "Title 1",
            Credits = 2,
            DepartmentID = 1
        };

        var response = await _client.PostAsJsonAsync("/api/courses", course);

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

        using (var scope = _appFactory.Services.CreateScope())
        {
            var schoolContext = scope.ServiceProvider.GetRequiredService<ISchoolContext>();

            schoolContext.Courses.Should().ContainSingle();
            schoolContext.Courses.First().CourseID.Should().Be(course.CourseID);
            schoolContext.Courses.First().Title.Should().Be(course.Title);
            schoolContext.Courses.First().Credits.Should().Be(course.Credits);
            schoolContext.Courses.First().DepartmentID.Should().Be(course.DepartmentID);
        }
    }

    [Fact]
    public async Task Delete_WithNonExistingId_ReturnsNotFound()
    {
        var response = await _client.DeleteAsync("/api/courses/1");
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Delete_WithExistingId_DeletesCourse()
    {
        using (var scope = _appFactory.Services.CreateScope())
        {
            var course = new Course
            {
                CourseID = 1,
                Title = "Test 1",
                Credits = 2,
                DepartmentID = 3
            };

            var schoolContext = scope.ServiceProvider.GetRequiredService<ISchoolContext>();
            schoolContext.Courses.Add(course);
            await schoolContext.SaveChangesAsync();

            var response = await _client.DeleteAsync("/api/courses/1");
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);

            schoolContext.Courses.Should().BeEmpty();
        }
    }


    [Fact]
    public async Task Update_UpdatesCourse()
    {
        using (var scope = _appFactory.Services.CreateScope())
        {
            var course = new Course
            {
                CourseID = 1,
                Title = "Title 1",
                Credits = 2,
                DepartmentID = 3
            };

            var schoolContext = scope.ServiceProvider.GetRequiredService<ISchoolContext>();
            schoolContext.Courses.Add(course);
            await schoolContext.SaveChangesAsync();
        }

        var updateCourseCommand = new UpdateCourseCommand
        {
            CourseID = 1,
            Title = "Title 2",
            Credits = 22,
            DepartmentID = 4
        };

        var response = await _client.PutAsJsonAsync("/api/courses", updateCourseCommand);
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);

        using (var scope = _appFactory.Services.CreateScope())
        {
            var schoolContext = scope.ServiceProvider.GetRequiredService<ISchoolContext>();

            schoolContext.Courses.Should().ContainSingle();
            schoolContext.Courses.First().Title.Should().Be(updateCourseCommand.Title);
            schoolContext.Courses.First().Credits.Should().Be(updateCourseCommand.Credits);
            schoolContext.Courses.First().DepartmentID.Should().Be(updateCourseCommand.DepartmentID);
        }
    }

    [Fact]
    public async Task GetByInstructor_WithoutCourses_ReturnsEmptyResponse()
    {
        var response = await _client.GetAsync("/api/courses/byinstructor/1");

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        (await response.Content.ReadAsAsync<CoursesForInstructorOverviewVM>()).Courses.Should().BeEmpty();
    }

    [Fact]
    public async Task GetByInstructor_WithCourses_ReturnsCoursesForInstructor()
    {
        var instructor = new Instructor
        {
            ID = 1,
            FirstMidName = "Firstname",
            LastName = "Lastname"
        };

        var department = new Department
        {
            DepartmentID = 1,
            Name = "Department name"
        };

        var course = new Course
        {
            CourseID = 1,
            Title = "Title 1",
            DepartmentID = department.DepartmentID
        };

        var courseAssignment = new CourseAssignment
        {
            CourseID = 1,
            InstructorID = 1
        };

        using (var scope = _appFactory.Services.CreateScope())
        {
            var schoolContext = scope.ServiceProvider.GetRequiredService<ISchoolContext>();

            schoolContext.Instructors.Add(instructor);
            schoolContext.Departments.Add(department);
            schoolContext.Courses.Add(course);
            schoolContext.CourseAssignments.Add(courseAssignment);
            await schoolContext.SaveChangesAsync();
        }

        var response = await _client.GetAsync("/api/courses/byinstructor/1");

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        var result = (await response.Content.ReadAsAsync<CoursesForInstructorOverviewVM>());
        
        result.Courses.Should().ContainSingle();
        result.Courses.First().CourseID.Should().Be(course.CourseID);
        result.Courses.First().Title.Should().Be(course.Title);
        result.Courses.First().DepartmentName.Should().Be(department.Name);
    }
}
