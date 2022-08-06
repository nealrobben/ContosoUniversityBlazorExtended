using ContosoUniversityBlazor.Application.Common.Interfaces;
using ContosoUniversityBlazor.Domain.Entities;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using WebUI.Shared.Instructors.Commands.CreateInstructor;
using WebUI.Shared.Instructors.Commands.UpdateInstructor;
using WebUI.Shared.Instructors.Queries.GetInstructorDetails;
using WebUI.Shared.Instructors.Queries.GetInstructorsLookup;
using WebUI.Shared.Instructors.Queries.GetInstructorsOverview;

namespace WebUI.IntegrationTests
{
    public class InstructorsControllerTests : IntegrationTest
    {
        [Fact]
        public async Task GetAll_WithoutInstructors_ReturnsEmptyResponse()
        {
            var response = await _client.GetAsync("/api/instructors");

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            (await response.Content.ReadAsAsync<InstructorsOverviewVM>()).Instructors.Should().BeEmpty();
        }

        [Fact]
        public async Task GetAll_WithInstructors_ReturnsInstructors()
        {
            var instructor = new Instructor
            {
                ID = 1,
                FirstMidName = "First name",
                LastName = "Last name",
                HireDate = DateTime.UtcNow,
            };

            using (var scope = _appFactory.Services.CreateScope())
            {
                var schoolContext = scope.ServiceProvider.GetRequiredService<ISchoolContext>();

                schoolContext.Instructors.Add(instructor);
                await schoolContext.SaveChangesAsync();
            }

            var response = await _client.GetAsync("/api/instructors");

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            var result = (await response.Content.ReadAsAsync<InstructorsOverviewVM>());

            result.Instructors.Should().ContainSingle();

            result.Instructors.First().InstructorID.Should().Be(instructor.ID);
            result.Instructors.First().FirstName.Should().Be(instructor.FirstMidName);
            result.Instructors.First().LastName.Should().Be(instructor.LastName);
            result.Instructors.First().HireDate.Should().Be(instructor.HireDate);
        }

        [Fact]
        public async Task GetSingle_WithNonExistingId_ReturnsNotFound()
        {
            var response = await _client.GetAsync("/api/courses/1");
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetSingle_WithExistingId_ReturnsInstructor()
        {
            var instructor = new Instructor
            {
                ID = 1,
                FirstMidName = "First name",
                LastName = "Last name",
                HireDate = DateTime.UtcNow
            };

            using (var scope = _appFactory.Services.CreateScope())
            {
                var schoolContext = scope.ServiceProvider.GetRequiredService<ISchoolContext>();

                schoolContext.Instructors.Add(instructor);
                await schoolContext.SaveChangesAsync();
            }

            var response = await _client.GetAsync("/api/instructors/1");

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            var result = (await response.Content.ReadAsAsync<InstructorDetailsVM>());

            result.InstructorID.Should().Be(instructor.ID);
            result.FirstName.Should().Be(instructor.FirstMidName);
            result.LastName.Should().Be(instructor.LastName);
            result.HireDate.Should().Be(instructor.HireDate);
        }

        [Fact]
        public async Task Create_CreatesInstructor()
        {
            var instructor = new CreateInstructorCommand
            {
                FirstName = "First name",
                LastName = "Last name",
                HireDate = DateTime.UtcNow
            };

            var response = await _client.PostAsJsonAsync("/api/instructors", instructor);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

            using (var scope = _appFactory.Services.CreateScope())
            {
                var schoolContext = scope.ServiceProvider.GetRequiredService<ISchoolContext>();

                schoolContext.Instructors.Should().ContainSingle();
                schoolContext.Instructors.First().FirstMidName.Should().Be(instructor.FirstName);
                schoolContext.Instructors.First().LastName.Should().Be(instructor.LastName);
                schoolContext.Instructors.First().HireDate.Should().Be(instructor.HireDate);
            }
        }

        [Fact]
        public async Task Delete_WithNonExistingId_ReturnsNotFound()
        {
            var response = await _client.DeleteAsync("/api/instructors/1");
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Delete_WithExistingId_DeletesInstructor()
        {
            using (var scope = _appFactory.Services.CreateScope())
            {
                var instructor = new Instructor
                {
                    ID = 1,
                    FirstMidName = "First name",
                    LastName = "Last name",
                    HireDate = DateTime.UtcNow
                };

                var schoolContext = scope.ServiceProvider.GetRequiredService<ISchoolContext>();
                schoolContext.Instructors.Add(instructor);
                await schoolContext.SaveChangesAsync();

                var response = await _client.DeleteAsync("/api/instructors/1");
                response.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);

                schoolContext.Instructors.Should().BeEmpty();
            }
        }

        [Fact]
        public async Task Update_UpdatesInstructor()
        {
            using (var scope = _appFactory.Services.CreateScope())
            {
                var instructor = new Instructor
                {
                    ID = 1,
                    FirstMidName = "First name",
                    LastName = "Last name",
                    HireDate = DateTime.UtcNow
                };

                var schoolContext = scope.ServiceProvider.GetRequiredService<ISchoolContext>();
                schoolContext.Instructors.Add(instructor);
                await schoolContext.SaveChangesAsync();
            }

            var updateInstructorCommand = new UpdateInstructorCommand
            {
                InstructorID = 1,
                FirstName = "First name 2",
                LastName = "Last name 2",
                HireDate = DateTime.UtcNow.AddDays(1)
            };

            var response = await _client.PutAsJsonAsync("/api/instructors", updateInstructorCommand);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);

            using (var scope = _appFactory.Services.CreateScope())
            {
                var schoolContext = scope.ServiceProvider.GetRequiredService<ISchoolContext>();

                schoolContext.Instructors.Should().ContainSingle();
                schoolContext.Instructors.First().ID.Should().Be(updateInstructorCommand.InstructorID);
                schoolContext.Instructors.First().FirstMidName.Should().Be(updateInstructorCommand.FirstName);
                schoolContext.Instructors.First().LastName.Should().Be(updateInstructorCommand.LastName);
                schoolContext.Instructors.First().HireDate.Should().Be(updateInstructorCommand.HireDate);
            }
        }

        [Fact]
        public async Task GetLookup_WithoutInstructors_ReturnsEmptyResponse()
        {
            var response = await _client.GetAsync("/api/instructors/lookup");

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            (await response.Content.ReadAsAsync<InstructorsLookupVM>()).Instructors.Should().BeEmpty();
        }

        [Fact]
        public async Task GetLookup_WithInstructors_ReturnsInstructors()
        {
            var instructor = new Instructor
            {
                ID = 1,
                FirstMidName = "First name",
                LastName = "Last name"
            };

            using (var scope = _appFactory.Services.CreateScope())
            {
                var schoolContext = scope.ServiceProvider.GetRequiredService<ISchoolContext>();

                schoolContext.Instructors.Add(instructor);
                await schoolContext.SaveChangesAsync();
            }

            var response = await _client.GetAsync("/api/instructors/lookup");

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            var result = (await response.Content.ReadAsAsync<InstructorsLookupVM>());

            result.Instructors.First().ID.Should().Be(instructor.ID);
            result.Instructors.First().FullName.Should().Be(instructor.FullName);
        }
    }
}
