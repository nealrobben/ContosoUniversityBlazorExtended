using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebUI.Shared.Courses.Commands.CreateCourse;
using WebUI.Shared.Courses.Commands.UpdateCourse;
using WebUI.Shared.Courses.Queries.GetCourseDetails;
using WebUI.Shared.Courses.Queries.GetCoursesForInstructor;
using WebUI.Shared.Courses.Queries.GetCoursesOverview;

namespace WebUI.Client.Services
{
    public class CourseService : ServiceBase
    {
        public CourseService(HttpClient http) : base(http)
        {
        }

        public async Task<CoursesOverviewVM> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<CoursesOverviewVM>("/api/courses");
        }

        public async Task<CourseDetailVM> GetAsync(string id)
        {
            return await _http.GetFromJsonAsync<CourseDetailVM>($"/api/courses/{id}");
        }

        public async Task<HttpResponseMessage> DeleteAsync(string id)
        {
            return await _http.DeleteAsync($"/api/courses/{id}");
        }

        public async Task<HttpResponseMessage> CreateAsync(CreateCourseCommand createCommand)
        {
            return await _http.PostAsJsonAsync($"/api/courses", createCommand);
        }

        public async Task<HttpResponseMessage> UpdateAsync(UpdateCourseCommand createCommand)
        {
            return await _http.PutAsJsonAsync($"/api/courses", createCommand);
        }

        public async Task<CoursesForInstructorOverviewVM> GetCoursesForInstructor(string instructorId)
        {
            return await _http.GetFromJsonAsync<CoursesForInstructorOverviewVM>($"/api/courses/byinstructor/{instructorId}");
        }
    }
}
