using ContosoUniversityBlazor.Application.Common.Interfaces;
using ContosoUniversityBlazor.Domain.Entities;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using WebUI.Shared.Courses.Commands.CreateCourse;
using WebUI.Shared.Courses.Commands.UpdateCourse;
using WebUI.Shared.Courses.Queries.GetCourseDetails;
using WebUI.Shared.Courses.Queries.GetCoursesForInstructor;
using WebUI.Shared.Courses.Queries.GetCoursesOverview;

namespace WebUI.IntegrationTests
{
    public class CoursesControllerTests : IntegrationTest
    {
        [Fact]
        public async Task GetAll_WithoutCourses_ReturnsEmptyResponse()
        {
            var response = await _client.GetAsync("/api/courses");

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            (await response.Content.ReadAsAsync<CoursesOverviewVM>()).Courses.Should().BeEmpty();
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
            var result = (await response.Content.ReadAsAsync<CoursesOverviewVM>());
            result.Courses.Should().ContainSingle();
            result.Courses.First().CourseID.Should().Be(course.CourseID);
            result.Courses.First().Title.Should().Be(course.Title);
            result.Courses.First().Credits.Should().Be(course.Credits);
            result.Courses.First().DepartmentName.Should().Be(department.Name);
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
}
