using ContosoUniversityBlazor.Application.Common.Interfaces;
using ContosoUniversityBlazor.Domain.Entities;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using WebUI.Shared.Common;
using WebUI.Shared.Students.Commands.CreateStudent;
using WebUI.Shared.Students.Commands.UpdateStudent;
using WebUI.Shared.Students.Queries.GetStudentDetails;
using WebUI.Shared.Students.Queries.GetStudentsForCourse;
using WebUI.Shared.Students.Queries.GetStudentsOverview;

namespace WebUI.IntegrationTests;

public class StudentsControllerTests : IntegrationTest
{
    [Fact]
    public async Task GetAll_WithoutStudents_ReturnsEmptyResponse()
    {
        var response = await _client.GetAsync("/api/students");

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        (await response.Content.ReadAsAsync<OverviewVM<StudentOverviewVM>>()).Records.Should().BeEmpty();
    }

    [Fact]
    public async Task GetAll_WitStudents_ReturnsStudents()
    {
        var student = new Student
        {
            ID = 1,
            FirstMidName = "First name",
            LastName = "Last name",
            EnrollmentDate = DateTime.UtcNow
        };

        using (var scope = _appFactory.Services.CreateScope())
        {
            var schoolContext = scope.ServiceProvider.GetRequiredService<ISchoolContext>();

            schoolContext.Students.Add(student);
            await schoolContext.SaveChangesAsync();
        }

        var response = await _client.GetAsync("/api/students");

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        var result = (await response.Content.ReadAsAsync<OverviewVM<StudentOverviewVM>>());
        
        result.Records.Should().ContainSingle();
        result.Records.First().StudentID.Should().Be(student.ID);
        result.Records.First().FirstName.Should().Be(student.FirstMidName);
        result.Records.First().LastName.Should().Be(student.LastName);
        result.Records.First().EnrollmentDate.Should().Be(student.EnrollmentDate);
    }

    [Fact]
    public async Task GetAll_WithSearchString_ReturnsStudentsWithNameMatchingSearchString()
    {
        var student1 = new Student
        {
            ID = 1,
            FirstMidName = "ab",
            LastName = "c",
            EnrollmentDate = DateTime.UtcNow
        };

        var student2 = new Student
        {
            ID = 2,
            FirstMidName = "de",
            LastName = "f",
            EnrollmentDate = DateTime.UtcNow
        };

        using (var scope = _appFactory.Services.CreateScope())
        {
            var schoolContext = scope.ServiceProvider.GetRequiredService<ISchoolContext>();

            schoolContext.Students.Add(student1);
            schoolContext.Students.Add(student2);
            await schoolContext.SaveChangesAsync();
        }

        var response = await _client.GetAsync("/api/students?searchString=de");

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        var result = (await response.Content.ReadAsAsync<OverviewVM<StudentOverviewVM>>());

        result.Records.Should().ContainSingle();

        result.Records.First().StudentID.Should().Be(student2.ID);
        result.Records.First().FirstName.Should().Be(student2.FirstMidName);
        result.Records.First().LastName.Should().Be(student2.LastName);
        result.Records.First().EnrollmentDate.Should().Be(student2.EnrollmentDate);
    }

    [Fact]
    public async Task GetAll_WithOrder_ReturnsStudentsInCorrectOrder()
    {
        var student1 = new Student
        {
            ID = 1,
            FirstMidName = "ab",
            LastName = "c",
            EnrollmentDate = DateTime.UtcNow
        };

        var student2 = new Student
        {
            ID = 2,
            FirstMidName = "de",
            LastName = "f",
            EnrollmentDate = DateTime.UtcNow
        };

        using (var scope = _appFactory.Services.CreateScope())
        {
            var schoolContext = scope.ServiceProvider.GetRequiredService<ISchoolContext>();

            schoolContext.Students.Add(student1);
            schoolContext.Students.Add(student2);
            await schoolContext.SaveChangesAsync();
        }

        var response = await _client.GetAsync("/api/students?sortOrder=lastname_desc");

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        var result = (await response.Content.ReadAsAsync<OverviewVM<StudentOverviewVM>>());

        result.Records.Count.Should().Be(2);

        result.Records[0].StudentID.Should().Be(student2.ID);
        result.Records[0].FirstName.Should().Be(student2.FirstMidName);
        result.Records[0].LastName.Should().Be(student2.LastName);
        result.Records[0].EnrollmentDate.Should().Be(student2.EnrollmentDate);

        result.Records[1].StudentID.Should().Be(student1.ID);
        result.Records[1].FirstName.Should().Be(student1.FirstMidName);
        result.Records[1].LastName.Should().Be(student1.LastName);
        result.Records[1].EnrollmentDate.Should().Be(student1.EnrollmentDate);
    }

    [Fact]
    public async Task GetAll_WithPageSize_ReturnsOnlyStudentsOnFirstPage()
    {
        var student1 = new Student
        {
            ID = 1,
            FirstMidName = "ab",
            LastName = "c",
            EnrollmentDate = DateTime.UtcNow
        };

        var student2 = new Student
        {
            ID = 2,
            FirstMidName = "de",
            LastName = "f",
            EnrollmentDate = DateTime.UtcNow
        };

        var student3 = new Student
        {
            ID = 3,
            FirstMidName = "gh",
            LastName = "i",
            EnrollmentDate = DateTime.UtcNow
        };

        var student4 = new Student
        {
            ID = 4,
            FirstMidName = "jk",
            LastName = "l",
            EnrollmentDate = DateTime.UtcNow
        };

        using (var scope = _appFactory.Services.CreateScope())
        {
            var schoolContext = scope.ServiceProvider.GetRequiredService<ISchoolContext>();

            schoolContext.Students.Add(student1);
            schoolContext.Students.Add(student2);
            schoolContext.Students.Add(student3);
            schoolContext.Students.Add(student4);
            await schoolContext.SaveChangesAsync();
        }

        var response = await _client.GetAsync("/api/students?pageSize=2");

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        var result = (await response.Content.ReadAsAsync<OverviewVM<StudentOverviewVM>>());

        result.Records.Count.Should().Be(2);

        result.Records[0].StudentID.Should().Be(student1.ID);
        result.Records[0].FirstName.Should().Be(student1.FirstMidName);
        result.Records[0].LastName.Should().Be(student1.LastName);
        result.Records[0].EnrollmentDate.Should().Be(student1.EnrollmentDate);

        result.Records[1].StudentID.Should().Be(student2.ID);
        result.Records[1].FirstName.Should().Be(student2.FirstMidName);
        result.Records[1].LastName.Should().Be(student2.LastName);
        result.Records[1].EnrollmentDate.Should().Be(student2.EnrollmentDate);
    }

    [Fact]
    public async Task GetAll_WithPageSizeAndPageNumber_ReturnsOnlyStudentsOnSecondPage()
    {
        var student1 = new Student
        {
            ID = 1,
            FirstMidName = "ab",
            LastName = "c",
            EnrollmentDate = DateTime.UtcNow
        };

        var student2 = new Student
        {
            ID = 2,
            FirstMidName = "de",
            LastName = "f",
            EnrollmentDate = DateTime.UtcNow
        };

        var student3 = new Student
        {
            ID = 3,
            FirstMidName = "gh",
            LastName = "i",
            EnrollmentDate = DateTime.UtcNow
        };

        var student4 = new Student
        {
            ID = 4,
            FirstMidName = "jk",
            LastName = "l",
            EnrollmentDate = DateTime.UtcNow
        };

        using (var scope = _appFactory.Services.CreateScope())
        {
            var schoolContext = scope.ServiceProvider.GetRequiredService<ISchoolContext>();

            schoolContext.Students.Add(student1);
            schoolContext.Students.Add(student2);
            schoolContext.Students.Add(student3);
            schoolContext.Students.Add(student4);
            await schoolContext.SaveChangesAsync();
        }

        var response = await _client.GetAsync("/api/students?pageSize=2&pageNumber=1");

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        var result = (await response.Content.ReadAsAsync<OverviewVM<StudentOverviewVM>>());

        result.Records.Count.Should().Be(2);

        result.Records[0].StudentID.Should().Be(student3.ID);
        result.Records[0].FirstName.Should().Be(student3.FirstMidName);
        result.Records[0].LastName.Should().Be(student3.LastName);
        result.Records[0].EnrollmentDate.Should().Be(student3.EnrollmentDate);

        result.Records[1].StudentID.Should().Be(student4.ID);
        result.Records[1].FirstName.Should().Be(student4.FirstMidName);
        result.Records[1].LastName.Should().Be(student4.LastName);
        result.Records[1].EnrollmentDate.Should().Be(student4.EnrollmentDate);
    }

    [Fact]
    public async Task GetSingle_WithNonExistingId_ReturnsNotFound()
    {
        var response = await _client.GetAsync("/api/students/1");
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetSingle_WithExistingId_ReturnsStudent()
    {
        var student = new Student
        {
            ID = 1,
            FirstMidName = "First name",
            LastName = "Last name",
            EnrollmentDate = DateTime.UtcNow
        };

        using (var scope = _appFactory.Services.CreateScope())
        {
            var schoolContext = scope.ServiceProvider.GetRequiredService<ISchoolContext>();

            schoolContext.Students.Add(student);
            await schoolContext.SaveChangesAsync();
        }

        var response = await _client.GetAsync("/api/students/1");
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        var result = (await response.Content.ReadAsAsync<StudentDetailsVM>());

        result.StudentID.Should().Be(student.ID);
        result.FirstName.Should().Be(student.FirstMidName);
        result.LastName.Should().Be(student.LastName);
        result.EnrollmentDate.Should().Be(student.EnrollmentDate);
    }

    [Fact]
    public async Task Create_CreatesStudent()
    {
        var student = new CreateStudentCommand
        {
            FirstName = "First name",
            LastName = "Last name",
            EnrollmentDate = DateTime.UtcNow
        };

        var response = await _client.PostAsJsonAsync("/api/students", student);

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

        using (var scope = _appFactory.Services.CreateScope())
        {
            var schoolContext = scope.ServiceProvider.GetRequiredService<ISchoolContext>();

            schoolContext.Students.Should().ContainSingle();
            schoolContext.Students.First().FirstMidName.Should().Be(student.FirstName);
            schoolContext.Students.First().LastName.Should().Be(student.LastName);
            schoolContext.Students.First().EnrollmentDate.Should().Be(student.EnrollmentDate);
        }
    }

    [Fact]
    public async Task Delete_WithNonExistingId_ReturnsNotFound()
    {
        var response = await _client.DeleteAsync("/api/students/1");
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Delete_WithExistingId_DeletesStudent()
    {
        using (var scope = _appFactory.Services.CreateScope())
        {
            var student = new Student
            {
                ID = 1,
                FirstMidName = "First name",
                LastName = "Last name",
                EnrollmentDate = DateTime.UtcNow
            };

            var schoolContext = scope.ServiceProvider.GetRequiredService<ISchoolContext>();
            schoolContext.Students.Add(student);
            await schoolContext.SaveChangesAsync();

            var response = await _client.DeleteAsync("/api/students/1");
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);

            schoolContext.Students.Should().BeEmpty();
        }
    }

    [Fact]
    public async Task Update_UpdatesStudent()
    {
        using (var scope = _appFactory.Services.CreateScope())
        {
            var student = new Student
            {
                ID = 1,
                FirstMidName = "First name",
                LastName = "Last name",
                EnrollmentDate = DateTime.UtcNow
            };

            var schoolContext = scope.ServiceProvider.GetRequiredService<ISchoolContext>();
            schoolContext.Students.Add(student);
            await schoolContext.SaveChangesAsync();
        }

        var updateStudentCommand = new UpdateStudentCommand
        {
            StudentID = 1,
            FirstName = "First name 2",
            LastName = "Last name 2",
            EnrollmentDate = DateTime.UtcNow.AddDays(1)
        };

        var response = await _client.PutAsJsonAsync("/api/students", updateStudentCommand);
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);

        using (var scope = _appFactory.Services.CreateScope())
        {
            var schoolContext = scope.ServiceProvider.GetRequiredService<ISchoolContext>();

            schoolContext.Students.Should().ContainSingle();
            schoolContext.Students.First().ID.Should().Be(updateStudentCommand.StudentID);
            schoolContext.Students.First().FirstMidName.Should().Be(updateStudentCommand.FirstName);
            schoolContext.Students.First().LastName.Should().Be(updateStudentCommand.LastName);
            schoolContext.Students.First().EnrollmentDate.Should().Be(updateStudentCommand.EnrollmentDate);
        }
    }

    [Fact]
    public async Task ByCourse_WithoutStudents_ReturnsEmptyResponse()
    {
        var response = await _client.GetAsync("/api/students/bycourse/1");

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        (await response.Content.ReadAsAsync<StudentsForCourseVM>()).Students.Should().BeEmpty();
    }

    [Fact]
    public async Task ByCourse_WithStudents_ReturnsStudentsForCourse()
    {
        var student = new Student
        {
            ID = 1,
            FirstMidName = "First name",
            LastName = "Last name",
            EnrollmentDate = DateTime.UtcNow
        };

        var course = new Course
        {
            CourseID = 1,
            Title = "Title",
            Credits = 2
        };

        var enrollment = new Enrollment
        {
            CourseID = course.CourseID,
            StudentID = student.ID,
            Grade = ContosoUniversityBlazor.Domain.Enums.Grade.B
        };

        using (var scope = _appFactory.Services.CreateScope())
        {
            var schoolContext = scope.ServiceProvider.GetRequiredService<ISchoolContext>();

            schoolContext.Students.Add(student);
            schoolContext.Courses.Add(course);
            schoolContext.Enrollments.Add(enrollment);
            await schoolContext.SaveChangesAsync();
        }

        var response = await _client.GetAsync("/api/students/bycourse/1");

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        var result = (await response.Content.ReadAsAsync<StudentsForCourseVM>());
        result.Students.Should().ContainSingle();
        result.Students.First().StudentName.Should().Be(student.FullName);
        result.Students.First().StudentGrade.Should().Be(enrollment.Grade);
    }
}
